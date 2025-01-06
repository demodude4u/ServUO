#region References
using System;
using System.Collections;
using System.Collections.Generic;
using Server.Factions;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.Ninjitsu;
using Server.Spells.Seventh;
using Server.Targeting;
using Server.Engines.VvV;
#endregion

namespace Server.SkillHandlers
{
    public delegate void ItemStolenEventHandler(ItemStolenEventArgs e);

	public class Stealing
	{
        //If true, vendors and monsters will NEVER lose the gold they give you.
        private static bool INFINITE_GOLD = false;
		
		public static void Initialize()
		{
			SkillInfo.Table[33].Callback = OnUse;
		}

        public static event ItemStolenEventHandler ItemStolen;

		public static readonly bool ClassicMode = false;
		public static readonly bool SuspendOnMurder = false;

		public static bool IsInGuild(Mobile m)
		{
			return (m is PlayerMobile && ((PlayerMobile)m).NpcGuild == NpcGuild.ThievesGuild);
		}

		public static bool IsInnocentTo(Mobile from, Mobile to)
		{
			return (Notoriety.Compute(from, to) == Notoriety.Innocent);
		}

		private class StealingTarget : Target
		{
			private readonly Mobile m_Thief;

			public StealingTarget(Mobile thief)
				: base(1, false, TargetFlags.None)
			{
				m_Thief = thief;

				AllowNonlocal = true;
			}

