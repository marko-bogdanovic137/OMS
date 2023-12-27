using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model
{
    public class Kvar
    {
        public string ID { get; set; }

        public DateTime Datum { get; set; }

        public string Status { get; set; }


        public Kvar() { }

        public Kvar(string iD,DateTime datum, string status)
        {
            this.ID = iD;
            this.Datum = datum;
            this.Status = status;
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-15} {1,-35} {2,-20} ",
                "ID", "DATUM", "STATUS");
        }

		public override string ToString()
		{
			return string.Format("{0,-15} {1,-35} {2,-20} ",
				this.ID, this.Datum.ToString("dd.MM.yyyy."), this.Status);
		}
	}
}
