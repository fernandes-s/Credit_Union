using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace DAL
{
    public class RetrievingFromDataBase : DAO
    {
        SqlDataReader dr;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();

        public bool validUsername(string username)
        {
            int rowCount = 0;

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspFreeUsername";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            rowCount = (int)cmd.ExecuteScalar();
            CloseCon();

            return (rowCount == 0);
        }



    }
}
