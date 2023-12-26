using OMS.DAO.Impl;
using OMS.DAO;
using OMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Service
{
    public class ElektricniElementService
    {
        private static readonly IElektricniElementDAO elektricniElementDAO = new ElektricniElementDAOImpl();

        public List<ElektricniElement> FindAll()
        {
            return elektricniElementDAO.FindAll().ToList();
        }
        public int Save(ElektricniElement elektricniElement)
        {
            return elektricniElementDAO.Save(elektricniElement);
        }

        public bool ExistsById(string id)
        {
            return elektricniElementDAO.ExistById(id);
        }

        public ElektricniElement FindById(string id)
        {
            return elektricniElementDAO.FindById(id);
        }

    }
}
