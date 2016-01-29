using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoundConnect.Survey.Core.Entities
{
    [Table("Answer", Schema = "survey")]
    public class Answer
    {
        [Key]
        [Column("answer_id")]
        public int Id { get; set; }

        [Column("survey_answer_id")]
        public int SurveyAnswerId { get; set; }

        [Column("question_id")]
        public int QuestionId { get; set; }

        [Column("response_text")]
        public string ResponseText { get; set; }

        [Column("note")]
        public string Note { get; set; }

    }
}
