namespace SoundConnect.Survey.Application.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public int SurveyAnswerId { get; set; }
        public int QuestionId { get; set; }
        public string ResponseText { get; set; }
        public string Note { get; set; }
    }
}
