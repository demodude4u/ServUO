<#
.SYNOPSIS
  Reverts files that:
    1) Differ from HEAD by line endings only (no real content).
    2) Produce the "LF will be replaced by CRLF" warning from Git.

.DESCRIPTION
  - First, it collects all files that are changed according to normal Git diff.
  - Then, for each, it checks if ignoring CR-vs-LF differences yields an empty diff
    (meaning the file is only changed by line endings).
  - Next, it checks if "git diff HEAD -- <file>" produces the warning:
      "LF will be replaced by CRLF the next time Git touches it"
    on stderr.
  - Only if both conditions are met does it revert the file to match HEAD.

  This ensures that files with genuine edits remain untouched,
  while purely line-ending-changed files that cause Git's CRLF warning get reverted.

.USAGE
  powershell.exe -ExecutionPolicy Bypass -File .\Revert-OnlyLineEndingWarnings.ps1

#>

Write-Host "Collecting changed files..."
# 1. Gather all files that Git sees as modified (regardless of reason)
$changedFiles = git diff --name-only HEAD

if (-not $changedFiles) {
    Write-Host "No files are listed as changed. Exiting."
    return
}

# Split the output into an array
$changedFilesArr = $changedFiles -split "`r?`n" | Where-Object { $_ -ne "" }

if (-not $changedFilesArr) {
    Write-Host "No changed files found. Exiting."
    return
}

Write-Host "Checking each changed file..."

foreach ($file in $changedFilesArr) {
    # 2. Check if ignoring CR vs LF yields no diff (meaning line-end only)
    $ignoreEOLDiff = git diff --ignore-cr-at-eol HEAD -- $file

    # If there's content, it's not purely a CRLF difference
    if ($ignoreEOLDiff) {
        # There's some difference beyond line endings; skip revert
        continue
    }

    # 3. Check if Git produces the "LF will be replaced by CRLF" warning 
    #    for this file by running a standard diff and capturing stderr.
    $stdErrOutput = & git diff HEAD -- $file 2>&1 | Out-String

    # Look for the specific warning text in stderr
    if ($stdErrOutput -match "LF will be replaced by CRLF") {
        Write-Host "Reverting '$file' (pure CRLF difference + warning detected)."
        git checkout HEAD -- $file
    }
    else {
        # It's line-ending-only, but no "LF->CRLF" warning. 
        # Decide whether you still want to revert in this scenario.
        # For now, let's NOT revert if it doesn't produce that exact warning.
        # If you do want to revert them all, remove the 'if/else' and revert anyway.
    }
}

Write-Host "Done! Check 'git status' to see what's left."
