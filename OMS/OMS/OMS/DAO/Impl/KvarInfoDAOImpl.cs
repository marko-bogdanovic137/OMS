using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using OMS.ConnectionPool;
using OMS.Model;
using OMS.Utils;

namespace OMS.DAO.Impl
{
    public class KvarInfoDAOImpl : IKvarInfoDAO
    {
		public IEnumerable<KvarInfo> FindAll()
		{
			string query = "SELECT * from kvar_info";
			List<KvarInfo> kvarInfoList = new List<KvarInfo>();

			using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
			{
				connection.Open();
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = query;
					command.Prepare();

					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							KvarInfo kvarInfo = new KvarInfo(reader.GetString(0), reader.GetString(1),
								reader.GetString(2), reader.GetString(3));
							kvarInfoList.Add(kvarInfo);
						}
					}
				}
			}
			return kvarInfoList;
		}
		public int Save(KvarInfo kvarInfo)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(kvarInfo, connection);
            }
        }

        private int Save(KvarInfo kvarInfo, IDbConnection connection)
        {
            string insertSql = "insert into kvar_info (id, kopis, element, dopis) values (:id, :kopis, :element, :dopis)";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = insertSql;
                ParameterUtil.AddParameter(command, "id", DbType.String, 20);
                ParameterUtil.AddParameter(command, "kopis", DbType.String, 20);
                ParameterUtil.AddParameter(command, "element", DbType.String, 20);
                ParameterUtil.AddParameter(command, "dopis", DbType.String, 50);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id", kvarInfo.ID);
                ParameterUtil.SetParameterValue(command, "kopis", kvarInfo.kOpis);
                ParameterUtil.SetParameterValue(command, "element", kvarInfo.element);
                ParameterUtil.SetParameterValue(command, "dopis", kvarInfo.dOpis);

                return command.ExecuteNonQuery();
            }
        }
    }
}
