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

namespace OMS.UIHandler
{
	public class KvarDatumiUIHandler
	{
		private readonly KvarDatumService kvarDatumService = new KvarDatumService();
		private readonly KvarService kvarService = new KvarService();
		private readonly AkcijaService akcijaService = new AkcijaService();
		public void HandleKvarDatumiMenu()
		{
			String answer;
			do
			{
				Console.WriteLine();
				Console.WriteLine("Odaberite opciju za rad sa kvarovima:");
				Console.WriteLine("1 - Pretraga po datumu");
				Console.WriteLine("2 - Prikaz jednog kvara");
				Console.WriteLine("X - Main menu");

				answer = Console.ReadLine();

				switch (answer)
				{
					case "1":
						UnosDatuma();
						break;
					case "2":
						PrikazKvara();
						break;
				}
			} while (!answer.ToUpper().Equals("X"));
		}
		private void UnosDatuma()
		{
			DateTime date;
			DateTime date2;
			string input = "";
			string input2 = "";
			while (true)
			{
				Console.WriteLine("Unesite pocetni datum (pr. 26.12.2023.): ");
				input = Console.ReadLine();
				if (DateTime.TryParse(input, out date))
				{
					Console.WriteLine("Unesite krajnji datum: ");
					input2 = Console.ReadLine();
					if (DateTime.TryParse(input2, out date2))
					{
						break;
					}
					else
					{
						Console.WriteLine("Pogresan format, pokusaj ponovo: ");
					}
				}
				else
				{
					Console.WriteLine("Pogresan format, pokusaj ponovo: ");
				}
			}
			Console.WriteLine(KvarDatum.GetFormattedHeader());
			Console.WriteLine("------------------------------------------------------------------------------------");
			foreach (KvarDatum kvarDatum in kvarDatumService.FindAll(date, date2))
			{
				Console.WriteLine(kvarDatum);
				Console.WriteLine("------------------------------------------------------------------------------------");
			}

		}
		private void PrikazKvara() 
		{
			DateTime currentDate;
			TimeSpan timeSpan;
			double poeni = -1;

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

				if (postojeciKvar.Status == "U popravci")
				{
					currentDate = DateTime.Now;
					timeSpan = currentDate - postojeciKvar.Datum;
					double totalDays = (int)timeSpan.TotalDays;

					double rows = (akcijaService.CountRowsWithId(id) / 2.0);

					poeni = totalDays + rows;
					poeni = Math.Round(poeni, 2);
				}

				kvarDatumService.FindKvar(id);
				if (poeni != -1)
				{
					Console.WriteLine();
					Console.WriteLine("Prioritet popravke je: " + poeni);
				}
			}
			catch (DbException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
