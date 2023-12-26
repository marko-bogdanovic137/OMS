using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Model;
using OMS.Service;
using System.Data.Common;


namespace OMS.UIHandler
{
    public class ElektricniElementUIHandler
    {
        private static readonly ElektricniElementService elektricniElementService = new ElektricniElementService();

        public void HandleElektricniElementMenu()
        {
            String answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju za rad sa elektricnim elementima:");
                Console.WriteLine("1 - Prikaz svih");
                Console.WriteLine("2 - Unos jednog elementa");

                Console.WriteLine("X - Izlazak iz aplikacije");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        ShowAll();
                        break;
                    case "2":
                        UnosJednog();
                        break;

                }
            } while (!answer.ToUpper().Equals("X"));
        }
        private void ShowAll()
        {
            Console.WriteLine(ElektricniElement.GetFormattedHeader());
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");

            try
            {
                foreach (ElektricniElement elektricniElement in elektricniElementService.FindAll())
                {
                    Console.WriteLine(elektricniElement);
					Console.WriteLine("------------------------------------------------------------------------------");
				}
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UnosJednog()
        {
            Console.WriteLine("Unesite id elektricnog elementa");
            string id = Console.ReadLine();

            Console.WriteLine("Unesite naziv elektricnog elementa");
            string naziv = Console.ReadLine();

            Console.WriteLine("Unesite tip elektricnog elementa");
            string tip = Console.ReadLine();

            Console.WriteLine("Unesite geolokaciju elektricnog elementa");
            string geolokacija = Console.ReadLine();

            string naponskiNivo = "";
            do
            {
                Console.WriteLine("Unesite naponski nivo elektricnog elementa (visoki/srednji/nizak napon)");
                naponskiNivo = Console.ReadLine();

            }
            while (!IsValidInput(naponskiNivo));
            

            try
            {
                int inserted = elektricniElementService.Save(new ElektricniElement(id, naziv, tip, geolokacija, naponskiNivo));
                if (inserted != 0)
                {
                    Console.WriteLine("Elektricni element uspesno dodat");
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static bool IsValidInput(string input)
        {
            return input!="visoki napon" || input != "srednji napon" || input != "nizak napon";
        }

    }
}
