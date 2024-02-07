namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class SubjectPreferenceData
    {
        public int Id { get; set; }
        public int LogId { get; set; }
        public int LEPSId { get; set; }
        public decimal IntendedStartYear { get; set; }
        public string SubjectPreference { get; set; }
        public int NoOfStudents { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LepDateSent { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
