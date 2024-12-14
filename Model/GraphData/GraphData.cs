using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Model.GraphData
{
    public abstract class GraphData
    {
        public int Id { get; set; }
        public Question? Question { get; set; }
    }
}
