using Server.Network;
using System.Globalization;
using System.Text;

namespace Server.Gumps
{
    public sealed class GumpSecondRenaissanceImage : GumpEntry
    {
        private static readonly byte[] LayoutName = Gump.StringToBuffer("srgumppic");
        private static readonly byte[] HueEquals = Gump.StringToBuffer(" hue=");

        private int _x;
        private int _y;
        private uint _logicalId;
        private int _hue;

        public GumpSecondRenaissanceImage(int x, int y, uint logicalId, int hue = 0)
        {
            _x = x;
            _y = y;
            _logicalId = logicalId;
            _hue = hue;
        }

        public override string Compile()
        {
            string command = string.Format(
                CultureInfo.InvariantCulture,
                "{{ srgumppic {0} {1} {2}",
                _x,
                _y,
                _logicalId
            );

            return _hue == 0 ? command + " }" : command + " hue=" + _hue + " }";
        }

        public override void AppendTo(IGumpWriter disp)
        {
            disp.AppendLayout(LayoutName);
            disp.AppendLayout(_x);
            disp.AppendLayout(_y);
            disp.AppendLayout(
                Encoding.ASCII.GetBytes(" " + _logicalId.ToString(CultureInfo.InvariantCulture))
            );

            if (_hue != 0)
            {
                disp.AppendLayout(HueEquals);
                disp.AppendLayoutNS(_hue);
            }
        }
    }
}
