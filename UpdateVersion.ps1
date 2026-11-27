$oldVersion = "10.0.20738-dev"
$newVersion = "10.0.20871-dev"

Get-ChildItem -Recurse -Include *.csproj, *.vbproj | ForEach-Object {
    (Get-Content $_.FullName -Raw).Replace($oldVersion, $newVersion) | Set-Content $_.FullName -Encoding UTF8 -NoNewline
}

