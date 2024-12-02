using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Model
{
    public class Question
    {
        public string QuestionId { get; set; } 
        public string Text { get; set; } 
        public QuestionType Type { get; set; } 
        public List<string> Options { get; set; }
    }

}
