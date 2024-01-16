﻿using SFA.DAS.EarlyConnect.Application.Models;
using SFA.DAS.EarlyConnect.Domain.Entities;

namespace SFA.DAS.EarlyConnect.Application.Extensions
{
    public static class MappingExtensions
    {
        public static List<StudentAnswer> MapFromAnswersDtoToCreate(this List<AnswerDto> answersDto)
        {
            var answers = new List<StudentAnswer>();

            foreach (AnswerDto dto in answersDto)
            {
                var answer = new StudentAnswer
                {
                    StudentSurveyId = dto.StudentSurveyId,
                    AnswerId = dto.AnswerId,
                    QuestionId = dto.QuestionId,
                    Response = dto.Response
                };

                answers.Add(answer);
            }

            return answers;
        }

        public static List<StudentAnswer> MapFromAnswersDtoToUpdate(this List<AnswerDto> answersDto)
        {
            var answers = new List<StudentAnswer>();

            foreach (AnswerDto dto in answersDto)
            {
                var answer = new StudentAnswer
                {
                    Id = (int)dto.Id,
                    StudentSurveyId = dto.StudentSurveyId,
                    AnswerId = dto.AnswerId,
                    QuestionId = dto.QuestionId,
                    Response = dto.Response
                };

                answers.Add(answer);
            }

            return answers;
        }
    }
}
