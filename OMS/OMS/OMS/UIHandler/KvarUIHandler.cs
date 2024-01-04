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
using OMS.Service;
using OfficeOpenXml;
using System.IO;

namespace OMS.UIHandler
{
    public class KvarUIHandler
    {
        private readonly KvarService kvarService = new KvarService();
        private readonly ElektricniElementService elektricniElementService = new ElektricniElementService();
		private readonly KvarInfoService kvarInfoService = new KvarInfoService();
        private readonly AkcijaService akcijaService = new AkcijaService();
        private readonly PrenosService prenosService = new PrenosService();
        private readonly KvarDatumiUIHandler kvarDatumiUIHandler = new KvarDatumiUIHandler();

		private DateTime lastCreationDate = DateTime.MinValue;
		private int dailyCount = 0;

		string id = "";

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
                Console.WriteLine("4 - Prikazi tabelu KvarInfo");
                Console.WriteLine("5 - Dodaj akciju");
                Console.WriteLine("6 - Prikazi akcije");
                Console.WriteLine("7 - Napredna pretraga kvarova");
                Console.WriteLine("8 - Kreiraj Excel tabelu");
                Console.WriteLine("X - Main menu");

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
                    case "4":
                        ShowAll2();
                        break;
                    case "5":
                        UnosAkcije();
                        break;
                    case "6":
                        ShowAll3();
                        break;
                    case "7":
                        kvarDatumiUIHandler.HandleKvarDatumiMenu();
                        break;
                    case "8":
                        KreirajTabelu();
                        break;
                }
            } while (!answer.ToUpper().Equals("X"));
        }
        private void ShowAll()
        {
            Console.WriteLine(Kvar.GetFormattedHeader());
			Console.WriteLine("------------------------------------------------------------------------------");

			try
            {
                foreach (Kvar kvar in kvarService.FindAll())
                {
                    Console.WriteLine(kvar);
					Console.WriteLine("------------------------------------------------------------------------------");
				}
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ShowAll2()
        {
            Console.WriteLine(KvarInfo.GetFormattedHeader());
			Console.WriteLine("------------------------------------------------------------------------------");

            try
            {
                foreach (KvarInfo kvarInfo in kvarInfoService.FindAll())
                {
                    Console.WriteLine(kvarInfo);
					Console.WriteLine("------------------------------------------------------------------------------");
				}
            }
			catch (DbException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private void ShowAll3()
		{
			Console.WriteLine(Akcija.GetFormattedHeader());
			Console.WriteLine("------------------------------------------------------------------------------");

			try
			{
				foreach (Akcija akcija in akcijaService.FindAll())
				{
					Console.WriteLine(akcija);
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
                try
                {
                    int inserted = kvarService.Save(kreiranje());
                    if (inserted != 0)
                    {
                        Console.WriteLine();
					    Console.WriteLine("----------------------------------");
					    Console.WriteLine("Kvar uspesno dodat");
					    
				}
                    int inserted2 = kvarInfoService.Save(kreiranje2());
                    if(inserted2 != 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("Kvar info uspesno dodat");
                        Console.WriteLine();
                    }
                    
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }
            
            private void UnosAkcije()
            {
			    try
			    {
				    int inserted = akcijaService.Save(kreiranje3());
                if (inserted != 0)
                {
                    Console.WriteLine("Akcija uspesno dodata");
                    Console.WriteLine("----------------------------------");
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
                    if (postojeciKvar.Status == "Zatvoreno")
                    {
                        Console.WriteLine("Nije moguce izmeniti status kvara");
                        return;
                    }
                    Console.WriteLine("Odaberite zeljeni status kvara:");
                    string unos = "";
                    string status = " ";
                while (unos != "1" && unos != "2" && unos != "3")
                {
                    Console.WriteLine("1. U popravci");
                    Console.WriteLine("2. Testiranje");
                    Console.WriteLine("3. Zatvoreno");
                    unos = Console.ReadLine();
                    Console.WriteLine();

                    switch (unos)
                    {
                        case "1":
							Console.WriteLine("----------------------------------");
							Console.WriteLine("Status kvara je promenjen u 'U popravci'.");
                            status = "U popravci";
                            break;
                        case "2":
							Console.WriteLine("----------------------------------");
							Console.WriteLine("Status kvara je promenjen u 'Testiranje'.");
                            status = "Testiranje";
                            break;
                        case "3":
							Console.WriteLine("----------------------------------");
							Console.WriteLine("Status kvara je promenjen u 'Zatvoreno'.");
                            status = "Zatvoreno";
                            break;
                        default:
							Console.WriteLine("----------------------------------");
							Console.WriteLine("Nepoznata opcija. Molimo unesite broj od 1 do 3.");
                            break;
                    }
                }
                    postojeciKvar.Status = status;
                    Kvar noviKvar = postojeciKvar;

                    kvarService.DeleteById(postojeciKvar.ID);

                    int updated = kvarService.Save(noviKvar);
                    if (updated != 0)
                    {
                        Console.WriteLine();
                    }

                   
                    
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        private Kvar kreiranje()
        {
            string yyyy = "";
            string MM = "0";
            string dd = "0";
            string mm = "-1";
            string ss = "-1";

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

            string status = "Nepotvrdjen";

            if (DateTime.Today != lastCreationDate)
            { 
				dailyCount = 0;
				lastCreationDate = DateTime.Today;
		    }
			    dailyCount++;


			id = yyyy + MM + dd + mm + ss + "_" + dailyCount;

                DateTime datum = DateTime.Today;


                return new Kvar(id, datum, status);
            }
        private KvarInfo kreiranje2()
        {
                    Console.WriteLine();
                    Console.WriteLine("Popunjavanje dodatnih informacija za kvar");
			        Console.WriteLine("----------------------------------");
			        Console.WriteLine("Unesite kratak opis kvara:");
                    string opis = Console.ReadLine();

                    Console.WriteLine("Unesite id elementa na kom se desio kvar: ");
                    string element = Console.ReadLine();

                    if(!elektricniElementService.ExistsById(element))
                    {
                        Console.WriteLine("Element ne postoji");
                        return null;
                    }

                    ElektricniElement elementt = elektricniElementService.FindById(element);
                    Console.WriteLine("Unesite detaljan opis kvara");
                    string dOpis = Console.ReadLine();

                    Console.Write("Da li postoji izvrsena akcija za kvar? DA/NE  ");
                    string input = Console.ReadLine().ToUpper();

			    switch (input)
			    {
				case "DA":
                    kreiranje3();
					break;
				case "NE":
					break;
				default:
					Console.WriteLine("Nepoznata opcija.");
					break;
			    }


			return new KvarInfo(id, opis, elementt.Naziv, dOpis);
        }

        private Akcija kreiranje3()
        {
            string datum = "" ;
            string opis = "";
            if (id != "")
            {
                Console.WriteLine("Unesite datum uzvrsenja akcije: ");
                 datum = Console.ReadLine();

                Console.WriteLine("Unesite opis uradjenog posla: ");
                 opis = Console.ReadLine();    
            }
            else
            {
                Console.WriteLine("ID kvara: ");
                string stringo = Console.ReadLine();
				Kvar postojeciKvar = kvarService.FindById(stringo);
				if (!kvarService.ExistById(stringo))
				{
					Console.WriteLine("Kvar ne postoji");
                    return null;
				}
				if (postojeciKvar.Status == "Zatvoreno")
				{
					Console.WriteLine("Nije moguce dodati akciju kavru");
					return null;
				}
				Console.WriteLine("Unesite datum uzvrsenja akcije: ");
				datum = Console.ReadLine();

				Console.WriteLine("Unesite opis uradjenog posla: ");
				opis = Console.ReadLine();

				return new Akcija(stringo, datum, opis);
			}
            return new Akcija(id, datum, opis);
        }

        public void KreirajTabelu()
        {
            try
            {
                prenosService.Prenos();
                Console.WriteLine("Excel tabela uspesno kreirana");
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}

