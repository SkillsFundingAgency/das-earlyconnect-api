using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EarlyConnect.Api.Requests;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Api.Mappers
{
    public static class StudentDataMapper
    {
        public static IEnumerable<StudentData> MapFromStudentDataPostRequest(this StudentDataPostRequest request)
        {
            var listOfStudentData = new List<StudentData>();

            foreach (StudentDataDto dto in request.ListOfStudentData) 
            {
                var studentData = new StudentData
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    DateOfBirth = dto.DateOfBirth.Date,
                    Email = dto.Email,
                    Postcode = dto.Postcode,
                    Industry = dto.Industry,
                    DateInterestShown = dto.DateOfInterest
                };

                listOfStudentData.Add(studentData);
            }

            return listOfStudentData;
        }
    }
}
