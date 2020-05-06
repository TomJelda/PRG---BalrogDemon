using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demon.Models
{
    public class DestLocal
    {
        public string Path { get; set; }

        public string FileSuffix { get; set; }

        public DestLocal(JToken json)
        {
            this.Path = (string)json["Path"];
            this.FileSuffix = (string)json["FileSuffix"];
        }
    }
}
