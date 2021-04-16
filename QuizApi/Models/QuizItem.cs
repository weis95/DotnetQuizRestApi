using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class QuizItem
    {
        public int QuizItemId { get; set; }
        public string Question { get; set; }
        public string Answers { get; set; }
        public string Options { get; set; }
    }
}
