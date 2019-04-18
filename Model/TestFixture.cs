using System;
using System.Collections.Generic;

namespace NunitHTMLReportGenerator.Model
{
    public class TestFixture
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string TestCaseCount { get; set; }
        public string Result { get; set; }
        public string Time { get; set; }
        public string Total { get; set; }
        public string Passed { get; set; }
        public string Failed { get; set; }
        public string Inconclusive { get; set; }
        public string Skipped { get; set; }
        public string Asserts { get; set; }
        public string Reason { get; set; }
        public IList<TestCase> TestCases { get; set; }
    }
}