using NunitHTMLReportGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace NunitHTMLReportGenerator
{
    public class XMLFileProcess
    {
        
        #region Processing

        public static TestResult ProcessFile(string file)
        {
            XElement doc = XElement.Load(file);
            TestResult testResult = new TestResult();
            // Load summary values
            testResult.Id = doc.Attribute("id").Value;
            testResult.ProjectName = doc.Attribute("name").Value;
            testResult.TestcaseCount = int.Parse(!string.IsNullOrEmpty(doc.Attribute("testcasecount").Value) ? doc.Attribute("testcasecount").Value : "0");
            testResult.Result = doc.Attribute("result").Value;
            testResult.Time = doc.Attribute("time").Value;
            testResult.TotalTests = int.Parse(!string.IsNullOrEmpty(doc.Attribute("total").Value) ? doc.Attribute("total").Value : "0");
            testResult.TotalTestPassed = int.Parse(!string.IsNullOrEmpty(doc.Attribute("passed").Value) ? doc.Attribute("passed").Value : "0");
            testResult.TotalTestFailed = int.Parse(!string.IsNullOrEmpty(doc.Attribute("failed").Value) ? doc.Attribute("failed").Value : "0");
            testResult.TotalTestInconclusive = int.Parse(!string.IsNullOrEmpty(doc.Attribute("inconclusive").Value) ? doc.Attribute("inconclusive").Value : "0");
            testResult.TotalTestSkipped = int.Parse(!string.IsNullOrEmpty(doc.Attribute("skipped").Value) ? doc.Attribute("skipped").Value : "0");
            testResult.TotalTestAsserts = int.Parse(!string.IsNullOrEmpty(doc.Attribute("asserts").Value) ? doc.Attribute("asserts").Value : "0");
            testResult.Date = DateTime.Parse(string.Format("{0}", doc.Attribute("run-date").Value));
            testResult.TestPlatform = doc.Element("environment").Attribute("platform").Value;


            // Calculate the success rate
            testResult.Percentage = 0;
            if (testResult.TotalTests > 0)
            {
                int failures = testResult.TotalTestFailed + testResult.TotalTestFailed;
                testResult.Percentage = decimal.Round(decimal.Divide(failures, testResult.TotalTests) * 100, 1);
            }

            // Process test fixtures
            testResult.TestFixtures = ProcessFixtures(doc.Descendants("test-suite").Where(x => x.Attribute("type").Value == "TestFixture"));

            return testResult;
        }

        private static IList<TestFixture> ProcessFixtures(IEnumerable<XElement> fixtures)
        {

            IList<TestFixture> testFixtures = new List<TestFixture>();
            // Loop through all of the fixtures
            foreach (var fixture in fixtures)
            {
                TestFixture testFixture = new TestFixture();
                testFixture.Name = fixture.Attribute("name").Value;
                testFixture.TestCaseCount = fixture.Attribute("testcasecount").Value;
                testFixture.Result = fixture.Attribute("result").Value;
                testFixture.Time = fixture.Attribute("time") != null ? fixture.Attribute("time").Value : string.Empty;
                testFixture.Total = fixture.Attribute("total").Value;
                testFixture.Passed = fixture.Attribute("passed").Value;
                testFixture.Failed = fixture.Attribute("failed").Value;
                testFixture.Inconclusive = fixture.Attribute("inconclusive").Value;
                testFixture.Skipped = fixture.Attribute("skipped").Value;
                testFixture.Asserts = fixture.Attribute("asserts").Value;
                testFixture.Reason = fixture.Element("reason") != null ? fixture.Element("reason").Element("message").Value : string.Empty;
                
                // Generate a unique id for the modal dialog
                testFixture.ID = fixture.Attribute("id").Value;

                testFixture.TestCases = ProcessTestcase(fixture.Descendants("test-case"));

                testFixtures.Add(testFixture);
            }

            return testFixtures;
        }

        private static IList<TestCase> ProcessTestcase(IEnumerable<XElement> testcases)
        {
            IList<TestCase> tests = new List<TestCase>();
            // Loop through all of the testcase
            foreach (var testcase in testcases)
            {
                var test = new TestCase();
                test.Id = testcase.Attribute("id").Value;
                test.Name = testcase.Attribute("name").Value;
                test.Result = testcase.Attribute("result") != null ? testcase.Attribute("result").Value : string.Empty;
                test.Time = testcase.Attribute("time") != null ? testcase.Attribute("time").Value : string.Empty;
                test.Asserts = testcase.Attribute("asserts") != null ? testcase.Attribute("asserts").Value : string.Empty;
                test.Message = testcase.Descendants("message").Any() ? testcase.Descendants("message").FirstOrDefault().Value : string.Empty;
                test.StackTrace = testcase.Descendants("stack-trace").Any() ? testcase.Descendants("stack-trace").FirstOrDefault().Value : string.Empty;
                test.Description = testcase.Attribute("description") != null ? testcase.Attribute("description").Value : string.Empty;
                test.Category = testcase.Descendants("category").Any() ? testcase.Descendants("category").FirstOrDefault().Value : string.Empty;
                test.Property = testcase.Descendants("property").Any() ? testcase.Descendants("property").FirstOrDefault().Value : string.Empty;
                test.Reason = testcase.Descendants("reason").Any() ? testcase.Descendants("reason").FirstOrDefault().Value : string.Empty;

                tests.Add(test);
            }

            return tests;
        }
        #endregion Processing


    }
}