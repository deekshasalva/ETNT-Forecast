using System.Collections.Generic;

namespace Common.Models
{
    public class LookupViewModel
    {
        public IEnumerable<object> Orgs { get; set; }
        public IEnumerable<object> Users { get; set; }
        public IEnumerable<object> Projects { get; set; }
        public IEnumerable<object> Skills { get; set; }
        public IEnumerable<object> Business { get; set; }
        public IEnumerable<object> Capabilities { get; set; }
        public IEnumerable<object> ForecastConfidence { get; set; }
        public IEnumerable<int> FyYears { get; set; }
    }
}