#
# Must be executed with Administrator privileges
#

# x64
$registryKey = "Registry::HKLM\SOFTWARE\Wow6432Node\Microsoft\.NETFramework\AssemblyFolders\BitMiracle.Docotic.Pdf"
if (Test-Path $registryKey) {
  Remove-Item $registryKey
}

$registryKey = "Registry::HKLM\SOFTWARE\Wow6432Node\Microsoft\.NETFramework\v4.0.30319\AssemblyFoldersEx\BitMiracle.Docotic.Pdf"
if (Test-Path $registryKey) {
  Remove-Item $registryKey
}

# x64
$registryKey = "Registry::HKLM\SOFTWARE\Microsoft\.NETFramework\AssemblyFolders\BitMiracle.Docotic.Pdf"
if (Test-Path $registryKey) {
  Remove-Item $registryKey
}

$registryKey = "Registry::HKLM\SOFTWARE\Microsoft\.NETFramework\v4.0.30319\AssemblyFoldersEx\BitMiracle.Docotic.Pdf"
if (Test-Path $registryKey) {
  Remove-Item $registryKey
}
