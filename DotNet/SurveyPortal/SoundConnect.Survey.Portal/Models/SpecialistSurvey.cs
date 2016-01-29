using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Portal.Modules;

namespace SoundConnect.Survey.Portal.Models
{
    public class SpecialistSurvey
    {
        public SurveyDto Survey { get; set; }
        [QuestionKey("SP_Q_1")]        
        public Question Question1 { get; set; }
        [QuestionKey("SP_Q_2A")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2A { get; set; }
        [QuestionKey("SP_Q_2B")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2B { get; set; }
        [QuestionKey("SP_Q_2C")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2C { get; set; }
        [QuestionKey("SP_Q_2D")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2D { get; set; }
        [QuestionKey("SP_Q_2E")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2E { get; set; }
        [QuestionKey("SP_Q_2F")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2F { get; set; }        
        [QuestionKey("SP_Q_2WHY")]
        [RequiredWhyResponse(new string[] { "Question2A", "Question2B", "Question2C", "Question2D", "Question2E", "Question2F"})]
        public Question Question2Why { get; set; }
        [QuestionKey("SP_Q_3")]        
        public Question Question3 { get; set; }
        [QuestionKey("SP_Q_4")]        
        public Question Question4 { get; set; }
        [QuestionKey("SP_Q_5")]        
        public Question Question5 { get; set; }

        public SpecialistSurvey()
        {
        }

        public SpecialistSurvey(SurveyDto survey)
        {
            Survey = survey;
        }
    }
}