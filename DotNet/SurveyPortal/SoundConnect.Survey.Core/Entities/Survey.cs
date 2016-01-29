using CodeFirstStoredProcs;
using System;

namespace SoundConnect.Survey.Core.Entities
{
    public class Survey
    {

        [StoredProcAttributes.Name("survey_id")]
        public int Id { get; set;}
        [StoredProcAttributes.Name("provider_first_name")]
        public string ProviderFirstName { get; set; }
        [StoredProcAttributes.Name("provider_last_name")]
        public string ProviderLastName { get; set; }
        [StoredProcAttributes.Name("facility_name")]
        public string FacilityName { get; set; }
        [StoredProcAttributes.Name("survey_code")]
        public string SurveyCode { get; set; }
        [StoredProcAttributes.Name("survey_type")]
        public int SurveyType { get; set; }        
        [StoredProcAttributes.Name("is_complete")]        
        public bool IsComplete { get; set; }
        [StoredProcAttributes.Name("is_expired")]
        public bool IsExpired { get; set; }
    }
}
