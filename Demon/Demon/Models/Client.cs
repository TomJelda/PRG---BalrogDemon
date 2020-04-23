using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demon.Models
{
    public class Client
    {
        public Client() { }

        public Client(dynamic json)
        {
            this.Id = json.Id;
            this.Name = json.Name;
            this.MAC = json.MAC;
            this.DateOfLogin = json.DateOfLogin;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string MAC { get; set; }

        public DateTime? DateOfLogin { get; set; }
    }
}
