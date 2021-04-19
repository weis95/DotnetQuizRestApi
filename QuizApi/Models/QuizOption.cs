using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    [Table("QuizOption")]
    public class QuizOption
    {
        public int Id { get; set; }
        public string Option { get; set; }
        [ForeignKey("QuizItem")]
        public int QuizItemId { get; set; }
        public QuizItem QuizItem { get; set; }
    }
}
