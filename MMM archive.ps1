# Needs to be at least that version, or mmm can't read the archive
#Requires -Modules @{ ModuleName="Microsoft.PowerShell.Archive"; ModuleVersion="1.2.3" }
$Name = "MonkeSwim-Editor" # Replace with your mods name
$Version = "v1.0.1"

Compress-Archive -force .\Assets\ $Name-$Version.zip
