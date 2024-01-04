using OMS.DAO.Impl;
using OMS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace OMS.Services
{
    public class PrenosService
    {
        private static readonly IPrenosPodataka prenosDAO = new ExcelTabelaImpl();

        public bool Prenos()
        {
            return prenosDAO.Prenesi();
        }


    }
}
