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
        //Create Login 
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

        //Update Acc
        public void updateAccountDetails(int accNum, string email, string phone,
            string add1, string add2, string city, string cy)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspUpdateAccount";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNum);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@add1", add1);
            cmd.Parameters.AddWithValue("@add2", add2);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@cy", cy);

            cmd.ExecuteNonQuery();
            CloseCon();

        }

        // Create Acc
        public void CreateAccount(string user, string fn, string sn, string email,
            string phone, string add1, string add2, string city, string cy, string accType,
            int sortCode, decimal bal, decimal overdraft)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspCreateAccount";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", user);
            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@sn", sn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@add1", add1);
            cmd.Parameters.AddWithValue("@add2", add2);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@county", cy);
            cmd.Parameters.AddWithValue("@accType", accType);
            cmd.Parameters.AddWithValue("@sortCode", sortCode);
            cmd.Parameters.AddWithValue("@bal", bal);
            cmd.Parameters.AddWithValue("@overdraft", overdraft);

            cmd.ExecuteNonQuery();
            CloseCon();

        }






    }
}
