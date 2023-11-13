namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class MetricsFlag
    {
        public int Id { get; set; }
        public string FlagName { get; set; }
        public string FlagCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual ICollection<ApprenticeMetricsFlagData> MetricsFlagLookups { get; set; } // 1-to-Many with MetricsFlagLookup
    }
}
