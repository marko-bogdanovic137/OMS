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
    public class KvarDAOImpl : IKvarDAO
    {
        public int Count()
        {
            string query = "select COUNT(*) from evidencija";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }
        public bool ExistById(string id)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return ExistById(id, connection);
            }
        }

        private bool ExistById(string id, IDbConnection connection)
        {
            string query = "select * from evidencija where id=:id";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "id", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<Kvar> FindAll()
        {
            string query = "SELECT * from evidencija";
            List<Kvar> kvarList = new List<Kvar>();

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
                            Kvar kvar = new Kvar(reader.GetString(0), reader.GetDateTime(1),
                                reader.GetString(2));
                            kvarList.Add(kvar);
                        }
                    }
                }
            }
            return kvarList;
        }



        public int Save(Kvar kvar)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(kvar, connection);
            }
        }

        private int Save(Kvar kvar, IDbConnection connection)
        {
            string insertSql = "insert into evidencija (id, datum, status) values (:id, :datum, :status)";
            string updateSql = "update evidencija set datum=:datum, status=:status where id=:id";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistById(kvar.ID, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "id", DbType.String, 20);
                ParameterUtil.AddParameter(command, "datum", DbType.DateTime, 20);
                ParameterUtil.AddParameter(command, "status", DbType.String, 20);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id", kvar.ID);
                ParameterUtil.SetParameterValue(command, "datum", kvar.Datum);
                ParameterUtil.SetParameterValue(command, "status", kvar.Status);

                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<Kvar> kvars)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                int numSaved = 0;

                foreach (Kvar kvar in kvars)
                {
                    numSaved += Save(kvar, connection);
                }
                transaction.Commit();

                return numSaved;
            }
        }

        public Kvar FindById(string id)
        {
            string query = "select id, datum, status from evidencija where id=:id";
            Kvar kvar = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "id", DbType.String);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "id", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            kvar = new Kvar(reader.GetString(0), reader.GetDateTime(1), reader.GetString(2));
                        }
                    }
                }
            }
            return kvar;
        }
        public int Delete(Kvar kvar)
        {
            return DeleteById(kvar.ID);
        }

        public int DeleteById(string id)
        {
            string query = "delete from evidencija where id=:id";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "id", DbType.String);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "id", id);
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
