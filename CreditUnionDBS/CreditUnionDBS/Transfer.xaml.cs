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
using System.Configuration;
using DAL;

namespace CreditUnionDBS
{
    /// <summary>
    /// Interaction logic for transfer.xaml
    /// </summary>
    public partial class transfer : Window
    {
        private int accoNum = 0;
         decimal overdraft = 0;
        private decimal receiverBalance = 0;
        SqlDataReader dr;
        DAO dao = new DAO();
        AddToDataBase addToDB = new AddToDataBase();
        RetrievingFromDataBase rtDB = new RetrievingFromDataBase();
        public transfer()
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

        //Withdraw
        private void WithdrawFunds_Click(object sender, RoutedEventArgs e)
        {
            Withdraw w = new Withdraw();
            w.Show();
            this.Hide();
        }


        //Transactions
        private void ViewTransactions_Click(object sender, RoutedEventArgs e)
        {
            transactions transac = new transactions();
            transac.Show();
            this.Hide();
        }



        //-------------------------------------------------------------------------------

        //Grid Load Event
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateComboBox();
            txtDate.Text = DateTime.Now.ToString();
            txtSortCode.Text = ConfigurationManager.AppSettings.Get("SortCode");
        }


        //Populating sender details
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
                    cboAccFrom.Items.Add(accountNumber);
                    cboAccTo.Items.Add(accountNumber);
                }
            }

            dao.CloseCon();
        }

        public void DisplayingInfoSender(int accNum)
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
            txtBal.Text = balance;
            txtUsername.Text = username;
        }


        private void cboAccFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayingInfoSender(int.Parse(cboAccFrom.SelectedItem.ToString()));
        }


        //----------------------------------------------------------------------------------------------
        //Populating Receiver Details 
        public void DisplayingInfoReciever(int accNum)
        {
            string accType = "";
            string username = "";
            string balance = "";

            using (SqlDataReader dataReader = rtDB.CollectAccNumber(accNum))
            {
                while (dataReader.Read())
                {
                    accType = dataReader["AccountType"].ToString();
                    username = dataReader["Username"].ToString();
                    balance = dataReader["InitialBalance"].ToString();
                }
            }
            dao.CloseCon();

            txtReceiverAccType.Text = accType;
            txtUsernameReciever.Text = username;
            txtBalReceiver.Text = balance;
        }
        private void cboAccTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DisplayingInfoReciever(int.Parse(cboAccTo.SelectedItem.ToString()));
        }





        //Transfering funds
        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            decimal newBal = 0;
            string senderAccType = txtAccType.Text;
            int senderAccNum = int.Parse(cboAccFrom.SelectedValue.ToString());
            decimal bal = decimal.Parse(txtBal.Text);

            string receiverAccType = txtReceiverAccType.Text;
            int receiverAccNum = int.Parse(cboAccTo.SelectedItem.ToString());
            decimal receiverBalance = decimal.Parse(txtBalReceiver.Text);

            int sortCode = int.Parse(txtSortCode.Text);
            DateTime date = DateTime.Now;
            decimal amount;
            try
            {
                amount = decimal.Parse(txtAmount.Text);
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert string to decimal!");
            }

            if (amount <= 0)
            {
                MessageBox.Show("You must transfer a value greater than 0.");
                txtAmount.Clear();
                txtAmount.Focus();
            }
            else if (amount > bal + overdraft)
            {
                MessageBox.Show("Insufficient Funds!");
                txtAmount.Clear();
                txtAmount.Focus();
            }
            else
            {
                //Adding Transfer to Tranfer Table
                addToDB.NewTransfer(accoNum, senderAccType, bal, receiverAccNum, receiverAccType, sortCode, amount,  date);
                //Confirming and Tidying up
                MessageBox.Show($"{amount} has been transferred to {receiverAccNum}.");

                //Internal transfer updates sender and receiver account
                if (sortCode == 101010)
                {
                    //Updating sender balance and overdraft in the database
                    newBal = senderNewBalance(bal, overdraft, amount);
                    overdraft = calculatingOverdraft(newBal);
                    addToDB.UpdateBalanceAndOverdraft(newBal, overdraft, senderAccNum);

                    //Updating receiver balance and overdraft in the database
                    bal = receiverBalance + amount; 
                    overdraft = calculatingOverdraft(bal);
                    addToDB.UpdateBalanceAndOverdraft(bal, overdraft, receiverAccNum);
                }
                //External transfer, uptdates only sender account
                else
                {
                    //Updating sender balance and overdraft in the database
                    newBal = senderNewBalance(bal, overdraft, amount); 
                    overdraft = calculatingOverdraft(newBal);
                    addToDB.UpdateBalanceAndOverdraft(newBal, overdraft, senderAccNum);
                }

                //Tidying up
                txtBal.Text = newBal.ToString();
                txtBalReceiver.Text = bal.ToString(); 
                txtReceiverAccType.Clear();
                txtSortCode.Clear();
                txtAmount.Clear();
                txtDate.Text = DateTime.Now.ToString();
                cboAccTo.SelectedIndex = 0;

            }


        }

        //Calculating sender new balance
        public decimal senderNewBalance(decimal bal, decimal overdraft, decimal amount)
        {
            decimal newBalance = 0;

            if (amount <= bal)
            {
                newBalance = bal - amount;
                return newBalance;
            }
            else newBalance = (bal + overdraft) - amount;

            return newBalance;
        }

        //Calculating new overdraft value
        public decimal calculatingOverdraft(decimal bal)
        {
            return bal / 10;
        }

    }
}
