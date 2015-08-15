del /f /q *.nupkg
nuget pack SerializationLib.nuspec -Prop Configuration=Release
nuget push *.nupkg