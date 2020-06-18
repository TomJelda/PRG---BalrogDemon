using Demon.Backups;
using Demon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Demon
{
    public class Application
    {
        private Client Me { get; set; }

        private List<BackupTemplate> Backups { get; set; }

        public Application()
        {
            this.Backups = new List<BackupTemplate>();
            this.Start();
        }

        public void Start()
        {
            this.Menu();
            this.Loop();
        }

        private void Loop()
        {
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    char input = Console.ReadKey(false).KeyChar;
                    switch (input)
                    {
                        case '1':
                            Connector.RegisterClient();
                            this.Menu();
                            break;
                        case '2':
                            Connector.UpdateClient(this.Me);
                            this.Menu();
                            break;
                        case '3':
                            Connector.GetMyJobs(this.Backups);
                            this.Menu();
                            break;
                        case '4':
                            this.DoFull();
                            this.Menu();
                            break;
                        case '5':
                            this.DoDiff();
                            this.Menu();
                            break;
                        case '6':
                            this.DoInc();
                            this.Menu();
                            break;
                    }
                }
            }
        } 

        public void MakeDiff()
        {
            //DifferentialBackups diff = new DifferentialBackups();
        }

        private void Menu()
        {
            Console.Clear();
            Console.WriteLine("Klikni '1' pro manuální registraci nebo '2' pro vyvolání aktualizace dat");
            Console.WriteLine(Connector.ClientString);
            Console.WriteLine(Connector.test);

            if (this.Me != null)
            {
                Console.WriteLine(this.Me.Name + ", " + this.Me.MAC);
            }
        }

        private void DoJobs()
        {
            foreach (var job in this.Backups)
                job.DoJob();
        }

        private void DoFull()
        {
            this.Backups[0].DoJob();
        }

        private void DoDiff()
        {
            this.Backups[1].DoJob();
        }

        private void DoInc()
        {
            //this.Backups[0].DoJob();

            IncrementalBackup diff = new IncrementalBackup(null);
            diff.DOOT();
        }
    }
}
