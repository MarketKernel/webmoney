echo off

echo ========== START ==========

nuget pack .\..\WebMoney.XmlInterfaces\WebMoney.XmlInterfaces.csproj -Build -Properties Configuration=Release -OutputDirectory .\..\NUPKG

echo ========== DONE ==========
pause