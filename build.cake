#addin nuget:?package=Cake.Coverlet&version=4.0.1
#tool dotnet:?package=GitVersion.Tool&version=5.12.0
#tool dotnet:?package=dotnet-reportgenerator-globaltool&version=5.2.2

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
string version = String.Empty;
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
        DotNetClean(solution);
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
Task("Version")
    .Does(() => {
   var result = GitVersion(new GitVersionSettings {
        UpdateAssemblyInfo = true
    });
    
    version = result.NuGetVersionV2;
    Information($"Version: { version }");
});

Task("Build")
    .IsDependentOn("Version")
    .Does(() => {
     var buildSettings = new DotNetBuildSettings {
                        Configuration = configuration,
                        MSBuildSettings = new DotNetMSBuildSettings()
                                                      .WithProperty("Version", version)
                                                      .WithProperty("AssemblyVersion", version)
                                                      .WithProperty("FileVersion", version)
                       };
     var projects = GetFiles("./**/**/*.csproj");
     foreach(var project in projects )
     {
         Information($"Building {project.ToString()}");
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
          Information($"Testing Project : { project.ToString() }");
            
          var codeCoverageOutputName = $"{project.GetFilenameWithoutExtension()}.cobertura.xml";
          var coverletSettings = new CoverletSettings {
              CollectCoverage = true,
               CoverletOutputFormat = CoverletOutputFormat.cobertura,
               CoverletOutputDirectory =  coverageOutput,
               CoverletOutputName =codeCoverageOutputName,
               ArgumentCustomization = args => args.Append($"--logger trx")
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
 
   var settings = new DotNetPackSettings
    {
        Configuration = configuration,
        OutputDirectory = "./artifacts",
        NoBuild = true,
        NoRestore = true,
        MSBuildSettings = new DotNetMSBuildSettings()
                        .WithProperty("PackageVersion", version)
                        .WithProperty("Copyright", $"© Copyright nostrfi.net {DateTime.Now.Year}")
                        .WithProperty("Version", version)
    }; 
    DotNetPack(solution, settings);
 });

Task("Default")
       .IsDependentOn("Clean")
       .IsDependentOn("Restore")
       .IsDependentOn("Build")
       .IsDependentOn("Test")
       .IsDependentOn("Pack");
RunTarget(target);