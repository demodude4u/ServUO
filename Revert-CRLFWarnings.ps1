<#
.SYNOPSIS
  Detects Git warnings "LF will be replaced by CRLF..." and reverts those files.

.DESCRIPTION
  1) Runs `git diff HEAD` (or another command you choose).
  2) Captures stderr, looking for lines with the warning:
       "in the working copy of 'path/to/file', LF will be replaced by CRLF"
  3) Extracts the file path from each warning line.
  4) Reverts those files to match HEAD, removing them from the "modified" list.

.USAGE
  .\Revert-CRLFWarnings.ps1

.NOTES
  - This does NOT handle actual whitespace or BOM differences beyond line endings.
  - If you want to combine it with your existing "ignore CRLF" logic, you can do so,
    but this script focuses specifically on lines that produce Git's "LF replaced by CRLF" warning.
#>

Write-Host "Running Git diff to detect 'LF will be replaced by CRLF' warnings..."
# Execute `git diff HEAD` and capture both stdout and stderr
# because Git prints these line-ending warnings to stderr.
$diffOutput = & git diff HEAD 2>&1

# Alternatively, you could run `git diff --cached HEAD 2>&1` if you’re checking staged changes,
# or even `git diff 2>&1` if you want all changes vs. HEAD or the index.
# Adjust as needed.

if (-not $diffOutput) {
    Write-Host "No output from git diff. No warnings found."
    return
}

# Parse lines containing the warning
# Example line might be:
# "warning: in the working copy of 'bin/roslyn/vbc.rsp', LF will be replaced by CRLF the next time Git touches it"
Write-Host "Scanning for 'LF will be replaced by CRLF' warnings..."

$warningLines = $diffOutput | Select-String "LF will be replaced by CRLF"

if (-not $warningLines) {
    Write-Host "No warnings found about LF -> CRLF. Exiting."
    return
}

# Extract file paths from each warning line using a regex capture
# The pattern:
#   "working copy of '([^']+)'"
# captures the path between single quotes.
Write-Host "Files triggering LF->CRLF warnings:"
$filesToRevert = @()
foreach ($line in $warningLines) {
    if ($line -match "working copy of '([^']+)'") {
        $filePath = $matches[1]
        Write-Host "  $filePath"
        $filesToRevert += $filePath
    }
}

# If you want to confirm these paths are truly only line-ending changes, you could do an additional check
# (e.g., `git diff --ignore-cr-at-eol HEAD -- $file`) before reverting.

Write-Host "`nReverting these files to match HEAD..."
foreach ($file in $filesToRevert) {
    git checkout HEAD -- "$file"
}

Write-Host "Done! The above files should no longer appear modified just for LF/CRLF differences."
