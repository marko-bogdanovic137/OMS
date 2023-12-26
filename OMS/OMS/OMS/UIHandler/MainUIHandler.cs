using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.UIHandler
{
    public class MainUIHandler
    {
      
        private readonly KvarUIHandler kvarUIHandler = new KvarUIHandler();
        private readonly ElektricniElementUIHandler elektricniElementUIHandler = new ElektricniElementUIHandler();

        public void HandleMainMenu()
        {
            string answer;
            do
            {
                
                Console.WriteLine("KVAROVI I UREDJAJI");
                Console.WriteLine("1 - KVAROVI");
                Console.WriteLine("2 - UREDJAJI");
                Console.WriteLine("X - Izlazak iz programa");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        kvarUIHandler.HandleKvarMenu();
                        break;
                    case "2":
                        elektricniElementUIHandler.HandleElektricniElementMenu();
                        break;
                }

            } while (!answer.ToUpper().Equals("X"));
        }
    }
}
