using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Portal.Modules;

namespace SoundConnect.Survey.Portal.Models
{
    public class CareTeamSurvey
    {

        public SurveyDto Survey { get; set; }
        [QuestionKey("CT_Q_1")]        
        public Question Question1 { get; set; }
        [QuestionKey("CT_Q_2")]        
        public Question Question2 { get; set; }
        [QuestionKey("CT_Q_3A")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3A { get; set; }
        [QuestionKey("CT_Q_3B")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3B { get; set; }
        [QuestionKey("CT_Q_3C")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3C { get; set; }
        [QuestionKey("CT_Q_3D")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3D { get; set; }
        [QuestionKey("CT_Q_3E")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3E { get; set; }
        [QuestionKey("CT_Q_3F")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3F { get; set; }
        [QuestionKey("CT_Q_3G")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3G { get; set; }
        [QuestionKey("CT_Q_3H")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3H { get; set; }
        [QuestionKey("CT_Q_3I")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3I { get; set; }
        [QuestionKey("CT_Q_3J")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3J { get; set; }
        [QuestionKey("CT_Q_3K")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3K { get; set; }
        [QuestionKey("CT_Q_3L")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3L { get; set; }
        [QuestionKey("CT_Q_3M")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3M { get; set; }
        [QuestionKey("CT_Q_3N")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3N { get; set; }
        [QuestionKey("CT_Q_3O")]
        [RequiredResponse(ErrorMessage = "This question requires an answer")]
        public Question Question3O { get; set; }
        [QuestionKey("CT_Q_3WHY")]
        [RequiredWhyResponseAttribute(new string[] { "Question3A", "Question3B", "Question3C", "Question3D", "Question3E", "Question3F", 
                        "Question3G", "Question3H", "Question3I", "Question3J", "Question3K", "Question3L", "Question3M", "Question3N", "Question3O"})]
        public Question Question3Why { get; set; }
        [QuestionKey("CT_Q_4")]        
        public Question Question4 { get; set; }
        [QuestionKey("CT_Q_5")]        
        public Question Question5 { get; set; }
        [QuestionKey("CT_Q_6")]        
        public Question Question6 { get; set; }

        public CareTeamSurvey()
        {
        }

        public CareTeamSurvey(SurveyDto survey)
        {
            Survey = survey;
        }
    }
}