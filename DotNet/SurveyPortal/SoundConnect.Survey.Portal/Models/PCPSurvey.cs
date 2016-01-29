using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Portal.Modules;

namespace SoundConnect.Survey.Portal.Models
{
    public class PCPSurvey
    {
        public SurveyDto Survey { get; set; }        
        [QuestionKey("PCP_Q_1")]        
        [RequiredNote("Other", ErrorMessage = "For other has to specify")]
        public Question Question1 { get; set; }        
        [QuestionKey("PCP_Q_2A")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2A { get; set; }
        [QuestionKey("PCP_Q_2B")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2B { get; set; }
        [QuestionKey("PCP_Q_2C")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2C { get; set; }
        [QuestionKey("PCP_Q_2D")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2D { get; set; }
        [QuestionKey("PCP_Q_2E")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2E { get; set; }
        [QuestionKey("PCP_Q_2F")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2F { get; set; }
        [QuestionKey("PCP_Q_2G")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2G { get; set; }
        [QuestionKey("PCP_Q_2H")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question2H { get; set; }
        [QuestionKey("PCP_Q_2WHY")]        
        [RequiredWhyResponse(new string[] { "Question2A", "Question2B", "Question2C", "Question2D", "Question2E", "Question2F",
                        "Question2G", "Question2H"})]
        public Question Question2Why { get; set; }
        [QuestionKey("PCP_Q_3")]        
        public Question Question3 { get; set; }
        [QuestionKey("PCP_Q_4")]        
        public Question Question4 { get; set; }
        [QuestionKey("PCP_Q_5")]        
        public Question Question5 { get; set; }

        public PCPSurvey()
        {
        }

        public PCPSurvey(SurveyDto survey)
        {
            Survey = survey;
        }



    }
}