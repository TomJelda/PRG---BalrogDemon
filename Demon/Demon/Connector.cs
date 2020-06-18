using Demon.Backups;
using Demon.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public const string Token = "eyJ0eXAiOiJKV1QiššrLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImNsaWVudCJ9.UkxCoZeXQrxcVrobfDZEKisP4y-qyFRucQUCLoCM2KA";
        public static string test = "";

        public static void UpdateClient(Client Client)
        {
            using (var client = new WebClient())   
            {
                try
                {
                    client.Headers.Add("Content-Type:application/json");
                    client.Headers.Add("Accept:application/json");
                    string jsonString = client.DownloadString(ApiString + "/query/getclient/?token=" + Connector.Token + "&mac=" + Connector.GetMACAdress());
                    var json = JsonConvert.DeserializeObject<dynamic>(jsonString)[0];
                    Client = new Client(json);
                    Connector.ClientString = "Name: " + json["Name"] + ", MAC: " + json["MAC"] + ", Date of login: " + json["DateOfLogin"];
                }
                catch 
                {
                    Connector.ClientString = "Client Neexistuje";
                }
            }
        }

        public static void RegisterClient()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");

                Client newClient = new Client()
                {
                    Name = Connector.GetPCName(),
                    MAC = Connector.GetMACAdress(),
                    DateOfLogin = null
                };

                client.QueryString.Add("token", Connector.Token);
                client.QueryString.Add("item", JsonConvert.SerializeObject(newClient));

                var post = client.UploadString(ApiString + "client/?token=" + Connector.Token, JsonConvert.SerializeObject(newClient));
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
            string jsonString = "";
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");

                jsonString = client.DownloadString(ApiString + "/query/GetMyJobs/?token=" + Connector.Token + "&mac=" + Connector.GetMACAdress());
            }

            JArray json = JsonConvert.DeserializeObject<dynamic>(jsonString); //ne switch, dictionary, key - typ
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
