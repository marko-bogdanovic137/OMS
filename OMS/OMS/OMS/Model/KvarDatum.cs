using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model
{
	public class KvarDatum
	{
		public string ID { get; set; }

		public DateTime Datum { get; set; }

		public string Opis { get; set; }

		public string Status { get; set; }

		public KvarDatum() { }

		public KvarDatum(string id, DateTime datum, string opis, string status)
		{
			this.ID = id;
			this.Datum = datum;
			this.Opis = opis;
			this.Status = status;
		}

		public static string GetFormattedHeader()
		{
			return string.Format("{0,-15} {1,-35} {2,-20} {3,-35} ",
				"ID", "DATUM", "OPIS", "STATUS");
		}

		public override string ToString()
		{
			return string.Format("{0,-15} {1,-35} {2,-20} {3,-35}",
				this.ID, this.Datum.ToString("dd.MM.yyyy."), this.Opis, this.Status);
		}
	}

}
