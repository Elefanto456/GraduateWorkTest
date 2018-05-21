using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduateWorkTest
{
    public class EachSentenceInfo
    {
        public string text { get; set; }
        public JObject annotations { get; set; } 
    }
}
