using System;

namespace SoundConnect.Survey.Application.DTOs
{
   
    public class SurveyAnswerDto
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public DateTime RespondDate { get; set; }
    }
}
