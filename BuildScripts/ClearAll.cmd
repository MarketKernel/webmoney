echo off

echo ========== START ==========

rd /s /q .\..\packages

rd /s /q .\..\TestResults

rd /s /q .\..\WebMoney.Cryptography\bin
rd /s /q .\..\WebMoney.Cryptography\obj

rd /s /q .\..\WebMoney.Cryptography.Tests\bin
rd /s /q .\..\WebMoney.Cryptography.Tests\obj

rd /s /q .\..\WebMoney.KeyExtractor\bin
rd /s /q .\..\WebMoney.KeyExtractor\obj

rd /s /q .\..\WebMoney.XmlInterfaces\bin
rd /s /q .\..\WebMoney.XmlInterfaces\obj

rd /s /q .\..\WebMoney.XmlInterfaces.SampleApp\bin
rd /s /q .\..\WebMoney.XmlInterfaces.SampleApp\obj

rd /s /q .\..\WebMoney.XmlInterfaces.Sandbox\bin
rd /s /q .\..\WebMoney.XmlInterfaces.Sandbox\obj

echo ========== DONE ==========
pause