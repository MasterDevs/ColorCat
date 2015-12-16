$packageName = 'ColorCat.portable' # arbitrary name for the package, used in messages
$url ="https://github.com/MasterDevs/ColorCat/releases/download/v0.0.2/bin.zip"

try 
{
  $installDir = Join-Path $env:AllUsersProfile "$packageName"
  Write-Host "Adding `'$installDir`' to the path and the current shell path"
  Install-ChocolateyPath "$installDir"
  $env:Path = "$($env:Path);$installDir"

  Install-ChocolateyZipPackage "$packageName" "$url" "$installDir"
} 
catch 
{
  Write-ChocolateyFailure "$packageName" "$($_.Exception.Message)"
  throw
}
