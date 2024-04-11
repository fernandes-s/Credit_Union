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
    /// Interaction logic for Deposit.xaml
    /// </summary>
    public partial class Deposit : Window
    {
        private decimal overdraft = 0;
        private int accoNum = 0;
        SqlDataReader dr;
        DAO dao = new DAO();
        AddToDataBase addToDb = new AddToDataBase();
        RetrievingFromDataBase rtDB = new RetrievingFromDataBase();
        //List <int> accountNumbers = new List <int>();
        public Deposit()
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

        //Withdraw
        private void WithdrawFunds_Click(object sender, RoutedEventArgs e)
        {
            Withdraw w = new Withdraw();
            w.Show();
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

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            decimal depositAmt = 0;
            decimal balance = decimal.Parse(txtBalance.Text);
            try
            {
                depositAmt = decimal.Parse(txtAmount.Text);
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert string to decimal! You must enter a number.");
            }

            if (depositAmt <= 0)
            {
                MessageBox.Show("Your deposit amount must be greater than 0.");
            }
            else
            {
                string accType = txtAccType.Text;
                decimal newBal = newBalance(balance, depositAmt);
                overdraft = newOverdraft(newBal);
                addToDb.UpdateBalanceAndOverdraft(newBal, overdraft, int.Parse(cboDeposit.SelectedValue.ToString()));
                addToDb.NewDeposit(int.Parse(cboDeposit.SelectedValue.ToString()), accType, balance, depositAmt, newBal);
                MessageBox.Show($"Successfully deposited {depositAmt} in your account!\nNew Balance: {newBal}");
                txtAmount.Clear();
                txtBalance.Text = newBal.ToString();
            }
        }

        //Calculating new Balance
        private decimal newBalance(decimal bal, decimal depAmt)
        {
            return bal + depAmt;
        }

        //Calculating new overdraft value
        private decimal newOverdraft(decimal bal)
        {
            return bal / 10;
        }

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
                    cboDeposit.Items.Add(accountNumber);
                }
            }

            dao.CloseCon();
        }

        public void depositGrid_Loader(object sender, EventArgs e)
        {
            PopulateComboBox();
        }

        public void cboAccNumber_selectionChanged(object sender, EventArgs e)
        {
            DisplayingInfo(int.Parse(cboDeposit.SelectedItem.ToString()));
        }

        public void DisplayingInfo(int accNum)
        {
            string accType = "";
            string balance = "";
            string username = "";

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

    }
}
