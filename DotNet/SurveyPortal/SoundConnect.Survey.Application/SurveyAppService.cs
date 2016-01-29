using AutoMapper;
using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Repositories;
using System.Collections.Generic;

namespace SoundConnect.Survey.Application
{
    public class SurveyAppService : ISurveyAppService
    {
        private readonly ISurveyRepository _repository;
       
        
        public SurveyAppService(ISurveyRepository context)
        {
            _repository = context;
        }
        
        public SurveyDto GetBySurveyCode(string surveyCode)
        {
            if (!string.IsNullOrEmpty(surveyCode))
            {
                var result = _repository.GetBySurveyCode(surveyCode);

                return Mapper.Map<SurveyDto>(result);
            }

            return null;
        }

        public void UpdateComplete(int surveyId)
        {
            _repository.UpdateCompleteSurvey(surveyId);
        }

        public HospitalDetailsDto GetHospitalDetailsByName(string hospitalName)
        {
            if (!string.IsNullOrEmpty(hospitalName))
            {
                var result = _repository.GetHospitalDetailsByName(hospitalName);

                return Mapper.Map<HospitalDetailsDto>(result);
            }

            return null;
        }
    }
}
