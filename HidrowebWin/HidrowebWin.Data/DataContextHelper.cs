using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using HidrowebWin.Data.Properties;

namespace HidrowebWin.Data
{
    public class DataContextHelper
    {
        public static IEnumerable<HIDRODataSet.Chuvas24Row> EstacaoChuvas(int codEstacao)
        {
            var connection = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source=|DataDirectory|\HIDRO.mdb");

            string query = "select * from Chuvas24 where EstacaoCodigo=2043003";

            connection.Open();

            var command = new OleDbCommand(query, connection);

            var result = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            var has = result.HasRows;

         
            while (result.Read())
            {
                var final = result[0];
            }

            return null;
        }
    }
}
