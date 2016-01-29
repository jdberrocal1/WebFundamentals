using SoundConnect.Survey.Application.DTOs;
using System.Collections.Generic;

namespace SoundConnect.Survey.Application.Interfaces
{
    public interface ISurveyAppService
    {        
        SurveyDto GetBySurveyCode(string surveyCode);
        void UpdateComplete(int surveyId);
        HospitalDetailsDto GetHospitalDetailsByName(string hospitalName);
    }
}
