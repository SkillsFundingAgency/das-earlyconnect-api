namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class ApprenticeMetricsData
    {
        public int Id { get; set; }
        public int LogId { get; set; } // FK to ECAPILog
        public ECAPILog Log { get; set; } // Navigation Property to ECAPILog
        public int LEPSId { get; set; } // FK to LEPSData
        public decimal IntendedStartYear { get; set; }
        public int MaxTravelInMiles { get; set; }
        public bool WillingnessToRelocate { get; set; }
        public int NoOfGCSCs { get; set; }
        public int NoOfStudents { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<MetricsFlagLookup>? MetricsFlagLookups { get; set; } // 1-to-Many with MetricsFlagLookup
    }
}