			private Item TryStealItem(Item toSteal, ref bool caught)
			{
				Item stolen = null;

				object root = toSteal.RootParent;

				StealableArtifactsSpawner.StealableInstance si = null;
				if (toSteal.Parent == null || !toSteal.Movable)
				{
					si = toSteal is AddonComponent ? StealableArtifactsSpawner.GetStealableInstance(((AddonComponent)toSteal).Addon) : StealableArtifactsSpawner.GetStealableInstance(toSteal);
				}

				if (!IsEmptyHanded(m_Thief))
				{
					m_Thief.SendLocalizedMessage(1005584); // Both hands must be free to steal.
				}
				else if (root is Mobile && ((Mobile)root).Player && !IsInGuild(m_Thief))
				{
					m_Thief.SendLocalizedMessage(1005596); // You must be in the thieves guild to steal from other players.
				}
				else if (SuspendOnMurder && root is Mobile && ((Mobile)root).Player && IsInGuild(m_Thief) && m_Thief.Kills > 0)
				{
					m_Thief.SendLocalizedMessage(502706); // You are currently suspended from the thieves guild.
				}
				else if (root is PlayerVendor)
				{
					m_Thief.SendLocalizedMessage(502709); // You can't steal from vendors.
				}
				else if (!m_Thief.CanSee(toSteal))
				{
					m_Thief.SendLocalizedMessage(500237); // Target can not be seen.
				}
				else if (m_Thief.Backpack == null || !m_Thief.Backpack.CheckHold(m_Thief, toSteal, false, true))
				{
					m_Thief.SendLocalizedMessage(1048147); // Your backpack can't hold anything else.
				}
				#region Sigils
				else if (toSteal is Sigil)
				{
					PlayerState pl = PlayerState.Find(m_Thief);
					Faction faction = (pl == null ? null : pl.Faction);

					Sigil sig = (Sigil)toSteal;

					if (!m_Thief.InRange(toSteal.GetWorldLocation(), 1))
					{
						m_Thief.SendLocalizedMessage(502703); // You must be standing next to an item to steal it.
					}
					else if (root != null) // not on the ground
					{
						m_Thief.SendLocalizedMessage(502710); // You can't steal that!
					}
					else if (faction != null)
					{
						if (!m_Thief.CanBeginAction(typeof(IncognitoSpell)))
						{
							m_Thief.SendLocalizedMessage(1010581); //	You cannot steal the sigil when you are incognito
						}
						else if (DisguiseTimers.IsDisguised(m_Thief))
						{
							m_Thief.SendLocalizedMessage(1010583); //	You cannot steal the sigil while disguised
						}
						else if (!m_Thief.CanBeginAction(typeof(PolymorphSpell)))
						{
							m_Thief.SendLocalizedMessage(1010582); //	You cannot steal the sigil while polymorphed				
						}
						else if (TransformationSpellHelper.UnderTransformation(m_Thief))
						{
							m_Thief.SendLocalizedMessage(1061622); // You cannot steal the sigil while in that form.
						}
						else if (AnimalForm.UnderTransformation(m_Thief))
						{
							m_Thief.SendLocalizedMessage(1063222); // You cannot steal the sigil while mimicking an animal.
						}
						else if (pl.IsLeaving)
						{
							m_Thief.SendLocalizedMessage(1005589); // You are currently quitting a faction and cannot steal the town sigil
						}
						else if (sig.IsBeingCorrupted && sig.LastMonolith.Faction == faction)
						{
							m_Thief.SendLocalizedMessage(1005590); //	You cannot steal your own sigil
						}
						else if (sig.IsPurifying)
						{
							m_Thief.SendLocalizedMessage(1005592); // You cannot steal this sigil until it has been purified
						}
						else if (m_Thief.CheckTargetSkill(SkillName.Stealing, toSteal, 80.0, 80.0))
						{
							if (Sigil.ExistsOn(m_Thief))
							{
								m_Thief.SendLocalizedMessage(1010258);
									//	The sigil has gone back to its home location because you already have a sigil.
							}
							else if (m_Thief.Backpack == null || !m_Thief.Backpack.CheckHold(m_Thief, sig, false, true))
							{
								m_Thief.SendLocalizedMessage(1010259); //	The sigil has gone home because your backpack is full
							}
							else
							{
								if (sig.IsBeingCorrupted)
								{
									sig.GraceStart = DateTime.UtcNow; // begin grace period
								}

								m_Thief.SendLocalizedMessage(1010586); // YOU STOLE THE SIGIL!!!   (woah, calm down now)

								if (sig.LastMonolith != null && sig.LastMonolith.Sigil != null)
								{
									sig.LastMonolith.Sigil = null;
									sig.LastStolen = DateTime.UtcNow;
								}

								return sig;
							}
						}
						else
						{
							m_Thief.SendLocalizedMessage(1005594); //	You do not have enough skill to steal the sigil
						}
					}
					else
					{
						m_Thief.SendLocalizedMessage(1005588); //	You must join a faction to do that
					}
				}
				#endregion
                #region VvV Sigils
                else if (toSteal is VvVSigil && ViceVsVirtueSystem.Instance != null)
                {
                    VvVPlayerEntry entry = ViceVsVirtueSystem.Instance.GetPlayerEntry<VvVPlayerEntry>(m_Thief);

                    VvVSigil sig = (VvVSigil)toSteal;

                    if (!m_Thief.InRange(toSteal.GetWorldLocation(), 1))
                    {
                        m_Thief.SendLocalizedMessage(502703); // You must be standing next to an item to steal it.
                    }
                    else if (root != null) // not on the ground
                    {
                        m_Thief.SendLocalizedMessage(502710); // You can't steal that!
                    }
                    else if (entry != null)
                    {
                        if (!m_Thief.CanBeginAction(typeof(IncognitoSpell)))
                        {
                            m_Thief.SendLocalizedMessage(1010581); //	You cannot steal the sigil when you are incognito
                        }
                        else if (DisguiseTimers.IsDisguised(m_Thief))
                        {
                            m_Thief.SendLocalizedMessage(1010583); //	You cannot steal the sigil while disguised
                        }
                        else if (!m_Thief.CanBeginAction(typeof(PolymorphSpell)))
                        {
                            m_Thief.SendLocalizedMessage(1010582); //	You cannot steal the sigil while polymorphed				
                        }
                        else if (TransformationSpellHelper.UnderTransformation(m_Thief))
                        {
                            m_Thief.SendLocalizedMessage(1061622); // You cannot steal the sigil while in that form.
                        }
                        else if (AnimalForm.UnderTransformation(m_Thief))
                        {
                            m_Thief.SendLocalizedMessage(1063222); // You cannot steal the sigil while mimicking an animal.
                        }
                        else if (m_Thief.CheckTargetSkill(SkillName.Stealing, toSteal, 100.0, 120.0))
                        {
                            if (m_Thief.Backpack == null || !m_Thief.Backpack.CheckHold(m_Thief, sig, false, true))
                            {
                                m_Thief.SendLocalizedMessage(1010259); //	The sigil has gone home because your backpack is full
                            }
                            else
                            {
                                m_Thief.SendLocalizedMessage(1010586); // YOU STOLE THE SIGIL!!!   (woah, calm down now)

                                sig.OnStolen(entry);

                                return sig;
                            }
                        }
                        else
                        {
                            m_Thief.SendLocalizedMessage(1005594); //	You do not have enough skill to steal the sigil
                        }
                    }
                    else
                    {
                        m_Thief.SendLocalizedMessage(1155415); //	Only participants in Vice vs Virtue may use this item.
                    }
                }
                #endregion

                else if (si == null && (toSteal.Parent == null || !toSteal.Movable) && !ItemFlags.GetStealable(toSteal))
				{
					m_Thief.SendLocalizedMessage(502710); // You can't steal that!
				}
				else if ((toSteal.LootType == LootType.Newbied || toSteal.CheckBlessed(root)) && !ItemFlags.GetStealable(toSteal))
				{
					m_Thief.SendLocalizedMessage(502710); // You can't steal that!
				}
				else if (Core.AOS && si == null && toSteal is Container && !ItemFlags.GetStealable(toSteal))
				{
					m_Thief.SendLocalizedMessage(502710); // You can't steal that!
				}
				else if (!m_Thief.InRange(toSteal.GetWorldLocation(), 1))
				{
					m_Thief.SendLocalizedMessage(502703); // You must be standing next to an item to steal it.
				}
				else if (si != null && m_Thief.Skills[SkillName.Stealing].Value < 100.0)
				{
					m_Thief.SendLocalizedMessage(1060025, "", 0x66D); // You're not skilled enough to attempt the theft of this item.
				}
				else if (toSteal.Parent is Mobile)
				{
					m_Thief.SendLocalizedMessage(1005585); // You cannot steal items which are equiped.
				}
				else if (root == m_Thief)
				{
					m_Thief.SendLocalizedMessage(502704); // You catch yourself red-handed.
				}
				else if (root is Mobile && ((Mobile)root).IsStaff())
				{
					m_Thief.SendLocalizedMessage(502710); // You can't steal that!
				}
				else if (root is Mobile && !m_Thief.CanBeHarmful((Mobile)root))
				{ }
				else if (root is Corpse)
				{
					m_Thief.SendLocalizedMessage(502710); // You can't steal that!
				}
				else
				{
					double w = toSteal.Weight + toSteal.TotalWeight;

					if (w > 10)
					{
						m_Thief.SendMessage("That is too heavy to steal.");
					}
					else
					{
						if (toSteal.Stackable && toSteal.Amount > 1)
						{
							int maxAmount = (int)((m_Thief.Skills[SkillName.Stealing].Value / 10.0) / toSteal.Weight);

							if (maxAmount < 1)
							{
								maxAmount = 1;
							}
							else if (maxAmount > toSteal.Amount)
							{
								maxAmount = toSteal.Amount;
							}

							int amount = Utility.RandomMinMax(1, maxAmount);

							if (amount >= toSteal.Amount)
							{
								int pileWeight = (int)Math.Ceiling(toSteal.Weight * toSteal.Amount);
								pileWeight *= 10;

								if (m_Thief.CheckTargetSkill(SkillName.Stealing, toSteal, pileWeight - 22.5, pileWeight + 27.5))
								{
									stolen = toSteal;
								}
							}
							else
							{
								int pileWeight = (int)Math.Ceiling(toSteal.Weight * amount);
								pileWeight *= 10;

								if (m_Thief.CheckTargetSkill(SkillName.Stealing, toSteal, pileWeight - 22.5, pileWeight + 27.5))
								{
									stolen = Mobile.LiftItemDupe(toSteal, toSteal.Amount - amount);

									if (stolen == null)
									{
										stolen = toSteal;
									}
								}
							}
						}
						else
						{
							int iw = (int)Math.Ceiling(w);
							iw *= 10;

							if (m_Thief.CheckTargetSkill(SkillName.Stealing, toSteal, iw - 22.5, iw + 27.5))
							{
								stolen = toSteal;
							}
						}

                        // Non-movable stealable (not in fillable container) items cannot result in the stealer getting caught
                        if (stolen != null && (root is FillableContainer || stolen.Movable))
                        {
                            double skillValue = m_Thief.Skills[SkillName.Stealing].Value;

                            if (root is FillableContainer)
                            {
								caught = Utility.Random(100) < ContainerStealCaughtChance(skillValue);
                                //caught = (Utility.Random((int)(skillValue / 2.5)) == 0); // 1 of 48 chance at 120
                            }
                            else
                            {
								caught = Utility.Random(100) < OpenStealCaughtChance(skillValue);
                                //caught = (skillValue < Utility.Random(150));
                            }
                        }
                        else
                        {
                            double skillValue = m_Thief.Skills[SkillName.Stealing].Value;
							caught = Utility.Random(100) < OpenStealCaughtChance(skillValue);
                        }

                        if (stolen != null)
						{
							m_Thief.SendLocalizedMessage(502724); // You succesfully steal the item.

							ItemFlags.SetTaken(stolen, true);
							ItemFlags.SetStealable(stolen, false);
							stolen.Movable = true;

                            InvokeItemStolen(new ItemStolenEventArgs(stolen, m_Thief));

							if (si != null)
							{
								toSteal.Movable = true;
								si.Item = null;
							}
						}
						else
						{
							m_Thief.SendLocalizedMessage(502723); // You fail to steal the item.
						}
					}
				}

				return stolen;
			}

