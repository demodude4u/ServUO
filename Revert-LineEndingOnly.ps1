<#
  SYNOPSIS
    Reverts files whose only difference from HEAD is the line-ending type
    (CRLF vs LF).

  DESCRIPTION
    This script looks at each file that Git currently lists as changed.
    If ignoring CR vs LF differences causes the diff to be empty,
    we revert that file back to HEAD.

  USAGE:
    1. Save this script as Revert-LineEndingOnly.ps1 in your repo.
    2. Open PowerShell in the repo’s root folder.
    3. Run: .\Revert-LineEndingOnly.ps1
#>

Write-Host "Reverting files with *only* line-ending (CR vs LF) differences..."

# Gather the list of changed files, ignoring deletions
$changedFiles = git status --porcelain | Select-String '^[AM][^D]' | ForEach-Object {
    $line = $_.Line.Trim()
    # Typically: "M path/to/file"
    $parts = $line -split '\s+', 2
    if ($parts.Count -eq 2) {
        $parts[1]
    }
}

foreach ($file in $changedFiles) {
    # Diff ignoring CR vs LF line-ending changes
    $diff = git diff --ignore-cr-at-eol HEAD -- $file
    
    # If diff output is empty, it means that the *only* difference was line endings
    if (-not $diff) {
        Write-Host "  Reverting line-ending-only changes for '$file'"
        git checkout HEAD -- $file
    }
}

Write-Host "Done!"
