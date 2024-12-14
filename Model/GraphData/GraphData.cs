using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms.Model.GraphData
{
    [JsonObject(ItemTypeNameHandling = TypeNameHandling.Auto)]
    public abstract class GraphData
    {
        public string Id { get; set; }
        public Question? Question { get; set; }
    }
}
