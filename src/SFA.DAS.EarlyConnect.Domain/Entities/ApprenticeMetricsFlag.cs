using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class ApprenticeMetricsFlag
    {
        public int Id { get; set; }
        public string FlagName { get; set; }
        public string FlagCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<MetricsFlagLookup> MetricsFlagLookups { get; set; } // 1-to-Many with MetricsFlagLookup
    }
}
