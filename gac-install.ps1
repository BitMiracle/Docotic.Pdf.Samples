#
# Must be executed with Administrator privileges
#

$myDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# x64
$registryKey = "Registry::HKLM\SOFTWARE\Wow6432Node\Microsoft\.NETFramework\AssemblyFolders\BitMiracle.Docotic.Pdf"
New-Item $registryKey -Force | Out-Null
New-ItemProperty -Path $registryKey -Name '(Default)' -Value $myDir -Force | Out-Null

$registryKey = "Registry::HKLM\SOFTWARE\Wow6432Node\Microsoft\.NETFramework\v4.0.30319\AssemblyFoldersEx\BitMiracle.Docotic.Pdf"
New-Item $registryKey -Force | Out-Null
New-ItemProperty -Path $registryKey -Name '(Default)' -Value $myDir -Force | Out-Null

# win32
$registryKey = "Registry::HKLM\SOFTWARE\Microsoft\.NETFramework\AssemblyFolders\BitMiracle.Docotic.Pdf"
New-Item $registryKey -Force | Out-Null
New-ItemProperty -Path $registryKey -Name '(Default)' -Value $myDir -Force | Out-Null

$registryKey = "Registry::HKLM\SOFTWARE\Microsoft\.NETFramework\v4.0.30319\AssemblyFoldersEx\BitMiracle.Docotic.Pdf"
New-Item $registryKey -Force | Out-Null
New-ItemProperty -Path $registryKey -Name '(Default)' -Value $myDir -Force | Out-Null
