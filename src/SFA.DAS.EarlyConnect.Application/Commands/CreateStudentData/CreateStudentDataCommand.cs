﻿using MediatR;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Application.Commands.CreateStudentData
{
    public class CreateStudentDataCommand : IRequest<Unit>
    {
        public IEnumerable<StudentData> StudentDataList { get; set; }
    }
}