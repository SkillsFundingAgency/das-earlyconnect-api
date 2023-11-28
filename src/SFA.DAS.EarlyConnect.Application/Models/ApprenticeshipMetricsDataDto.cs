namespace SFA.DAS.EarlyConnect.Application.Models
{
    public class ApprenticeshipMetricsDataDto
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public decimal IntendedStartYear { get; set; }
        public int MaxTravelInMiles { get; set; }
        public bool WillingnessToRelocate { get; set; }
        public int NoOfGCSCs { get; set; }
        public int NoOfStudents { get; set; }
        public int LogId { get; set; }
        public DateTime DateAdded { get; set; }
        public IList<MetricsFlagDto> InterestAreas { get; set; }
    }
}
