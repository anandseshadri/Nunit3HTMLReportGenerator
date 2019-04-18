using Newtonsoft.Json;
using NunitHTMLReportGenerator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Configuration;
using System.Linq;

namespace NunitHTMLReportGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string xmlFolderpath, outputFolderPath;
            // Checking if XML folder and output location is being passed as arguments
            if ( args.Length != 0)
            {
                xmlFolderpath = args[0];
                outputFolderPath = args[1];
            }
            else
            {
                xmlFolderpath = ConfigurationManager.AppSettings["xmlFilepath"];
                outputFolderPath = ConfigurationManager.AppSettings["outputPath"];
            }
            //Getting the latest XML file from folder
            var directory = new DirectoryInfo(xmlFolderpath);
            var myFile = directory.GetFiles("*.xml").OrderByDescending(f => f.LastWriteTime).First().FullName;

            TestResult testResult = XMLFileProcess.ProcessFile(myFile);
            var html =  GenerateOutput.GenerateHTML(testResult);
            
            // GUIDs for filename.
            Guid fileName = Guid.NewGuid();
            if(!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }
            File.WriteAllText($"{outputFolderPath}\\{fileName}.html", html);
        }
    }
}