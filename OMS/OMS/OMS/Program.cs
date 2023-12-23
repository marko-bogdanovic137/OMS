using OMS.UIHandler;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS
{
    class Program
    {

        private static readonly KvarUIHandler mainUIHandler = new KvarUIHandler();

        static void Main(string[] args)
        {
            mainUIHandler.HandleKvarMenu();
        }
    }
}