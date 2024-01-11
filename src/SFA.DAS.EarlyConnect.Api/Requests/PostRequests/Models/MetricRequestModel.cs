using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.EarlyConnect.Api.Requests.PostRequests.Models
{
    public class MetricRequestModel
    {
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Invalid Region")]
        public string Region { get; set; }
        public decimal IntendedStartYear { get; set; }
        public int MaxTravelInMiles { get; set; }
        public bool WillingnessToRelocate { get; set; }
        public int NoOfGCSCs { get; set; }
        public int NoOfStudents { get; set; }
        public int LogId { get; set; }
        public IList<string> MetricFlags { get; set; }
    }
}
