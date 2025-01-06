<#
.SYNOPSIS
  Reverts files that have no actual content changes ignoring CRLF, but do produce
  the "LF will be replaced by CRLF" warning in a normal diff.

.DESCRIPTION
  Steps:
    1) Collect all unstaged + staged changes (via `git diff --name-only HEAD` and 
       `git diff --cached --name-only HEAD`) to get a complete list.
    2) For each file:
       a. Use `git diff --ignore-cr-at-eol HEAD -- <file>` 
          while discarding stderr => if empty, only line endings differ.
       b. Use `git diff HEAD -- <file>` capturing stdout+stderr => 
          check if it contains "LF will be replaced by CRLF".
       c. If both a + b are true, revert the file to match HEAD.

.NOTES
  - If a file has trailing whitespace or BOM differences, they won't be ignored by `--ignore-cr-at-eol`. 
    Then the script won't revert it.
  - If Git doesn't print the exact text "LF will be replaced by CRLF," 
    the script won't revert that file.
#>

Write-Host "=== Gathering all changed files (unstaged + staged) ==="
Write-Host "-> Unstaged changes vs HEAD..."
$unstaged = git diff --name-only HEAD

Write-Host "-> Staged changes (index) vs HEAD..."
$staged   = git diff --cached --name-only HEAD

Write-Host "-> Combining results..."
$allChanged = ($unstaged + $staged) -split "`r?`n" | Where-Object { $_ -ne "" } | Sort-Object -Unique

if (-not $allChanged) {
    Write-Host "No changed files found. Exiting."
    return
}

Write-Host ""
Write-Host "Found $($allChanged.Count) changed files:"
$allChanged | ForEach-Object { Write-Host "  $_" }
Write-Host ""

foreach ($file in $allChanged) {
    Write-Host "`n--- Analyzing: $file ---"

    # 1) Check if ignoring CR-vs-LF yields an empty diff
    #    Here, we discard stderr so that "LF will be replaced..." warnings don't pollute $ignoreEolDiff.
    Write-Host "  -> Checking diff with --ignore-cr-at-eol (stdout only)..."
    $ignoreEolDiff = & git diff --ignore-cr-at-eol HEAD -- $file 2>$null | Out-String

    if ($ignoreEolDiff) {
        Write-Host "  => Some REAL difference ignoring CRLF. Skipping revert."
        continue
    } else {
        Write-Host "  => No diff ignoring CRLF => likely line-ending-only change."
    }

    # 2) Check if normal diff warns about "LF will be replaced by CRLF"
    Write-Host "  -> Checking normal diff for 'LF will be replaced by CRLF' warning..."
    $normalDiffAll = & git diff HEAD -- $file 2>&1 | Out-String

    if ($normalDiffAll -match "LF will be replaced by CRLF") {
        Write-Host "  => FOUND that warning. Reverting this file..."
        git checkout HEAD -- "$file"
    } else {
        Write-Host "  => No 'LF->CRLF' warning. Leaving it as-is."
    }
}

Write-Host "`n=== Done! Run 'git status' to see what's left. ==="