            private bool PickTry(Mobile from, Mobile mobile, LokaiSuccessRating rating, Skill skill, Container pack)
            {
                bool pickGold, pickItem;
                bool pickNone = false;
                Item[] goldItems = pack.FindItemsByType(typeof(Gold));
                int goldAmount = 0;
                foreach (Item item in goldItems) goldAmount += item.Amount;

                List<Item> pickables = new List<Item>();
                Search(pack, ref pickables);

                if (pickables.Count > 0) pickItem = true; else pickItem = false;
                if (goldAmount > 0) pickGold = true; else pickGold = false;

                if (!pickGold && !pickItem)
                {
                    pickNone = true;
                }
                else if (pickGold && pickItem)
                {
                    pickGold = Utility.RandomBool();
                    pickItem = !pickGold;
                }

                if (rating >= LokaiSuccessRating.PartialSuccess)
                {
                    if (pickNone)
                    {
                        from.NextSkillTime = Core.TickCount;
                        from.SendMessage("Their pockets are empty.");
                    }
                    else if (pickGold)
                    {
                        int pickAmount = Math.Min((int)(4 * skill.Value), (int)(goldAmount / 1000));
                        if ((INFINITE_GOLD && !(mobile is PlayerMobile) || (goldAmount > 10 && pickAmount < 10)))
                            pickAmount = Utility.Random(6) + 5;
                        if (goldAmount > 100 && rating >= LokaiSuccessRating.CompleteSuccess && pickAmount < 100)
                            pickAmount = Utility.Random(61) + 40;
                        if (goldAmount > 500 && rating >= LokaiSuccessRating.ExceptionalSuccess && pickAmount < 500)
                            pickAmount = Utility.Random(301) + 200;
                        if (from.AddToBackpack(new Gold(pickAmount)))
                        {
                            if ((mobile is PlayerMobile && pack.ConsumeTotal(typeof(Gold), pickAmount) ||
                                (!INFINITE_GOLD && pack.ConsumeTotal(typeof(Gold), pickAmount))))
                                from.SendMessage("You were able to pick {0} gold from their pocket!", pickAmount.ToString());
                            else
                                from.SendMessage("You find {0} gold in their pocket!", pickAmount.ToString());
                        }
                    }
                    else
                    {
                        int random = Utility.Random(pickables.Count);
                        if (random >= pickables.Count) random = 0;
                        Item itemToPick = pickables[random];
                        if (pickables[random].Amount > 1 && from.AddToBackpack(itemToPick))
                        {
							from.SendMessage("An item was placed in your pack.");
                            itemToPick.Amount = 1;
                            pickables[random].Amount--;
                        }
                        else
                        {
                            if (from.AddToBackpack(itemToPick)) 
							{
								from.SendMessage("The item was placed in your pack.");
								pack.OnItemRemoved(pickables[random]);
							}
							else
							{
								from.SendMessage("I was unable to spawn the item in your pack.");
							}
                        }
                    }
                }
                else if (rating == LokaiSuccessRating.Failure)
                {
                    from.SendMessage("You fail.");
                }
                else if (rating == LokaiSuccessRating.HazzardousFailure || rating == LokaiSuccessRating.CriticalFailure)
                {
                    from.SendMessage("You fail utterly.");
                }
                else if (rating == LokaiSuccessRating.TooDifficult)
                {
                    from.SendMessage("You do not have the necessary skill to attempt this.");
                }
                double caughtChance = 1.0;

                switch (rating)
                {
                    case LokaiSuccessRating.CriticalFailure: caughtChance = 0.95; break;
                    case LokaiSuccessRating.HazzardousFailure: caughtChance = 0.8; break;
                    case LokaiSuccessRating.Failure: caughtChance = 0.65; break;
                    case LokaiSuccessRating.PartialSuccess: caughtChance = 0.5; break;
                    case LokaiSuccessRating.Success: caughtChance = 0.3; break;
                    case LokaiSuccessRating.CompleteSuccess: caughtChance = 0.1; break;
                    case LokaiSuccessRating.ExceptionalSuccess: caughtChance = 0.05; break;
                    case LokaiSuccessRating.TooEasy: caughtChance = -0.01; break;
                    default: break;
                }
                return caughtChance < Utility.RandomDouble();
            }

