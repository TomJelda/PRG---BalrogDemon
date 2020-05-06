using Demon.Backups;
using Demon.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Demon
{
    public static class Connector
    {
        public static string ApiString { get; } = "http://localhost:49497/api/";

        public static string ClientString { get; set; } = "";
        public static string Test = "";

        static Connector()
        {
            Random randon = new Random();
        }

        public static void UpdateClient(Client Client)
        {
            using (var client = new WebClient())
            {
                try
                {
                    client.Headers.Add("Content-Type:application/json");
                    client.Headers.Add("Accept:application/json");
                    string jsonString = client.DownloadString(ApiString + "/query/getclient/" + Connector.GetMACAdress());
                    var json = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString)[0];
                    Client = new Client(json);
                    Connector.ClientString = "Name: " + json["Name"] + ", MAC: " + json["MAC"] + ", Date of login: " + json["DateOfLogin"];
                }
                catch 
                {
                    Connector.ClientString = "Client Neexistuje";
                }
            }
        }

        public static void Register()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");

                Client cl = new Client()
                {
                    Name = Connector.GetPCName(),
                    MAC = Connector.GetMACAdress(),
                    DateOfLogin  = null
                };

                var result = client.UploadString(ApiString + "client", JsonConvert.SerializeObject(cl));
            }
        }

        public static string GetPCName()
        {
            return Environment.MachineName.ToString();
        }

        public static string GetMACAdress()
        {
            foreach (var item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet && item.OperationalStatus == OperationalStatus.Up &&
                    !item.Description.Contains("Virtual") && !item.Description.Contains("Pseudo"))
                    return item.GetPhysicalAddress().ToString();
            }
            return "";
        }

        public static void GetMyJobs(List<BackupTemplate> Backups)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                string jsonString = client.DownloadString(ApiString + "/query/GetMyJobs/" + Connector.GetMACAdress());
                JArray json = JsonConvert.DeserializeObject<dynamic>(jsonString);

                for (int i = 0; i < json.Count; i++)
                {
                    var configuration = json[i]["configuration"];
                    switch ((string)configuration["BackupType"])
                    {
                        case "FULL":
                            Backups.Add(new FullBackup(json[i]));
                            break;
                        case "INC":
                            Backups.Add(new IncrementalBackup(json[i]));
                            break;
                        case "DIFF":
                            Backups.Add(new DifferentialBackups(json[i]));
                            break;
                    }
                }
            }
        }
    }
}
