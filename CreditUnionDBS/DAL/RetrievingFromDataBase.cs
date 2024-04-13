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

        public string validLogn(string name, string password)
        {

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspCheckPassword";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user", name);
            cmd.Parameters.AddWithValue("@pass", password);
            string exists = cmd.ExecuteScalar().ToString();
            CloseCon();

            return exists;

        }
        
        public int countingAccounts()
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspCountAccount";
            cmd.CommandType = CommandType.StoredProcedure;

            int count = int.Parse(cmd.ExecuteScalar().ToString());
            CloseCon();

            return count;
        }

        public DataTable allAccounts()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspAllAccounts";
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        


        public bool ValidadeAccountNumber(int accountNumber)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspValidadeAccountNumber";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
            int transferId = int.Parse(cmd.ExecuteScalar().ToString());
            CloseCon();

            return transferId == 0 ? true : false;
        }

        public SqlDataReader CollectAccNumber(int accountId)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspCollectAccNum";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", accountId);

            return cmd.ExecuteReader();
        }

        public DataTable FilterByAccType(string accType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspFilterByAccType";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accType", accType);

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        public DataTable GetWithdrawals(string transType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspGetWithdrawals";
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        public DataTable GetDeposits(string transType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspGetDeposits";
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        public DataTable GetTransfers(string transType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspGetTransfer";
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }
    }
}
