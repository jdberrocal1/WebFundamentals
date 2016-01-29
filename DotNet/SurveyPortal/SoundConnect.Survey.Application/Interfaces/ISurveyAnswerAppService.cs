using SoundConnect.Survey.Application.DTOs;

namespace SoundConnect.Survey.Application.Interfaces
{
    public interface ISurveyAnswerAppService
    {
        SurveyAnswerDto Insert(SurveyAnswerDto surveyAnswersDto);
    }
}
