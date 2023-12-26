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
    public class KvarInfoService
    {
        private static readonly IKvarInfoDAO kvarInfoDAO = new KvarInfoDAOImpl();

        public int Save(KvarInfo kvarInfo)
        {
            return kvarInfoDAO.Save(kvarInfo);
        }
		public List<KvarInfo> FindAll()
		{
			return kvarInfoDAO.FindAll().ToList();
		}
	}
}