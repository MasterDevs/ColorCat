$packageName = 'ColorCat.portable' # arbitrary name for the package, used in messages
$zipName = "bin.zip"

try 
{
  $installDir = Join-Path $env:AllUsersProfile "$packageName"
  Uninstall-ChocolateyZipPackage "$packageName" "$zipName" 
  Remove-Item -Recurse -Force "$installDir"
} 
catch 
{
  Write-ChocolateyFailure "$packageName" "$($_.Exception.Message)"
  throw
}
