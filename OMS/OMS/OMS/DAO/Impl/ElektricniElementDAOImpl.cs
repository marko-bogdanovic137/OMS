using OMS.ConnectionPool;
using OMS.Model;
using OMS.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace OMS.DAO.Impl
{
    public class ElektricniElementDAOImpl : IElektricniElementDAO
    {
        public int Count()
        {
            string query = "select (*) from elektricni_element";

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
            string query = "select * from elektricni_element where id=:id";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "id", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<ElektricniElement> FindAll()
        {
            string query = "select id, naziv, tip, geolokacija, naponski_nivo from elektricni_element";
            List<ElektricniElement> elektricniElementList = new List<ElektricniElement>();

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
                            ElektricniElement elektricniElement = new ElektricniElement(reader.GetString(0), reader.GetString(1),
                                reader.GetString(2), reader.GetString(3), reader.GetString(4));
                            elektricniElementList.Add(elektricniElement);
                        }
                    }
                }
            }
            return elektricniElementList;
        }



        public int Save(ElektricniElement elektricniElement)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(elektricniElement, connection);
            }
        }

        private int Save(ElektricniElement elektricniElement, IDbConnection connection)
        {
            string insertSql = "insert into elektricni_element (id, naziv, tip, geolokacija, naponski_nivo) values (:id, :naziv, :tip, :geolokacija, :naponski_nivo)";
            string updateSql = "update elektricni_element set id=:id, naziv=:naziv, tip=:tip, geolokacija=:geolokacija, naponski_nivo=:naponski_nivo";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistById(elektricniElement.ID, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "id", DbType.String, 50);
                ParameterUtil.AddParameter(command, "naziv", DbType.String, 50);
                ParameterUtil.AddParameter(command, "tip", DbType.String, 50);
                ParameterUtil.AddParameter(command, "geolokacija", DbType.String, 50);
                ParameterUtil.AddParameter(command, "naponski_nivo", DbType.String, 50);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id", elektricniElement.ID);
                ParameterUtil.SetParameterValue(command, "naziv", elektricniElement.Naziv);
                ParameterUtil.SetParameterValue(command, "tip", elektricniElement.Tip);
                ParameterUtil.SetParameterValue(command, "geolokacija", elektricniElement.GeoLokacija);
                ParameterUtil.SetParameterValue(command, "naponski_nivo", elektricniElement.NaponskiNivo);
                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<ElektricniElement> elektricniElements)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                int numSaved = 0;

                foreach (ElektricniElement elektricniElement in elektricniElements)
                {
                    numSaved += Save(elektricniElement, connection);
                }
                transaction.Commit();

                return numSaved;
            }
        }

        public ElektricniElement FindById(string id)
        {
            string query = "select id, naziv, tip, geolokacija, naponski_nivo from elektricni_element where id=:id";
            ElektricniElement elektricniElement = null;

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
                            elektricniElement = new ElektricniElement(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                                reader.GetString(3), reader.GetString(4));
                        }
                    }
                }
            }
            return elektricniElement;
        }
        public int Delete(ElektricniElement element)
        {
            return DeleteById(element.ID);
        }

        public int DeleteById(string id)
        {
            string query = "delete from elektricni_element where id=:id";

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


        public bool IspisiTip()
        {
            string query = "select tip from elektricni_element";
            List<string> tipovi = new List<string>();
            string tip = "";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tip = reader.GetString(0);
                            tipovi.Add(tip);
                        }

                    }
                }
            }
            SnimiEvidenciju(tipovi);
            return true;
        }
        public const string filePath = @"C:\Users\Duska\Desktop\PROJJJ\OMS\OMS\OMS\TipoviElemenata.txt";

        public static void SnimiEvidenciju(List<string> tipovi)
        {
            string[] lines = tipovi.ToArray();
            File.WriteAllLines(filePath, lines);
            Console.WriteLine("Tipovi elemenata su izvučeni iz baze i sačuvani u fajlu.");
        } 
    }
}

