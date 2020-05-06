using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demon.Models
{
    public class Source
    {
        public string Path { get; set; }

        public Source(JToken json)
        {
            this.Path = (string)json["Path"];
        }
    }
}
