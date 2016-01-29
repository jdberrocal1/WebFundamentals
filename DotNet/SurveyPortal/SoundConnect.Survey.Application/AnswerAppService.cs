using AutoMapper;

using SoundConnect.Survey.Application.DTOs;
using SoundConnect.Survey.Application.Interfaces;
using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Core.Repositories;
using System.Collections.Generic;
using System.Transactions;

namespace SoundConnect.Survey.Application
{
    public class AnswerAppService : IAnswerAppService
    {
        private readonly IAnswerRepository _repository;

        public AnswerAppService(IAnswerRepository context)
        {
            _repository = context;
        }

        public ICollection<AnswerDto> GetBySurveyId(int surveyId)
        {
            var result = _repository.GetBySurveyId(surveyId);

            return Mapper.Map<List<AnswerDto>>(result);
        }

        public void Insert(ICollection<AnswerDto> answersDto)
        {
            using (var transaction = new TransactionScope())
            {
                foreach (AnswerDto answerDto in answersDto)
                {
                    var answer = Mapper.Map<Answer>(answerDto);
                    _repository.Create(answer);
                }

                transaction.Complete();
            }
        }


    }

}
