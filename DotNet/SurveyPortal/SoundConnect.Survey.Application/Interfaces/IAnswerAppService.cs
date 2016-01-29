using SoundConnect.Survey.Application.DTOs;
using System.Collections.Generic;

namespace SoundConnect.Survey.Application.Interfaces
{
    public interface IAnswerAppService
    {
        ICollection<AnswerDto> GetBySurveyId(int surveyId);
        void Insert(ICollection<AnswerDto> answersDto);
    }
}
