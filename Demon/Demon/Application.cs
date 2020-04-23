using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demon
{
    public class Application
    {
        private Connector connector { get; set; }

        public void Start()
        {
            this.connector = new Connector();

            this.Menu();

            this.Loop();
        }

        private void Loop()
        {
            while(true)
            {
                char input = Console.ReadKey(false).KeyChar;

                switch (input)
                {
                    case '1':
                        this.connector.Register();
                        this.Menu();
                        break;
                    case '2':
                        this.connector.Update();
                        this.Menu();
                        break;
                }
            }
        } 

        private void Menu()
        {
            Console.Clear();
            Console.WriteLine("Klikni '1' pro manuální registraci nebo '2' pro vyvolání aktualizace dat");
        }
    }
}
