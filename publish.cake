
var target = Argument("target", "Default");

Task("PublishGithub")
  .IsDependentOn("Pack")
  .Does(context => {
  if (BuildSystem.GitHubActions.IsRunningOnGitHubActions)
   {
      foreach(var file in GetFiles("./artifacts/*.nupkg"))
      {
        Information("Publishing {0}...", file.GetFilename().FullPath);
        DotNetNuGetPush(file, new DotNetNuGetPushSettings {
              ApiKey = EnvironmentVariable("GITHUB_TOKEN"),
              Source = "https://nuget.pkg.github.com/geekiam/index.json",
              SkipDuplicate = true
        });
      } 
   } 
 }); 
Task("PublishNuget")
 .IsDependentOn("Pack")
 .Does(context => {
   if (BuildSystem.GitHubActions.IsRunningOnGitHubActions)
   {
     foreach(var file in GetFiles("./.artifacts/*.nupkg"))
     {
       Information("Publishing {0}...", file.GetFilename().FullPath);
       DotNetNuGetPush(file, new DotNetNuGetPushSettings {
          ApiKey = context.EnvironmentVariable("NUGET_API_KEY"),
          Source = "https://api.nuget.org/v3/index.json",
          SkipDuplicate = true
       });
     }
   }
 }); 
 
 Task("Default")
        .IsDependentOn("PublishGithub")
        .IsDependentOn("PublishNuget");
        
 RunTarget(target);