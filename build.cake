#addin "nuget:?package=Polly&version=4.2.0"
#tool "nuget:?package=xunit.runner.console&version=2.1.0"

using System;
using Polly;

var target = Argument("target", "Default");
var platform = Argument("platform", "AnyCPU");
var configuration = Argument("configuration", "Release");
var isPlatformAnyCPU = StringComparer.OrdinalIgnoreCase.Equals(platform, "AnyCPU");
var isPlatformX86 = StringComparer.OrdinalIgnoreCase.Equals(platform, "x86");
var isPlatformX64 = StringComparer.OrdinalIgnoreCase.Equals(platform, "x64");
var MSBuildSolution = "./Core2D.sln";
var XBuildSolution = "./Core2D.mono.sln";
var dirSuffix = platform + "/" + configuration;
var dirSuffixSkia = (isPlatformAnyCPU ? "x86" : platform) + "/" + configuration;
Func<IFileSystemInfo, bool> ExcludeSkia = i => !(i.Path.FullPath.IndexOf("Skia", StringComparison.OrdinalIgnoreCase) >= 0);
Func<string, DirectoryPathCollection> GetSkiaDirectories = pattern => GetDirectories(pattern) - GetDirectories(pattern, ExcludeSkia);
var buildDirs = 
    GetDirectories("./src/**/bin/" + dirSuffix, ExcludeSkia) + 
    GetSkiaDirectories("./src/**/bin/" + dirSuffixSkia) +
    GetDirectories("./src/**/obj/" + dirSuffix, ExcludeSkia) + 
    GetSkiaDirectories("./src/**/obj/" + dirSuffixSkia) + 
    GetDirectories("./dependencies/**/bin/" + dirSuffix, ExcludeSkia) + 
    GetSkiaDirectories("./dependencies/**/bin/" + dirSuffixSkia) + 
    GetDirectories("./dependencies/**/obj/" + dirSuffix, ExcludeSkia) + 
    GetSkiaDirectories("./dependencies/**/obj/" + dirSuffixSkia) + 
    GetDirectories("./tests/**/bin/" + dirSuffix, ExcludeSkia) + 
    GetSkiaDirectories("./tests/**/bin/" + dirSuffixSkia) + 
    GetDirectories("./tests/**/obj/" + dirSuffix, ExcludeSkia) + 
    GetSkiaDirectories("./tests/**/obj/" + dirSuffixSkia);
var testResultsDir = (DirectoryPath)Directory("./test-results");		

Task("Clean")
    .Does(() =>
{
    CleanDirectories(buildDirs);
    CleanDirectory(testResultsDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var maxRetryCount = 5;
    var toolTimeout = 1d;
    Policy
        .Handle<Exception>()
        .Retry(maxRetryCount, (exception, retryCount, context) => {
            if (retryCount == maxRetryCount)
            {
                throw exception;
            }
            else
            {
                Verbose("{0}", exception);
                toolTimeout+=0.5;
            }})
        .Execute(()=> {
            if(IsRunningOnWindows())
            {
                NuGetRestore(MSBuildSolution, new NuGetRestoreSettings {
                    ToolTimeout = TimeSpan.FromMinutes(toolTimeout)
                });
            }
            if(IsRunningOnUnix())
            {
                NuGetRestore(XBuildSolution, new NuGetRestoreSettings {
                    ToolTimeout = TimeSpan.FromMinutes(toolTimeout)
                });
            }
        });
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        MSBuild(MSBuildSolution, settings => {
            settings.SetConfiguration(configuration);
            settings.WithProperty("Platform", platform);
            settings.SetVerbosity(Verbosity.Minimal);
        });
    }
    if(IsRunningOnUnix())
    {
        XBuild(XBuildSolution, settings => {
            settings.SetConfiguration(configuration);
            settings.WithProperty("Platform", platform);
            settings.SetVerbosity(Verbosity.Minimal);
        });
    }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    string pattern = "./tests/**/bin/" + platform + "/" + configuration + "/*.UnitTests.dll";
    if (isPlatformAnyCPU || isPlatformX86)
    {
        XUnit2(pattern, new XUnit2Settings { 
            ToolPath = "./tools/xunit.runner.console/tools/xunit.console.x86.exe",
            OutputDirectory = testResultsDir,
            XmlReportV1 = true,
            NoAppDomain = true
        });
    }
    else if (isPlatformX64)
    {
        XUnit2(pattern, new XUnit2Settings { 
            ToolPath = "./tools/xunit.runner.console/tools/xunit.console.exe",
            OutputDirectory = testResultsDir,
            XmlReportV1 = true,
            NoAppDomain = true
        });
    }
    else
    {
        throw new PlatformNotSupportedException("Not supported XUnit platform.");
    }
});

Task("Default")
  .IsDependentOn("Run-Unit-Tests");

Task("AppVeyor")
  .IsDependentOn("Run-Unit-Tests");

Task("Travis")
  .IsDependentOn("Run-Unit-Tests");

RunTarget(target);
