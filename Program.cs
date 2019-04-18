using Newtonsoft.Json;
using NunitHTMLReportGenerator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Configuration;

namespace NunitHTMLReportGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string xmlFilepath, outputPath;
            // Checking if input file and output location is being passed 
            if ( args.Length != 0)
            {
                xmlFilepath = args[0];
                outputPath = args[1];
            }
            else
            {
                xmlFilepath = ConfigurationManager.AppSettings["xmlFilepath"];
                outputPath = ConfigurationManager.AppSettings["outputPath"];
            }
            TestResult testResult = XMLFileProcess.ProcessFile(xmlFilepath);
            var html =  GenerateOutput.GenerateHTML(testResult);
            
            // GUIDs for filename.
            Guid fileName = Guid.NewGuid();
            if(!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            File.WriteAllText($"{outputPath}\\{fileName}.html", html);
        }
    }
}