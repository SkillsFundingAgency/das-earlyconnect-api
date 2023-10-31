using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class ECAPILog
    {
        public int Id { get; set; }
        public string RequestType { get; set; }
        public string RequestSource { get; set; }
        public string RequestIP { get; set; }
        public string Payload { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<StudentData> StudentData { get; set; }
        public ICollection<ApprenticeMetricsData> ApprenticeMetricsData { get; set; }
    }
}
