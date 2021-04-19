using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class QuizItem
    {
        public int QuizItemId { get; set; }
        public string Question { get; set; }
        [NotMapped]
        public string[] Answers { get; set; }
        [NotMapped]
        public string[] Options { get; set; }
    }
}
