namespace SFA.DAS.EarlyConnect.Data.Models
{
    public class ApprenticeshipMetricsDataDto
    {
        public decimal IntendedStartYear { get; set; }
        public int MaxTravelInMiles { get; set; }
        public bool WillingnessToRelocate { get; set; }
        public int NoOfGCSCs { get; set; }
        public int NoOfStudents { get; set; }
        public int LogId { get; set; }
        public IList<string>? MetricFlags { get; set; }
    }
}
