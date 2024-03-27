var target = Argument("target", "Default");
var GITHUB_TOKEN = Argument("GITHUB_TOKEN", "");
var NUGET_API_KEY = Argument("NUGET_API_KEY", "");

Task("Github")
  .Does(context => {
  if (BuildSystem.GitHubActions.IsRunningOnGitHubActions)
   {
      foreach(var file in GetFiles("./artifacts/nuget-packages/*.nupkg"))
      {
        Information("Publishing {0}...", file.GetFilename().FullPath);
        DotNetNuGetPush(file, new DotNetNuGetPushSettings {
              ApiKey = GITHUB_TOKEN,
              Source = "https://nuget.pkg.github.com/nostrfi/index.json",
              SkipDuplicate = true
        });
      } 
   } 
 }); 
 
Task("Nuget")
 .IsDependentOn("Github")
 .Does(context => {
   if (BuildSystem.GitHubActions.IsRunningOnGitHubActions)
   {
     foreach(var file in GetFiles("./artifacts/nuget-packages/*.nupkg"))
     {
       Information("Publishing {0}...", file.GetFilename().FullPath);
       DotNetNuGetPush(file, new DotNetNuGetPushSettings {
          ApiKey = NUGET_API_KEY,
          Source = "https://api.nuget.org/v3/index.json",
          SkipDuplicate = true
       });
     }
   }
 }); 
 
 Task("Default")
        .IsDependentOn("Github")
        .IsDependentOn("Nuget");
 RunTarget(target);