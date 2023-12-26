using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model
{
    public class ElektricniElement
    {
        public string ID { get; set; }

        public string Naziv { get; set; }

        public string Tip { get; set; }

        public string GeoLokacija { get; set; }

        public string NaponskiNivo { get; set; }

        public ElektricniElement() { }

        public ElektricniElement(string ID, string Naziv, string Tip, string GeoLokacija, string naponskiNivo)
        {
            this.ID = ID;
            this.Naziv = Naziv;
            this.Tip = Tip;
            this.GeoLokacija = GeoLokacija;
            this.NaponskiNivo = naponskiNivo;
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-6} {1,-35} {2,-20} {3,-35} {4,-20} ",
              "ID", "NAZIV", "TIP", "GEOLOKACIJA", "NAPONSKI_NIVO");

        }

        public override string ToString()
        {
            return string.Format("{0,-6} {1,-35} {2,-20} {3,-35} {4,-20}",
                this.ID, this.Naziv, this.Tip, this.GeoLokacija, this.NaponskiNivo);
        }

    }
}
