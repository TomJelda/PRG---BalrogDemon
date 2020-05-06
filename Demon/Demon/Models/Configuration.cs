using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demon.Models
{
    public class Configuration
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string BackupType { get; set; }

        public string Cron { get; set; }

        public bool RepeatableBackup { get; set; }

        public int SavedBackupNumber { get; set; }

        public Configuration(JToken json)
        {
            this.Name = (string)json["Name"];
            this.Description = (string)json["Description"];
            this.BackupType = (string)json["BackupType"];
            this.Cron = (string)json["Cron"];
            this.RepeatableBackup = (bool)json["RepeatableBackup"];
            this.SavedBackupNumber = (int)json["SavedBackupNumber"];
        }
    }
}
