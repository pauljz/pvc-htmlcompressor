pvc.Task("nuget-push", () => {
	// configure NuGet
	PvcNuGet.NuGetExePath = @"C:\Chocolatey\bin\NuGet.bat";
	PvcNuGet.ApiKey = "";

	pvc.Source("src/Pvc.HtmlCompressor.csproj")
	   .Pipe(new PvcNuGetPack(
	       createSymbolsPackage: true
	   ))
	   .Pipe(new PvcNuGetPush());
});