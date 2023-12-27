using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.Model;
using OMS.DAO;
using OMS.DAO.Impl;

namespace OMS.Services
{
	public class KvarDatumService
	{
		private static readonly IKvarDatumDAO kvarDatumDAO = new KvarDatumDAOImpl();

		public List<KvarDatum> FindAll(DateTime startDate, DateTime endDate)
		{
			return kvarDatumDAO.FindAll(startDate, endDate).ToList();
		}

		public void FindKvar(string id)
		{
			 kvarDatumDAO.FindKvar(id);
		}
	}
}
