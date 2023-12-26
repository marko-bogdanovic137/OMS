using OMS.ConnectionPool;
using OMS.Model;
using OMS.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.DAO.Impl
{
	public class AkcijeDAOImpl : IAkcijeDAO
	{
		public IEnumerable<Akcija> FindAll()
		{
			string query = "SELECT * from Akcije";
			List<Akcija> akcijaList = new List<Akcija>();

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
							Akcija akcija = new Akcija(reader.GetString(0), reader.GetString(1),
								reader.GetString(2));
							akcijaList.Add(akcija);
						}
					}
				}
			}
			return akcijaList;
		}




		public int Save(Akcija akcija)
		{
			using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
			{
				connection.Open();
				return Save(akcija, connection);
			}
		}

		private int Save(Akcija akcija, IDbConnection connection)
		{
			string insertSql = "insert into Akcije (id, datum, opisPosla) values (:id, :datum, :opisPosla)";

			using (IDbCommand command = connection.CreateCommand())
			{
				command.CommandText = insertSql;
				ParameterUtil.AddParameter(command, "id", DbType.String, 20);
				ParameterUtil.AddParameter(command, "datum", DbType.String, 20);
				ParameterUtil.AddParameter(command, "opisPosla", DbType.String, 50);
				command.Prepare();
				ParameterUtil.SetParameterValue(command, "id", akcija.ID);
				ParameterUtil.SetParameterValue(command, "datum", akcija.Date);
				ParameterUtil.SetParameterValue(command, "opisPosla", akcija.OpisPosla);

				return command.ExecuteNonQuery();
			}
		}
	}
}
