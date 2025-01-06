<#
.SYNOPSIS
  Reverts files that have no real content changes (ignoring line endings and whitespace).
.DESCRIPTION
  Scans the current Git repo for "changed" files, compares them to HEAD
  while ignoring CR-at-EOL and whitespace. If no real content changes
  are found, reverts them.

  Usage:
    1. Save as Revert-Unchanged.ps1 in your repo.
    2. Right-click > "Run with PowerShell" 
       or open a terminal in that directory and run: 
         .\Revert-Unchanged.ps1
#>

# Ensure we're in a Git repo
if (-not (Test-Path .git)) {
    Write-Host "This directory does not appear to be a Git repository. Exiting."
    exit 1
}

Write-Host "Scanning for files with no real content changes (ignoring CR-at-EOL & whitespace)..."

# Get the list of modified/added files. 
#   --porcelain produces a stable machine-readable output
#   We select only lines starting with 'A' or 'M' (but not 'D'), 
#   then grab the second token as the filename.
$changedFiles = git status --porcelain | Select-String '^[AM][^D]' | ForEach-Object {
    # "A" or "M" can be followed by optional status chars, but the
    # second field after that is the filename
    $line = $_.Line.Trim()
    # Typically looks like: "M file/path.txt"
    # We split on whitespace limit 2
    $parts = $line -split '\s+', 2
    if ($parts.Count -eq 2) {
        $parts[1]
    }
}

foreach ($file in $changedFiles) {
    # Compare file ignoring CR/LF differences and ignoring all whitespace
    $diffOutput = git diff --ignore-cr-at-eol --ignore-all-space HEAD -- $file
    
    if (-not $diffOutput) {
        # If there's no diff output, it means there's no real content change
        Write-Host "Reverting $file"
        git checkout HEAD -- $file
    }
}

Write-Host "Done!"
