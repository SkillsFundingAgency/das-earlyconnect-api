namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class ApprenticeMetricsFlagData
    {
        public int Id { get; set; }
        public int MetricsId { get; set; } // FK to ApprenticeMetricsData
        public virtual ApprenticeMetricsData MetricsData { get; set; } // Naviation Property to ApprenticeMetricsData
        public int FlagId { get; set; } // FK to ApprenticeMetricsFlag
        public virtual MetricsFlag MetricsFlag { get; set; } // Naviation Property to ApprenticeMetricsFlag
        public bool FlagValue { get; set; }
        public bool IsDeleted { get; set; }
    }
}
