$folderPath = "C:\Users\Paul Manzella\Desktop\ServUO-ServUO_Pub57\UO Fiddler\export"
Set-Location -Path $folderPath

# Get all PNG files in the directory
Get-ChildItem -Filter *.png | ForEach-Object {
    $filename = $_.BaseName
    $extension = $_.Extension

    # Extract the decimal part from the filename
    if ($filename -match '(\d+)') {
        $decPart = $matches[1]

        # Convert the decimal part to a hexadecimal number
        $hexPart = "{0:x}" -f [int]$decPart

        # Pad the hexadecimal part with zeros to make it four characters long
        $hexPartPadded = $hexPart.PadLeft(4, '0')

        # Create the new filename
        $newFilename = "Item 0x$hexPartPadded$extension"

        # Rename the file
        Rename-Item -Path $_.FullName -NewName $newFilename
    }
}

Write-Output "Renaming complete."
