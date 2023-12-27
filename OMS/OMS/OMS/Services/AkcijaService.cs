using OMS.DAO.Impl;
using OMS.DAO;
using OMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Services
{
	public class AkcijaService
	{
		private static readonly IAkcijeDAO akcijaDAO = new AkcijeDAOImpl();

		public int Save(Akcija akcija)
		{
			return akcijaDAO.Save(akcija);
		}
		public List<Akcija> FindAll()
		{
			return akcijaDAO.FindAll().ToList();
		}

		public int CountRowsWithId(string id)
		{
			return akcijaDAO.CountRowsWithId(id);
		}
	}

}
