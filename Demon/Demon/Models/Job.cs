using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demon.Models
{
    public class Job
    {
        public Configuration Configuration { get; set; }

        public Source Source { get; set; }

        public DestFtpServer DestFtpServer { get; set; }

        public DestLocal DestLocal { get; set; }

        public Job(JToken json)
        {
            this.Configuration = new Configuration(json["configuration"]);
            this.Source = new Source(json["source"]);
            this.DestFtpServer = new DestFtpServer(json["destFtpServer"]);
            this.DestLocal = new DestLocal(json["destLocal"]);
        }

        public bool CheckJob()
        {
            return true;
            //return this.CheckCRON();
        }

        private bool CheckCRON() // TODO, timer, quards - knihovna
        {
            if (Configuration.Cron == "NOW")
                return true;

            return false;
        }
    }
}
