using Newtonsoft.Json;
using NunitHTMLReportGenerator.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NunitHTMLReportGenerator
{
    public class GenerateOutput
    {
        public static string GenerateHTML(TestResult result)
        {
            string html;
            StreamReader sr = new StreamReader(@"Template\HTMLTemplate.html");
            html = sr.ReadToEnd();

            #region Write the test result
            html = html.Replace("##ProjectName##", result.ProjectName);
            html = html.Replace("##TestcaseCount##", result.TestcaseCount.ToString());
            html = html.Replace("##Result##", result.Result.ToString());//
            html = html.Replace("##Time##", result.Time.ToString());
            html = html.Replace("##Tests##", result.TotalTests.ToString());
            html = html.Replace("##TotalTestPassed##", result.TotalTestPassed.ToString());
            html = html.Replace("##Failed##", result.TotalTestFailed.ToString());
            html = html.Replace("##Inconclusive##", result.TotalTestInconclusive.ToString());
            html = html.Replace("##Skipped##", result.TotalTestSkipped.ToString());
            html = html.Replace("##Asserts##", result.TotalTestAsserts.ToString());
            html = html.Replace("##Date##", result.Date.ToString());
            html = html.Replace("##Platform##", result.TestPlatform.ToString());
            html = html.Replace("##SuccessRate##", result.Percentage.ToString());

         
            
            
            
            #endregion

            #region Write the test fixture and test data
            string json = JsonConvert.SerializeObject(result.TestFixtures);
            html = html.Replace("##dataInJson##", json);
            return html;
            #endregion
            
        }

    }
}
