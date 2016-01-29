using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundConnect.Survey.Core.Entities
{
    [Table("SurveyAnswer", Schema = "survey")]
    public class SurveyAnswer
    {
        [Key]
        [Column("survey_answer_id")]
        public int Id { get; set; }

        [Column("survey_id")]
        public int SurveyId { get; set; }

        [Column("respond_date")]
        public DateTime RespondDate { get; set; }
    }
}
