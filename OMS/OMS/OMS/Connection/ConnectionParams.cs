using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Connection
{
    public class ConnectionParams
    {
        public static readonly string LOCAL_DATA_SOURCE = "//localhost:1521/xe";
        //public static readonly string CLASSROM_DATA_SOURCE_ = "//192.168.0.102:1522/db2016";

        public static readonly string USER_ID = "sys";
        public static readonly string PASSWORD = "1337";
        public static readonly string DBA_PRIVILEGE = "SYSDBA";
    }
}
