using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddToDataBase : DAO
    {
        public void addLoginDetais(string username, string password)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspInsertLoginDetails";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            cmd.ExecuteNonQuery();
            CloseCon();
        }
    }
}
