using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundConnect.Survey.Core.Entities;

namespace SoundConnect.Survey.Data
{
    public interface IDataContext
    {
        IDbSet<Core.Entities.Survey> Survey { get; set; }
        IDbSet<Question> Question { get; set; }
        IDbSet<Answer> Answer { get; set; }
        IDbSet<SurveyAnswer> SurveyAnswer { get; set; }
        Database Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
