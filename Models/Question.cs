using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflow.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
