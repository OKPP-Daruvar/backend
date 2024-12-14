using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Model.GraphData
{
    public class ChoiceGraphData
    {
        public int Id { get; set; }
        public Question? Question { get; set; }
        public Dictionary<string, int>? Answers { get; set; }

    }
}
