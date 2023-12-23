using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using OMS.DAO;
using OMS.DAO.Impl;
using OMS.Model;
using OMS.Services;
using System.Data;

namespace OMS.UIHandler
    {
    public class KvarUIHandler
    {
        private readonly KvarService kvarService = new KvarService();

        public void HandleKvarMenu()
        {
            String answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju za rad sa kvarovima:");
                Console.WriteLine("1 - Prikaz svih");
                Console.WriteLine("2 - Unos jednog kvara");
                Console.WriteLine("3 - Promeni status kvara");
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
                    case "3":
                        PromeniStatus();
                        break;
                }
            } while (!answer.ToUpper().Equals("X"));
        }
        private void ShowAll()
        {
            Console.WriteLine(Kvar.GetFormattedHeader());

            try
            {
                foreach (Kvar kvar in kvarService.FindAll())
                {
                    Console.WriteLine(kvar);
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UnosJednog()
        {
                string yyyy = "";
                string MM = "0";
                string dd = "0";
                string mm = "-1";
                string ss = "-1";
                int brojac = 0;

                while (!string.Equals(yyyy, "2023"))
                {
                    Console.WriteLine("Unesi godinu kvara");
                    yyyy = Console.ReadLine();
                }

                while (int.Parse(MM) > 12 || int.Parse(MM) < 1)
                {
                    Console.WriteLine("Unesi mesec kvara");
                    MM = Console.ReadLine();
                }

                while (int.Parse(dd) > 12 || int.Parse(dd) < 1)
                {
                    Console.WriteLine("Unesi dan kvara");
                    dd = Console.ReadLine();
                }

                while (int.Parse(mm) > 60 || int.Parse(mm) < 0)
                {
                    Console.WriteLine("Unesi minut kvara");
                    mm = Console.ReadLine();
                }

                while (int.Parse(ss) > 60 || int.Parse(ss) < 0)
                {
                    Console.WriteLine("Unesi sekundu kvara");
                    ss = Console.ReadLine();
                }

                Console.WriteLine("Unesi datum belezenja kvara");
                string datum = Console.ReadLine();
            

                string status = "Nepotvrdjen";

                Console.WriteLine("Unesi kratak izvestaj o kvaru");
                string izvestaj = Console.ReadLine();

                string id = yyyy + MM + dd + mm + ss + "_" + brojac.ToString();

                try
                {
                int inserted = kvarService.Save(new Kvar(id, datum, status, izvestaj));
                if (inserted != 0)
                    {
                        Console.WriteLine("Kvar uspesno dodat");
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public void PromeniStatus()
            {
                Console.WriteLine("ID: ");
                string id = Console.ReadLine();

                try
                {
                    Kvar postojeciKvar = kvarService.FindById(id);
                    if (!kvarService.ExistById(id))
                    {
                        Console.WriteLine("Kvar ne postoji");
                        return;
                    }


                    string status = " ";
                    while (string.Equals(status, "U popravci") == false || string.Equals(status, "Testiranje") == false ||
                         string.Equals(status, "Zatvoreno") == false)
                     {
                        Console.WriteLine("Novi status kvara: ");
                        status = Console.ReadLine();
                        postojeciKvar.Status = status;
                    }

                    int updated = kvarService.Save(postojeciKvar);
                    if (updated != 0)
                    {
                        Console.WriteLine("Status kvara uspesno izmenjen");
                    }
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
}

