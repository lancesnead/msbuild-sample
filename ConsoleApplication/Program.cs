﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.Build.Locator;
using Microsoft.Build.Logging;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Add a reference to the Microsoft.Build.Locator package, and register the default Visual Studio MSBuild.
            MSBuildLocator.RegisterDefaults();

            BuildProject();

            Console.ReadLine();
        }

        private static void BuildProject()
        {
            var pc = new ProjectCollection();
            var path = Directory.CreateDirectory(Path.GetTempPath() + Guid.NewGuid().ToString("N") + "\\");
            var props = new Dictionary<string, string>
            {
                {"Configuration", "Debug"},
                {"Platform", "AnyCPU"},
                {"OutputPath", path.FullName}
            };
            var buildParams = new BuildParameters(pc)
            {
                DetailedSummary = true,
                Loggers = new List<ILogger> {new ConsoleLogger()},
                DefaultToolsVersion = "15.0",
                
            };
            var targets = new List<string> {"PrepareForBuild", "Clean", "Build", "Publish"};
            var reqData = new BuildRequestData(GetProjectPath(), props, "15.0", targets.ToArray(), null);
            try
            {
                Log("Starting MSBuild build");
                BuildManager.DefaultBuildManager.BeginBuild(buildParams);
                var buildResult = BuildManager.DefaultBuildManager.BuildRequest(reqData);
                Log($"MSBuild build complete: {buildResult.OverallResult}");
            }
            catch (InvalidCastException ex)
            {
                Log(ex.Message);
                throw;
            }
        }
       
        private static string GetProjectPath()
        {
            var segments = Environment.CurrentDirectory.Split('\\').TakeWhile(s => s != "ConsoleApplication").ToList();
            segments.Add("WpfApplication");
            segments.Add("WpfApplication.csproj");
            var projectPath = $"C:\\{Path.Combine(segments.Skip(1).ToArray())}";
            return projectPath;
        }

        [DebuggerStepThrough]
        private static void Log(string s)
        {
            Console.WriteLine(s);
        }
    }
}
