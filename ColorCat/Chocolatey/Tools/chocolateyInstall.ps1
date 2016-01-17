$packageName = 'ColorCat.portable' # name for the package, used in messages
$url ="https://github.com/MasterDevs/ColorCat/releases/download/v0.0.4/bin.zip"

  $installDir = Join-Path $env:AllUsersProfile "$packageName"
  Write-Host "Adding `'$installDir`' to the path and the current shell path"
  Install-ChocolateyPath "$installDir"
  $env:Path = "$($env:Path);$installDir"

  Install-ChocolateyZipPackage "$packageName" "$url" "$installDir"
