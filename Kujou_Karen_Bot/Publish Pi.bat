dotnet clean .
pause
dotnet restore .
pause
dotnet build .
pause
dotnet publish . -r ubuntu.16.04-arm
pause