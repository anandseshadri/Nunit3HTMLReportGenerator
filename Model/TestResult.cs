using System;
using System.Collections.Generic;
using System.Text;

namespace NunitHTMLReportGenerator.Model
{
    public class TestResult
    {
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public int TestcaseCount { get; set; }
        public string Result { get; set; }
        public string Time { get; set; }
        public int TotalTests { get; set; }
        public int TotalTestPassed { get; set; }
        public int TotalTestFailed { get; set; }
        public int TotalTestInconclusive { get; set; }
        public int TotalTestSkipped { get; set; }

        public int TotalTestAsserts { get; set; }

        public DateTime Date { get; set; }
        public string TestPlatform { get; set; }
      
        // Calculate the success rate
        public decimal Percentage { get; set; }

        

        public IList<TestFixture> TestFixtures { get; set; }
    }
}