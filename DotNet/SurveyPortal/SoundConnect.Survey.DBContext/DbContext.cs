using SoundConnect.Survey.Core.Entities;
using SoundConnect.Survey.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System;

namespace SoundConnect.Survey.EntityFramework
{
    public class SCDbContext : DbContext, IDataContext
    {
        public virtual IDbSet<Core.Entities.Survey> Survey { get; set; }
        public virtual IDbSet<Question> Question { get; set; }
        public virtual IDbSet<Answer> Answer { get; set; }
        public virtual IDbSet<SurveyAnswer> SurveyAnswer { get; set; }

        public Database Database
        {
            get
            {
                return Database;
            }
        }

        public SCDbContext()
            : base("Default")
        {
            Database.SetInitializer<SCDbContext>(null);
        }

        public SCDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public SCDbContext(DbConnection connection)
            : base(connection, true)
        {

        }

        public ICollection<ValidationResult> Commit()
        {
            var validationResults = new List<ValidationResult>();

            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException dbe)
            {
                foreach (DbEntityValidationResult validation in dbe.EntityValidationErrors)
                {
                    IEnumerable<ValidationResult> validations = validation.ValidationErrors.Select(
                        error => new ValidationResult(
                                     error.ErrorMessage,
                                     new[]
                                         {
                                             error.PropertyName
                                         }));

                    validationResults.AddRange(validations);

                    return validationResults;
                }
            }
            return validationResults;
        }
    }
}
