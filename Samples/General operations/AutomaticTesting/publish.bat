@echo off

SET TEST_APP_DIR=%~dp0\AotCompatibility
dotnet publish "%TEST_APP_DIR%\AotCompatibility.csproj" --no-build /warnaserror /p:PublishProfile="%TEST_APP_DIR%\Properties\PublishProfiles\win-x64.pubxml"