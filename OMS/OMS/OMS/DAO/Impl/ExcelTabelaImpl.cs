using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OMS.ConnectionPool;
using System.Data;
using OMS.Utils;

namespace OMS.DAO.Impl
{
    public class ExcelTabelaImpl : IPrenosPodataka
    {
        public bool Prenesi()
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var excelPackage = new ExcelPackage();
            var workSheet = excelPackage.Workbook.Worksheets.Add("Kvarovi");

            workSheet.Cells[1, 1].Value = "ID kvara";
            workSheet.Cells[1, 2].Value = "Naziv elementa";
            workSheet.Cells[1, 3].Value = "Naponski nivo";
            workSheet.Cells[1, 4].Value = "Opis Posla";


            string query1 = "SELECT ID FROM evidencija";
            string query2 = "SELECT element FROM kvar_info";
            string query3 = "SELECT naponski_nivo FROM elektricni_element";
            string query4 = "SELECT OpisPosla FROM Akcije";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query1;

                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {

                        int row = 2;

                        while (reader.Read())
                        {
                            workSheet.Cells[row, 1].Value = reader["ID"];

                            row++;
                        }
                    }

                    command.CommandText = query2;

                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {

                        int row = 2;

                        while (reader.Read())
                        {
                            workSheet.Cells[row, 2].Value = reader["element"];

                            row++;
                        }
                    }

                    command.CommandText = query3;

                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {

                        int row = 2;

                        while (reader.Read())
                        {
                            workSheet.Cells[row, 3].Value = reader["naponski_nivo"];

                            row++;
                        }
                    }

                    command.CommandText = query4;

                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {

                        int row = 2;

                        while (reader.Read())
                        {

                            workSheet.Cells[row, 4].Value = reader["OpisPosla"];
                            row++;
                        }
                    }
                }
            }


            FileInfo excelFile = new FileInfo(@"C:\Users\Duska\Desktop\PROJ\OMS\OMS\OMS\KvaroviExcel.xlsx");
            excelPackage.SaveAs(excelFile);

            return true;
        }
        

     }   
    
}
