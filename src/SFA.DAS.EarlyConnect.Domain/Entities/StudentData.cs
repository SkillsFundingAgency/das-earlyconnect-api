﻿namespace SFA.DAS.EarlyConnect.Domain.Entities
{
    public class StudentData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string DataSource { get; set; }
        public string SchoolName { get; set; }
        public string URN { get; set; } = string.Empty;
        public string Postcode { get; set; }
        public string Industry { get; set; }
        public DateTime? DateInterestShown { get; set; }
        public int? LepsId { get; set; }
        public DateTime? LepDateSent { get; set; }
        public DateTime DateAdded { get; set; }
        public int? LogId { get; set; }
        public virtual ECAPILog Log { get; set; }
        public virtual ICollection<StudentSurvey>? StudentSurveys { get; set; }
    }
}
