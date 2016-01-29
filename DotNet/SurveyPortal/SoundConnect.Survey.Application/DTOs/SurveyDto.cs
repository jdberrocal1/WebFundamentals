using SoundConnect.Survey.Core.Common;

namespace SoundConnect.Survey.Application.DTOs
{
    public class SurveyDto
    {
        public int Id { get; set; }
        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }
        public string FacilityName { get; set; }
        public string SurveyCode { get; set; }
        public SurveyType SurveyType { get; set; }                
        public bool IsComplete { get; set; }
        public bool IsExpired { get; set; }
    }
}
