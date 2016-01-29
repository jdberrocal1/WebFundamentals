using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Core.Common;
using System.Collections.Generic;

namespace SoundConnect.Survey.Application.Interfaces
{
    public interface IQuestionAppService
    {        
        ICollection<QuestionDto> GetBySurveyType(SurveyType surveyType);
    }
}
