using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class MetricsFlagLookup
    {
        public int Id { get; set; }
        public int MetricsId { get; set; } // FK to ApprenticeMetricsData
        public ApprenticeMetricsData MetricsData { get; set; } // Naviation Property to ApprenticeMetricsData
        public int FlagId { get; set; } // FK to ApprenticeMetricsFlag
        public ApprenticeMetricsFlag MetricsFlag { get; set; } // Naviation Property to ApprenticeMetricsFlag
        public bool FlagValue { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
