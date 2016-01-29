using AutoMapper;

using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Core.Repositories;

namespace SoundConnect.Survey.Application
{
    public class SurveyAnswerAppService : ISurveyAnswerAppService
    {
        private readonly ISurveyAnswerRepository _repository;

        public SurveyAnswerAppService(ISurveyAnswerRepository context)
        {
            _repository = context;
        }

        public SurveyAnswerDto Insert(SurveyAnswerDto surveyAnswersDto)
        {
            var surveyAnswer = Mapper.Map<SurveyAnswer>(surveyAnswersDto);
            surveyAnswer = _repository.Create(surveyAnswer);
            return Mapper.Map<SurveyAnswerDto>(surveyAnswer);
        }
    }

}
