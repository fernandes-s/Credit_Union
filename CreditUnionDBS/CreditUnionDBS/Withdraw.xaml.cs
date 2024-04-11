using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL;

namespace CreditUnionDBS
{
    /// <summary>
    /// Interaction logic for Withdraw.xaml
    /// </summary>
    public partial class Withdraw : Window
    {
        private int accoNum = 0;
        SqlDataReader dr;
        DAO dao = new DAO();
        private decimal overdraft = 0;
        AddToDataBase addToDB = new AddToDataBase();
        RetrievingFromDataBase rtDB = new RetrievingFromDataBase();
        public Withdraw()
        {
            InitializeComponent();
        }
        //Menu itens click events
        //Login
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logged out sucessfully!");
            SignIn login = new SignIn();
            login.Show();
            this.Hide();
        }

        //Exit
        private void Exit_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting the application!");
            this.Close();
        }

        //New Account
        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {
            NewAccount nac = new NewAccount();
            nac.Show();
            this.Hide();
        }

        //Edit
        private void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            MyAccount myAcc = new MyAccount();
            myAcc.Show();
            this.Hide();
        }

        //Deposit
        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            Deposit d = new Deposit();
            d.Show();
            this.Hide();
        }

        //Transfer
        private void TransferFunds_Click(object sender, RoutedEventArgs e)
        {
            transfer trans = new transfer();
            trans.Show();
            this.Hide();
        }

        //Transactions
        private void ViewTransactions_Click(object sender, RoutedEventArgs e)
        {
            transactions transac = new transactions();
            transac.Show();
            this.Hide();
        }

        //----------------------------------------------------------------------------------

        //Grid load event
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateComboBox();
        }

        //Withdraw button
        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            decimal balance = decimal.Parse(txtBalance.Text);
            decimal withdrawAmt = 0;
            decimal newBal = 0;
            string accType = txtAccType.Text;

            try
            {
                withdrawAmt = decimal.Parse(txtAmount.Text);
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert string to decimal! You must enter a number.");
            }

            if (withdrawAmt <= 0)
            {
                MessageBox.Show("Your withdraw amount must be greater than 0!");
            }
            else if (withdrawAmt > balance + overdraft)
            {
                MessageBox.Show("Insufficient funds!");
            }
            else
            {
                newBal = newBalance(balance, overdraft, withdrawAmt);
                decimal newOverdraft = calculatingNewOverdraft(newBal);
                addToDB.UpdateBalanceAndOverdraft(newBal, newOverdraft, accoNum);
                addToDB.NewWithdraw(accoNum, accType, balance, withdrawAmt, newBal);
                MessageBox.Show($"Amount Withdrawn: {withdrawAmt}\nNew Balance: {newBal}");
                txtAmount.Clear();
                txtBalance.Text = newBal.ToString();
            }



        }

        ////Pre-populating fields
        //public void MyAccountDetails()
        //{
        //    accoNum = int.Parse(cboWithdraw.SelectedItem.ToString());

        //    string accType = "";
        //    decimal bal = 0;

        //    SqlCommand cmd = dao.OpenCon().CreateCommand();
        //    cmd.CommandText = "uspMyAccountDetails";
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@accNum", accoNum);
        //    dr = cmd.ExecuteReader();

        //    while (dr.Read())
        //    {
        //        accType = dr["AccountType"].ToString();
        //        bal = decimal.Parse(dr["InitialBalance"].ToString());
        //        overdraft = decimal.Parse(dr["OverdraftLimit"].ToString());
        //    }
        //    dao.CloseCon();

        //    txtBalance.Text = bal.ToString("F2");
        //    txtAccType.Text = accType;


        //}

        public void PopulateComboBox()
        {
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspSelectAccNum";
            cmd.CommandType = CommandType.StoredProcedure;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int acc = int.Parse(dr["AccountId"].ToString());
                int accountNumber = int.Parse(dr["AccountNumber"].ToString());
                if (acc != accoNum)
                {
                    cboWithdraw.Items.Add(accountNumber);
                }
            }

            dao.CloseCon();
        }

    

        public void DisplayingInfo(int accNum)
        {
            string accType = "";
            string balance = "";
            string username = "";
            // possible solution saved there, please double check (new procedure)
            using (SqlDataReader dataReader = rtDB.CollectAccNumber(accNum))
            {
                while (dataReader.Read())
                {
                    accType = dataReader["AccountType"].ToString();
                    balance = dataReader["InitialBalance"].ToString();
                    username = dataReader["Username"].ToString();
                }
            }
            dao.CloseCon();

            txtAccType.Text = accType;
            txtBalance.Text = balance;
            txtUsername.Text = username;
        }

        //Calculating value of new balance
        public decimal newBalance(decimal bal, decimal overdraft, decimal withdraw)
        {
            decimal newBal = 0;

            if (withdraw <= bal)
            {
                newBal = bal - withdraw;
                return newBal;
            }
            else newBal = (bal + overdraft) - withdraw;

            return newBal;
        }

        //Calculating new overdraft value
        public decimal calculatingNewOverdraft(decimal bal)
        {
            decimal newOverdraft = bal / 10;
            return newOverdraft;
        }
        private void cboWithdraw_Selectionchanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayingInfo(int.Parse(cboWithdraw.SelectedItem.ToString()));
        }

        
    }
}
