using Demon.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Demon
{
    public class Connector
    {
        private Client MyClient { get; set; }

        private string ApiString { get; set; } = "http://localhost:49497/api/";

        public void Update()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                string json = client.DownloadString(ApiString + "/query/getclient/" + this.GetMACAdress());
                var dynamicJson = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
                this.MyClient = new Client(dynamicJson[0]);
            }
        }

        public void Register()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");

                Client cl = new Client()
                {
                    Name = this.GetPCName(),
                    MAC = this.GetMACAdress(),
                    DateOfLogin  = null
                };

                var result = client.UploadString(this.ApiString + "client", JsonConvert.SerializeObject(cl));
            }
        }

        private string GetPCName()
        {
            return Environment.MachineName.ToString();
        }

        private string GetMACAdress()
        {
            foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet && item.OperationalStatus == OperationalStatus.Up &&
                    !item.Description.Contains("Virtual") && !item.Description.Contains("Pseudo"))
                    return item.GetPhysicalAddress().ToString();
            }
            return "";
        }
    }
}
