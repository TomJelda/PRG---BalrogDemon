using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demon.Models
{
    public class DestFtpServer
    {
        public string Site { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Port { get; set; }

        public string FileSuffix { get; set; }

        public DestFtpServer(JToken json)
        {
            this.Site = (string)json["Site"];
            this.Login = (string)json["Login"];
            this.Password = (string)json["Password"];
            this.Port = (string)json["Port"];
            this.FileSuffix = (string)json["FileSuffix"];
        }
    }
}