            private void Search(Container pack, ref List<Item> pickables)
            {
                foreach (Item item in pack.Items)
                {
                    if (item is Container) Search((item as Container), ref pickables);
                    if ((item is BaseArmor || item is BaseBeverage || item is BaseClothing || item is BaseJewel
                        || item is BaseReagent || item is BaseTool || item is BaseWeapon) && !item.QuestItem
                        && item.LootType == LootType.Regular && !item.Insured && item.Movable)
                        pickables.Add(item);
                }
            }

			protected override void OnTarget(Mobile from, object target)
			{
				from.RevealingAction();

				Item stolen = null;
				object root = null;
				bool caught = false;

				if (target is Item)
				{
					root = ((Item)target).RootParent;
					stolen = TryStealItem((Item)target, ref caught);
				}
				else if (target is Mobile)
				{
					root = target;
					Mobile mobile = target as Mobile;
                    Container pack = mobile.Backpack;
					if (pack == null)
					{
						from.SendMessage("Sorry. That being has no backpack from which to steal!");
						return;
					}
					
                    bool withoutNotice = true;
					// from.SendLocalizedMessage(1005598); // You can't steal from shopkeepers.
					
					LokaiSuccessRating rating = LokaiSkillCheck.CheckSkill(m_Thief, m_Thief.Skills[SkillName.Stealing], 0.0, 60.0);
					withoutNotice = PickTry(m_Thief, mobile, rating, m_Thief.Skills[SkillName.Stealing], pack);

					if (!withoutNotice)
					{
						if (mobile is PlayerMobile)
						{
							from.CriminalAction(true);
							from.OnHarmfulAction(mobile, from.Criminal);
						}
						else if (mobile is BaseVendor)
						{
							JailUtility.CatchThief(mobile, from);
						}
						else if (mobile is BaseCreature)
						{
							(mobile as BaseCreature).AggressiveAction(from, from.Criminal);
						}
					}
				}
                else if (target is StaticTarget)
                {
                    StaticTarget staticTarget = target as StaticTarget;
                    if (staticTarget.Location.CompareTo(from.Location) > 2)
                    {
                        from.SendMessage("You are too far away to do that.");
                        return;
                    }
                    PilferFlags flags = PilferTarget(staticTarget, from);

                    if (flags != PilferFlags.None)
                    {
                        if (Core.Debug)
                            from.SendMessage("TEST: pilfering staticTarget: {0}.", flags.ToString());
                        List<PilferFlags> list = new List<PilferFlags>();
                        foreach (PilferFlags flag in Enum.GetValues(typeof(PilferFlags)))
                        {
                            if (flag != PilferFlags.None && GetFlag(flag, flags))
                                list.Add(flag);
                        }

                        if (list.Count > 0)
                        {
                            PilferFlags pilfer = PilferFlags.None;
                            if (list.Count > 1)
                                pilfer = list[Utility.Random(list.Count)];
                            else
                                pilfer = list[0];

                            ///TEST FOR SUCCESSRATING
                            /// ---------------------

							LokaiSuccessRating rating = LokaiSkillCheck.CheckSkill(m_Thief, m_Thief.Skills[SkillName.Stealing], 0.0, 60.0);

                            ///IF SUCCESSFUL
                            /// ------------

                            if (rating >= LokaiSuccessRating.PartialSuccess)
                            {
                                CreateItem(pilfer, rating, from);
                            }
                            else
                            {
                                from.SendMessage("You fail to pilfer anything.");
                            }
                        }
                        else
                            from.SendMessage("There is nothing to pilfer there.");
                    }
                    else
                        from.SendMessage("There is nothing to pilfer there.");
                }/*
				else if (target is Mobile)
				{
					Container pack = ((Mobile)target).Backpack;

					if (pack != null && pack.Items.Count > 0)
					{
						int randomIndex = Utility.Random(pack.Items.Count);

						root = target;
						stolen = TryStealItem(pack.Items[randomIndex], ref caught);
					}

                    #region Monster Stealables
                    if (target is BaseCreature && from is PlayerMobile)
                    {
                        Server.Engines.CreatureStealing.StealingHandler.HandleSteal(target as BaseCreature, from as PlayerMobile);
                    }
                    #endregion
				}*/
				else
				{
					m_Thief.SendLocalizedMessage(502710); // You can't steal that!
				}

				if (stolen != null)
				{
                    if (stolen is AddonComponent)
                    {
                        BaseAddon addon = ((AddonComponent)stolen).Addon as BaseAddon;
                        from.AddToBackpack(addon.Deed);
                        addon.Delete();
                    }
                    else
                    {
                        from.AddToBackpack(stolen);
                    }

					if (!(stolen is Container || stolen.Stackable))
					{
						// do not return stolen containers or stackable items
						StolenItem.Add(stolen, m_Thief, root as Mobile);
					}
				}

				if (caught)
				{
					if (root == null)
					{
						//Console.WriteLine("root is null");
						m_Thief.CriminalAction(false);
					}
					else if (root is Corpse && ((Corpse)root).IsCriminalAction(m_Thief))
					{
						m_Thief.CriminalAction(false);
					}
					else if (root is Mobile)
					{
						Mobile mobRoot = (Mobile)root;

						if (!IsInGuild(mobRoot) && IsInnocentTo(m_Thief, mobRoot))
						{
							m_Thief.CriminalAction(false);
						}

						string message = String.Format("You notice {0} trying to steal from {1}.", m_Thief.Name, mobRoot.Name);

						foreach (NetState ns in m_Thief.GetClientsInRange(8))
						{
							if (ns.Mobile != m_Thief)
							{
								ns.Mobile.SendMessage(message);
							}
						}
					}
					else
					{
						foreach (Mobile mob in m_Thief.GetMobilesInRange(6))
						{
							if (mob is BaseVendor)
							{
								JailUtility.CatchThief(mob, m_Thief);
								break;
							}
						}
						//Console.WriteLine("root is something else");
					}
				}
				else if (root is Corpse && ((Corpse)root).IsCriminalAction(m_Thief))
				{
					m_Thief.CriminalAction(false);
				}

				if (root is Mobile && ((Mobile)root).Player && m_Thief is PlayerMobile && IsInnocentTo(m_Thief, (Mobile)root) &&
					!IsInGuild((Mobile)root))
				{
					PlayerMobile pm = (PlayerMobile)m_Thief;

					pm.PermaFlags.Add((Mobile)root);
					pm.Delta(MobileDelta.Noto);
				}
			}
		}

