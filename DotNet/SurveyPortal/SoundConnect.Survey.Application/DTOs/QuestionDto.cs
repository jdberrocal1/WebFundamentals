using SoundConnect.Survey.Core.Common;

namespace SoundConnect.Survey.Application.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public SurveyType SurveyType { get; set; }
        public string QuestionText { get; set; }
    }
}
