using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Core.Repositories;
using SoundConnect.Survey.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.DBContext.Repository
{
    public class SurveyAnswerRepository : EntityRepository<SurveyAnswer>, ISurveyAnswerRepository
    {
        public SurveyAnswerRepository(IDataContext context)
            : base(context)
        {

        }
    }
}
