using System;
using System.Collections.Generic;
using System.Text;

namespace NunitHTMLReportGenerator.Model
{
    public class TestCase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Result { get; set; }
        public string Time { get; set; }
        public string Asserts { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Property { get; set; }
        public string Reason { get; set; }
    }
}