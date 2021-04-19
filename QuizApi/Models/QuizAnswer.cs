using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    [Table("QuizAnswer")]
    public class QuizAnswer
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        [ForeignKey("QuizItem")]
        public int QuizItemId { get; set; }
        public QuizItem QuizItem { get; set; }
    }
}
