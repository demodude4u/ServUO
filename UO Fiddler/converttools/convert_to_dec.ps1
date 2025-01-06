$folderPath = "C:\Users\Paul Manzella\Desktop\ServUO-ServUO_Pub57\UO Fiddler\export"
Set-Location -Path $folderPath

# Get all PNG files in the directory
Get-ChildItem -Filter *.png | ForEach-Object {
    $filename = $_.BaseName
    $extension = $_.Extension

    # Extract the hexadecimal part from the filename
    if ($filename -match '0x([0-9A-Fa-f]+)') {
        $hexPart = $matches[1]

        # Convert the hexadecimal part to a decimal number
        $decPart = [Convert]::ToInt32($hexPart, 16)

        # Create the new filename
        $newFilename = "$decPart$extension"

        # Rename the file
        Rename-Item -Path $_.FullName -NewName $newFilename
    }
}

Write-Output "Renaming complete."
