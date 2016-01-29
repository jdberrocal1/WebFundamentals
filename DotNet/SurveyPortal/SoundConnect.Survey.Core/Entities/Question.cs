using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Core.Entities
{
    [Table("Question", Schema = "survey")]
    public class Question
    {
        [Key]
        [Column("question_id")]
        public int Id { get; set; }

        [Column("question_key")]
        public string Key { get; set; }

        [Column("survey_type")]
        public int SurveyType { get; set; }

        [Column("question")]
        public string QuestionText { get; set; }

    }
}
