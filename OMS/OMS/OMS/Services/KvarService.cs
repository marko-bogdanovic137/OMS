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
    public class KvarService
    {
        private static readonly IKvarDAO kvarDAO = new KvarDAOImpl();

        public List<Kvar> FindAll()
        {
            return kvarDAO.FindAll().ToList();
        }
        public int Save(Kvar kvar)
        {
            return kvarDAO.Save(kvar);
        }

        public bool ExistById(string id)
        {
            return kvarDAO.ExistById(id);
        }

        public Kvar FindById(string id)
        {
            return kvarDAO.FindById(id);
        }

        public int Count()
        {
            return kvarDAO.Count();
        }

        public int DeleteById(string id)
        {
            return kvarDAO.DeleteById(id);
        }
    }
}
