using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model
{
	public class Akcija
	{
		public string ID { get; set; }

		public string Date { get; set; }

		public string OpisPosla { get; set; }

		public Akcija() { }

		public Akcija(string id, string date, string opisPosla)
		{
			this.ID = id;
			this.Date = date;
			this.OpisPosla = opisPosla;
		}

		public static string GetFormattedHeader()
		{
			return string.Format("{0,-15} {1,-35} {2,-50}",
				"ID", "Datum", "OpisPosla");
		}

		public override string ToString()
		{
			return string.Format("{0,-15} {1,-35} {2,-50}",
				this.ID, this.Date, this.OpisPosla);
		}
	}

}
