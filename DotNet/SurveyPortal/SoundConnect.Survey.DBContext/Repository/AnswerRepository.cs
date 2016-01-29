using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Core.Repositories;
using SoundConnect.Survey.Data;
using System.Collections.Generic;
using System.Linq;

namespace SoundConnect.Survey.DBContext.Repository
{
    public class AnswerRepository : EntityRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(IDataContext context)
            : base(context)
        {

        }

        public ICollection<Answer> GetBySurveyId(int surveyId)
        {
            var answers = this.Context.Answer.Where<Answer>(d => d.SurveyAnswerId.Equals(surveyId));
            return answers.ToList<Answer>();
        }


    }
}
