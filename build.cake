#addin nuget:?package=Cake.Coverlet&version=4.0.1
#tool dotnet:?package=GitVersion.Tool&version=5.12.0
#tool dotnet:?package=dotnet-reportgenerator-globaltool&version=5.2.4

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

const string TEST_COVERAGE_OUTPUT_DIR = "coverage";
var solution = "Database.sln";
Task("Clean")
    .Does(() => {
 
    if (BuildSystem.GitHubActions.IsRunningOnGitHubActions)
    {
      Information("Nothing to clean GitHubActions.");
    }
    else
    {
        CleanDirectories("./coverage");
        CleanDirectories("./artifacts");
        GetFiles("./**/**/*.csproj").ToList().ForEach(project => {
               Information($"Cleaning: { project.ToString() }");
               DotNetClean(project.ToString());
             });  
    }
});

Task("Restore")
    .IsDependentOn("Clean")
    .Description("Restoring the solution dependencies")
    .Does(() => {
    
    Information("Restoring the solution dependencies");
      var settings =  new DotNetRestoreSettings
        {
          Verbosity = DotNetVerbosity.Minimal,
          Sources = new [] { "https://api.nuget.org/v3/index.json" }
        };
        
   GetFiles("./**/**/*.csproj").ToList().ForEach(project => {
       Information($"Restoring { project.ToString() }");
       DotNetRestore(project.ToString(), settings);
     });
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
    
     var version = GitVersion(new GitVersionSettings {
        });
     var buildSettings = new DotNetBuildSettings {
                        Configuration = configuration,
                        MSBuildSettings = new DotNetMSBuildSettings()
                                                      .WithProperty("Version", version.AssemblySemVer)
                                                      .WithProperty("AssemblyVersion", version.AssemblySemVer)
                                                      .WithProperty("FileVersion", version.AssemblySemVer)
                       };
                       
    // Build the source project                   
     var projects = GetFiles("./src/**/*.csproj");
     foreach(var project in projects )
     {
         Information($"Building Source: {project.ToString()}");
         DotNetBuild(project.ToString(),buildSettings);
     }
     
     // Build the test projects
      var testProjects = GetFiles("./tests/**/*.csproj");
      foreach(var project in testProjects )
      {
        Information($"Building Test: {project.ToString()}");
       DotNetBuild(project.ToString(),buildSettings);
      }
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
       
       var testSettings = new DotNetTestSettings  {
                 Configuration = configuration,
                 NoBuild = true,
       };
        var coverageOutput = Directory(TEST_COVERAGE_OUTPUT_DIR);             
     
       GetFiles("./tests/**/*.csproj").ToList().ForEach(project => {
          Information($"Code Coverage : { project.ToString() }");
            
          var codeCoverageOutputName = $"{project.GetFilenameWithoutExtension()}.cobertura.xml";
          var coverletSettings = new CoverletSettings {
              CollectCoverage = true,
               CoverletOutputFormat = CoverletOutputFormat.cobertura,
               CoverletOutputDirectory =  coverageOutput,
               CoverletOutputName =codeCoverageOutputName,
               ArgumentCustomization = args => args.Append($"--logger trx"),
               ExcludeByFile = ["**/*Migrations/*.cs"]
          };
                  
          Information($"Running Tests : { project.ToString()}");
          DotNetTest(project.ToString(), testSettings, coverletSettings );        
        });
     
      Information($"Directory Path : { coverageOutput.ToString()}");
          
      var glob = new GlobPattern($"./{ coverageOutput}/*.cobertura.xml");
         
      Information($"Glob Pattern : { glob.ToString()}");
      var outputDirectory = Directory("./coverage/reports");
     
      var reportSettings = new ReportGeneratorSettings
      {
         ArgumentCustomization = args => args.Append($"-reportTypes:HtmlInline_AzurePipelines_Dark;Cobertura")
      };
         
      ReportGenerator(glob, outputDirectory, reportSettings);
          
         var summaryDirectory = Directory("./coverage/summary");
        var summarySettings = new ReportGeneratorSettings
        {
           ArgumentCustomization = args => args.Append($"-reportTypes:MarkdownSummaryGithub")
        };
        ReportGenerator(glob, summaryDirectory, summarySettings);
});

Task("Pack")
 .IsDependentOn("Test")
 .Does(() => {
 
   var version = GitVersion(new GitVersionSettings {
         
         });
   var settings = new DotNetPackSettings
    {
        Configuration = configuration,
        OutputDirectory = "./artifacts",
        NoBuild = true,
        NoRestore = true,
        MSBuildSettings = new DotNetMSBuildSettings()
                        .WithProperty("PackageVersion", version.NuGetVersionV2)
                        .WithProperty("Copyright", $"© Copyright nostrfi.net {DateTime.Now.Year}")
                        .WithProperty("Version", version.NuGetVersionV2)
    }; 
    
     DotNetPack(solution, settings);
     Information($"Packed : { solution }");
 });

Task("Default")
       .IsDependentOn("Clean")
       .IsDependentOn("Restore")
       .IsDependentOn("Build")
       .IsDependentOn("Test")
       .IsDependentOn("Pack");
       
RunTarget(target);