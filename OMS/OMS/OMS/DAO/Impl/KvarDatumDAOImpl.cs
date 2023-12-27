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
using System.Data.SqlClient;

namespace OMS.DAO.Impl
{
	public class KvarDatumDAOImpl : IKvarDatumDAO
	{
		public IEnumerable<KvarDatum> FindAll(DateTime startDate, DateTime endDate)
		{
			string query = "SELECT e.ID, e.Datum, e.Status, k.kopis FROM evidencija e " +
			   "JOIN kvar_info k ON e.ID = k.ID " +
			   "WHERE e.Datum >= :startDate AND e.Datum <= :endDate";
			List<KvarDatum> kvarDatumList = new List<KvarDatum>();

			using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
			{
				connection.Open();
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = query;
					command.Parameters.Add(new OracleParameter("startDate", startDate));
					command.Parameters.Add(new OracleParameter("endDate", endDate));
					command.Prepare();

					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							KvarDatum kvarDatum = new KvarDatum(reader.GetString(0), reader.GetDateTime(1), reader.GetString(3), reader.GetString(2));
							kvarDatumList.Add(kvarDatum);
						}
					}
				}
			}
			return kvarDatumList;
		}
		public void FindKvar(string id)
		{
			string query = "SELECT e.*, k.kOpis, k.element, k.dOpis " +
			   "FROM evidencija e " +
			   "JOIN kvar_info k ON e.ID = k.ID " +
			   "WHERE e.ID = :idInput";

			 string GetFormattedHeader()
			{
				return string.Format("{0,-15} {1,-35} {2,-20} {3,-20} {4,-20} {5,-20}",
					"ID", "DATUM", "STATUS", "KOPIS", "ELEMENT", "DOPIS");
			}

			using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
			{
				connection.Open();
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = query;
					command.Parameters.Add(new OracleParameter("idInput", id));
					command.Prepare();

					Console.WriteLine(GetFormattedHeader());
					Console.WriteLine("-------------------------------------------------------" +
						"-----------------------------------------------------------------");

					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							 Console.WriteLine(string.Format("{0,-15} {1,-35} {2,-20} {3,-20} {4,-20}\n{5,-20}",
							 reader.GetValue(reader.GetOrdinal("ID")),
							 reader.GetDateTime(reader.GetOrdinal("DATUM")).ToString("dd.MM.yyyy"),
							 reader.GetValue(reader.GetOrdinal("STATUS")).ToString(),
							 reader.GetValue(reader.GetOrdinal("KOPIS")).ToString(),
							 reader.GetValue(reader.GetOrdinal("ELEMENT")).ToString(),
							 reader.GetValue(reader.GetOrdinal("DOPIS")).ToString()));
						}
					}
				}
			}
		}

	}
}
