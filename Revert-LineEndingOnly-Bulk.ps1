# Revert-LineEndingOnly-Bulk.ps1
# ---------------------------------------------
# Reverts all files that differ from HEAD *only*
# by line endings (CRLF vs. LF). Real changes
# remain untouched.

Write-Host "Collecting changed files..."

# 1) Get ALL changed files (normal diff)
$filesNormal = git diff --name-only HEAD

# 2) Get changed files ignoring CR vs. LF
$filesIgnoreEOL = git diff --ignore-cr-at-eol --name-only HEAD

# Convert the results into arrays (in case they come back as multiline strings)
$filesNormalArr = $filesNormal -split "`r?`n" | Where-Object { $_ -ne "" }
$filesIgnoreEOLArr = $filesIgnoreEOL -split "`r?`n" | Where-Object { $_ -ne "" }

# 3) Identify files that appear in the normal diff,
#    but *do not* appear in the "ignore line endings" diff
#    => those are purely line-ending changes
$lineEndingOnly = Compare-Object -ReferenceObject $filesNormalArr -DifferenceObject $filesIgnoreEOLArr `
  | Where-Object { $_.SideIndicator -eq "<=" } `
  | Select-Object -ExpandProperty InputObject

if (-not $lineEndingOnly) {
    Write-Host "No line-ending-only changes found!"
    exit 0
}

Write-Host "Found files that differ by line endings only:"
$lineEndingOnly | ForEach-Object { Write-Host "  $_" }

# 4) Revert those files to match HEAD exactly
Write-Host "Reverting these files..."
foreach ($file in $lineEndingOnly) {
    Write-Host "  Reverting: $file"
    git checkout HEAD -- $file
}

Write-Host "Done! These files should no longer appear as changed."
