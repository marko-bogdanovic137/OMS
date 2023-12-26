using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model
{
	public class KvarInfo
	{
		public string ID { get; set; }

		public string kOpis { get; set; }

		public string element { get; set; }

		public string dOpis { get; set; }


		public KvarInfo() { }

		public KvarInfo(string iD, string kOpis, string element, string dOpis)
		{
			this.ID = iD;
			this.kOpis = kOpis;
			this.element = element;
			this.dOpis = dOpis;
		}

		public static string GetFormattedHeader()
		{
			return string.Format("{0,-15} {1,-35} {2,-20} {3,-35}",
				"ID", "kOpis", "element", "dOpis");
		}

		public override string ToString()
		{
			return string.Format("{0,-15} {1,-35} {2,-20}\n{3,-35}",
				this.ID, this.kOpis, this.element, this.dOpis);
		}
	}
}