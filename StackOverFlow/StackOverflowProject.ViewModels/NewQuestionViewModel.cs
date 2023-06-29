using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.ViewModels
{
    public class NewQuestionViewModel
    {
        [Required]
        public string QuestionText { get; set; }

        [Required]
        public DateTime QuestionAndTime { get; set; }

        [Required]
        public int UserID { get; set; }
   
        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int VoteCount { get; set; }

        [Required]
        public int AnswerCount { get; set; }

        [Required]
        public int ViewsCount { get; set; }
    }
}
