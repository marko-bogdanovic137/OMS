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

        public string Datum { get; set; }

        public string Status { get; set; }

        public string Opis { get; set; }

        public Kvar() { }

        public Kvar(string iD, string datum, string status, string opis)
        {
            this.ID = iD;
            this.Datum = datum;
            this.Status = status;
            this.Opis = opis;
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-15} {1,-35} {2,-20} {3,-35} ",
                "ID", "DATUM", "STATUS", "OPIS");
        }

        public override string ToString()
        {
            return string.Format("{0,-15} {1,-35} {2,-20} {3,-35}",
                this.ID, this.Datum, this.Status, this.Opis);
        }
    }
}