        private static void CreateItem(PilferFlags pilfer, LokaiSuccessRating rating, Mobile from)
        {
            switch (pilfer)
            {
                case PilferFlags.ArcheryWeapon:
                    {
                        Item item = Loot.RandomRangedWeapon();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Armor:
                    {
                        Item item = Loot.RandomArmorOrHat();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Clothes:
                    {
                        Item item = Loot.RandomClothing(from.Map == Map.Tokuno, true);
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Food:
                    {
                        Item item = RandomFood();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Jewel:
                    {
                        Item item = Loot.RandomJewelry();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.MetalWeapon:
                    {
                        Item item = Loot.RandomWeapon();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Potion:
                    {
                        Item item = Loot.RandomPotion();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Reagent:
                    {
                        Item item = Loot.RandomPossibleReagent();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Scroll:
                    {
                        int max = 0;
                        SpellbookType book = (SpellbookType)Utility.Random(2);
                        if (book == SpellbookType.Regular)
                        {
                            max = Loot.RegularScrollTypes.Length;
                            switch (rating)
                            {
                                case LokaiSuccessRating.PartialSuccess: { max /= 8; break; }
                                case LokaiSuccessRating.Success: { max /= 4; break; }
                                case LokaiSuccessRating.CompleteSuccess: { max /= 2; break; }
                                case LokaiSuccessRating.ExceptionalSuccess: { break; }
                            }
                        }
                        if (book == SpellbookType.Necromancer) max = Loot.SENecromancyScrollTypes.Length;
                        Item item = Loot.RandomScroll(0, max, book);
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Spellbook:
                    {
                        Item item = RandomSpellbook(rating);
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.DefaultName) ? "an" : "a", item.DefaultName);
                        break;
                    }
                case PilferFlags.Wand:
                    {
                        BaseWand item = Loot.RandomWand();
                        if (item == null || !from.AddToBackpack(item)) from.SendMessage("Unable to add item to backpack.");
                        else from.SendMessage("You pilfer {0} {1}.", StartsWithVowel(item.Effect.ToString()) ? "an" : "a", item.Effect.ToString());
                        break;
                    }
            }
        }

        private static bool StartsWithVowel(string text)
        {
            if (text == null || text == "") return false;
            return (text.StartsWith("a") || text.StartsWith("e") || text.StartsWith("i") || text.StartsWith("o") || text.StartsWith("u"));
        }

        private static Item RandomFood()
        {
            Type[] types = new Type[]{
                typeof(BreadLoaf), typeof(FrenchBread), typeof(Cake), typeof(Cookies),
                typeof(Muffins), typeof(CheesePizza), typeof(ApplePie), typeof(PeachCobbler),
                typeof(Quiche), typeof(Dough), typeof(JarHoney), typeof(Pitcher),
                typeof(SackFlour), typeof(Eggs), typeof(CheeseWheel), typeof(CookedBird),
                typeof(RoastPig), typeof(SackFlour), typeof(ChickenLeg), typeof(LambLeg),
                typeof(Skillet), typeof(FlourSifter), typeof(RollingPin),
                typeof(WoodenBowlOfCarrots), typeof(WoodenBowlOfCorn), typeof(WoodenBowlOfLettuce),
                typeof(WoodenBowlOfPeas), typeof(EmptyPewterBowl), typeof(PewterBowlOfCorn),
                typeof(PewterBowlOfLettuce), typeof(PewterBowlOfPeas), typeof(PewterBowlOfPotatos),
                typeof(WoodenBowlOfStew), typeof(WoodenBowlOfTomatoSoup)
            };

            try { return Activator.CreateInstance(types[Utility.Random(types.Length)]) as Item; }
            catch { return null; }
        }

        private static Item RandomSpellbook(LokaiSuccessRating rating)
        {
            Spellbook item = null;
            switch (Utility.Random(5))
            {
                default: item = new Spellbook(); break;
                case 1: item = new NecromancerSpellbook(); break;
                case 2: item = new BookOfBushido(); break;
                case 3: item = new BookOfChivalry(); break;
                case 4: item = new BookOfNinjitsu(); break;
            }
            if (item == null) return null;
            else
            {
                switch (item.SpellbookType)
                {
                    case SpellbookType.Regular:
                        {
                            switch (rating)
                            {
                                default:
                                    { item.Content = 0xFFFF; break; }
                                case LokaiSuccessRating.Success:
                                    { item.Content = 0xFFFFFFFF; break; }
                                case LokaiSuccessRating.CompleteSuccess:
                                    { item.Content = 0xFFFFFFFFFFFF; break; }
                                case LokaiSuccessRating.ExceptionalSuccess:
                                    { item.Content = ulong.MaxValue; break; }
                            }
                            break;
                        }
                    default: item.Content = (1ul << item.BookCount) - 1; break;
                }
            }
            return item;
        }

        [Flags]
        private enum PilferFlags
        {
            None = 0x000,
            Armor = 0x001,
            MetalWeapon = 0x002,
            Jewel = 0x004,
            Reagent = 0x008,
            Potion = 0x010,
            Food = 0x020,
            Clothes = 0x040,
            ArcheryWeapon = 0x080,
            Scroll = 0x100,
            Spellbook = 0x200,
            Wand = 0x400
        }

        private static bool IsFood(int itemID)
        {
            if ((itemID >= 0x970 && itemID <= 0x973) ||
                (itemID >= 0x976 && itemID <= 0x97E) ||
                (itemID >= 0x98C && itemID <= 0x9A7) ||
                (itemID >= 0x9AD && itemID <= 0x9AF) ||
                (itemID >= 0x9B3 && itemID <= 0x9FA) ||
                (itemID >= 0x1039 && itemID <= 0x1046) ||
                (itemID >= 0x15F8 && itemID <= 0x160C) ||
                (itemID >= 0x171D && itemID <= 0x172D) ||
                (itemID >= 0x1F7D && itemID <= 0x1F9E))
                return true;

            return false;
        }

        private static bool IsClothes(int itemID)
        {
            if ((itemID >= 0x1515 && itemID <= 0x1518) ||
                (itemID >= 0x152E && itemID <= 0x1531) ||
                (itemID >= 0x1537 && itemID <= 0x154C) ||
                (itemID >= 0x1EFD && itemID <= 0x1F04) ||
                (itemID >= 0x170B && itemID <= 0x171C))
                return true;

            return false;
        }

        private static bool IsArmor(int itemID)
        {
            if ((itemID >= 0x13BB && itemID <= 0x13E2) ||
                (itemID >= 0x13E5 && itemID <= 0x13F2) ||
                (itemID >= 0x1408 && itemID <= 0x141A) ||
                (itemID >= 0x144E && itemID <= 0x1457))
                return true;

            return false;
        }

        private static bool IsMetalWeapon(int itemID)
        {
            if ((itemID >= 0xF43 && itemID <= 0xF4E) ||
                (itemID >= 0xF51 && itemID <= 0xF52) ||
                (itemID >= 0xF5C && itemID <= 0xF63) ||
                (itemID >= 0x13AF && itemID <= 0x13B0) ||
                (itemID >= 0x13B5 && itemID <= 0x13BA) ||
                (itemID >= 0x13FA && itemID <= 0x13FB) ||
                (itemID >= 0x13FE && itemID <= 0x1407) ||
                (itemID >= 0x1438 && itemID <= 0x1443))
                return true;

            return false;
        }

        private static bool IsArcheryWeapon(int itemID)
        {
            if ((itemID >= 0xF4F && itemID <= 0xF50) ||
                (itemID >= 0x13B1 && itemID <= 0x13B2) ||
                (itemID >= 0x13FC && itemID <= 0x13FD))
                return true;

            return false;
        }

        private static int OpenStealCaughtChance(double stealing)
        {
			return
			stealing <= 5	? 100 :
			stealing <= 10  ?  95 :
			stealing <= 15  ?  90 :
			stealing <= 20  ?  85 :
			stealing <= 25  ?  80 :
			stealing <= 30  ?  75 :
			stealing <= 35  ?  70 :
			stealing <= 40  ?  65 :
			stealing <= 45  ?  60 :
			stealing <= 50  ?  55 :
			stealing <= 55  ?  50 :
			stealing <= 60  ?  45 :
			stealing <= 65  ?  40 :
			stealing <= 70  ?  35 :
			stealing <= 75  ?  30 :
			stealing <= 80  ?  25 :
			stealing <= 85  ?  20 :
			stealing <= 90  ?  15 :
			stealing <= 95  ?  10 :
			stealing <= 100 ?   7 :
			stealing <= 110 ?   5 :
			stealing <  120 ?   3 :	1;
		}

        private static int ContainerStealCaughtChance(double stealing)
        {
			return
			stealing <= 5	? 100 :
			stealing <= 10  ?  90 :
			stealing <= 15  ?  80 :
			stealing <= 20  ?  75 :
			stealing <= 25  ?  70 :
			stealing <= 30  ?  65 :
			stealing <= 35  ?  60 :
			stealing <= 40  ?  55 :
			stealing <= 45  ?  50 :
			stealing <= 50  ?  45 :
			stealing <= 55  ?  40 :
			stealing <= 60  ?  35 :
			stealing <= 65  ?  30 :
			stealing <= 70  ?  25 :
			stealing <= 75  ?  20 :
			stealing <= 80  ?  15 :
			stealing <= 85  ?  13 :
			stealing <= 90  ?  11 :
			stealing <= 95  ?   8 :
			stealing <= 100 ?   7 :
			stealing <= 110 ?   5 :
			stealing <  120 ?   3 : 1;
		}

        private static PilferFlags ProcessDisplayCase(StaticTile[] tiles)
        {
            PilferFlags flags = PilferFlags.None;

            for (int i = 0; i < tiles.Length; ++i)
            {
                int tileID = tiles[i].ID;
                tileID &= 0x3FFF;
                flags |= PilferTileItem(tileID);
            }

            if (Core.Debug)
                Console.WriteLine("TEST: Processed {0} tiles in the display case.", tiles.Length.ToString());

            return flags;
        }

        private static PilferFlags PilferTileItem(int itemID)
        {
            PilferFlags res = PilferFlags.None;

            ItemData id = TileData.ItemTable[itemID];
            TileFlag flags = id.Flags;

            if ((flags & TileFlag.Wearable) != 0)
            {
                if (IsClothes(itemID))
                    res |= PilferFlags.Clothes;
                else if (IsArmor(itemID))
                    res |= PilferFlags.Armor;
                else if (IsMetalWeapon(itemID))
                    res |= PilferFlags.MetalWeapon;
                else if (IsArcheryWeapon(itemID))
                    res |= PilferFlags.ArcheryWeapon;
            }

            if (IsFood(itemID))
                res |= PilferFlags.Food;

            if ((itemID >= 0xF0F && itemID <= 0xF30) ||
                (itemID >= 0x1F05 && itemID <= 0x1F0A))
                res |= PilferFlags.Jewel;

            if (itemID >= 0xEFB && itemID <= 0xF0E)
                res |= PilferFlags.Potion;

            if (itemID >= 0xF78 && itemID <= 0xF91)
                res |= PilferFlags.Reagent;

            if ((itemID >= 0xE34 && itemID <= 0xE3A) || 
                (itemID >= 0xEF3 && itemID <= 0xEF9) ||
                (itemID >= 0x1F2D && itemID <= 0x1F72) ||
                (itemID >= 0x2260 && itemID <= 0x227C) ||
                (itemID >= 0x2D51 && itemID <= 0x2D60))
                res |= PilferFlags.Scroll;

            if (itemID == 0xE38 || itemID == 0xEFA || 
               (itemID >= 0x2252 && itemID <= 0x2254) ||
                itemID == 0x238C || itemID == 0x23A0 || itemID == 0x2D50)
                res |= PilferFlags.Spellbook;

            if (itemID >= 0xDF0 && itemID <= 0xDF5)
                res |= PilferFlags.Wand;

            return res;
        }

        private static PilferFlags PilferTarget(StaticTarget target, Mobile from)
        {
            int itemID = target.ItemID;
            itemID &= 0x3FFF;

            StaticTile[] tiles = from.Map.Tiles.GetStaticTiles(target.X, target.Y);

            for (int i = 0; i < tiles.Length; ++i)
            {
                int tileID = tiles[i].ID;
                tileID &= 0x3FFF;
                if ((tileID >= 0xA9F && tileID <= 0xAA5) ||
                    (tileID >= 0xADF && tileID <= 0xB18))   //display case IDs
                {
                    return ProcessDisplayCase(tiles);
                }
            }
            //if (Core.Debug)
                Console.WriteLine("TEST: Not a display case.");

            return PilferTileItem(itemID);
        }

        private static bool GetFlag(PilferFlags flag, PilferFlags inFlags)
        {
            return ((inFlags & flag) != 0);
        }

        private static void SetFlag(PilferFlags flag, PilferFlags inFlags, bool value)
        {
            if (value)
                inFlags |= flag;
            else
                inFlags &= ~flag;
        }

		public static bool IsEmptyHanded(Mobile from)
		{
			if (from.FindItemOnLayer(Layer.OneHanded) != null)
			{
				return false;
			}

			if (from.FindItemOnLayer(Layer.TwoHanded) != null)
			{
				return false;
			}

			return true;
		}

		public static TimeSpan OnUse(Mobile m)
		{
			if (!IsEmptyHanded(m))
			{
				m.SendLocalizedMessage(1005584); // Both hands must be free to steal.
			}
			else
			{
				m.Target = new StealingTarget(m);
				m.RevealingAction();

				m.SendLocalizedMessage(502698); // Which item do you want to steal?
			}

			return TimeSpan.FromSeconds(10.0);
		}

        public static void InvokeItemStolen(ItemStolenEventArgs e)
        {
            if (ItemStolen != null)
            {
                ItemStolen(e);
            }
        }
	}

	public class StolenItem
	{
		public static readonly TimeSpan StealTime = TimeSpan.FromMinutes(2.0);

		private readonly Item m_Stolen;
		private readonly Mobile m_Thief;
		private readonly Mobile m_Victim;
		private DateTime m_Expires;

		public Item Stolen { get { return m_Stolen; } }
		public Mobile Thief { get { return m_Thief; } }
		public Mobile Victim { get { return m_Victim; } }
		public DateTime Expires { get { return m_Expires; } }

		public bool IsExpired { get { return (DateTime.UtcNow >= m_Expires); } }

		public StolenItem(Item stolen, Mobile thief, Mobile victim)
		{
			m_Stolen = stolen;
			m_Thief = thief;
			m_Victim = victim;

			m_Expires = DateTime.UtcNow + StealTime;
		}

		private static readonly Queue m_Queue = new Queue();

		public static void Add(Item item, Mobile thief, Mobile victim)
		{
			Clean();

			m_Queue.Enqueue(new StolenItem(item, thief, victim));
		}

		public static bool IsStolen(Item item)
		{
			Mobile victim = null;

			return IsStolen(item, ref victim);
		}

		public static bool IsStolen(Item item, ref Mobile victim)
		{
			Clean();

			foreach (StolenItem si in m_Queue)
			{
				if (si.m_Stolen == item && !si.IsExpired)
				{
					victim = si.m_Victim;
					return true;
				}
			}

			return false;
		}

		public static void ReturnOnDeath(Mobile killed, Container corpse)
		{
			Clean();

			foreach (StolenItem si in m_Queue)
			{
				if (si.m_Stolen.RootParent == corpse && si.m_Victim != null && !si.IsExpired)
				{
					if (si.m_Victim.AddToBackpack(si.m_Stolen))
					{
						si.m_Victim.SendLocalizedMessage(1010464); // the item that was stolen is returned to you.
					}
					else
					{
						si.m_Victim.SendLocalizedMessage(1010463); // the item that was stolen from you falls to the ground.
					}

					si.m_Expires = DateTime.UtcNow; // such a hack
				}
			}
		}

		public static void Clean()
		{
			while (m_Queue.Count > 0)
			{
				StolenItem si = (StolenItem)m_Queue.Peek();

				if (si.IsExpired)
				{
					m_Queue.Dequeue();
				}
				else
				{
					break;
				}
			}
		}
	}

    public class ItemStolenEventArgs : EventArgs
    {
        public Item Item { get; set; }
        public Mobile Mobile { get; set; }

        public ItemStolenEventArgs(Item item, Mobile thief)
        {
            Mobile = thief;
            Item = item;
        }
    }
}
