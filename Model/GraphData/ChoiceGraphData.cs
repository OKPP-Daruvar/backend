using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Model.GraphData
{
    public class ChoiceGraphData : GraphData
    {
        public Dictionary<string, int>? Answers { get; set; }
    }
}
