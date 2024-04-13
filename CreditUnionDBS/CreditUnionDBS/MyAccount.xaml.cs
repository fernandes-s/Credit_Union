using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Security.Policy;
using DAL;
using BIZ;
using System.Xml.Serialization;
using System.Xml;
using System.Security.AccessControl;


namespace CreditUnionDBS
{
    /// <summary>
    /// Interaction logic for MyAccount.xaml
    /// </summary>
    public partial class MyAccount : Window
    {
        DAO dao = new DAO();
        SqlDataReader dr;
        AddToDataBase addToDB = new AddToDataBase();
        RetrievingFromDataBase rtDB = new RetrievingFromDataBase();
        CollectionViewSource cs = new CollectionViewSource();
        private int accoNum = 0;

        public MyAccount()
        {
            InitializeComponent();
        }


        //Menu itens click event
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logged out sucessfully!");
            SignIn login = new SignIn();
            login.Show();
            this.Hide();
        }

        // Exit
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting the application!");
            this.Close();
        }

        //Show
        //NewAccount
        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {
            NewAccount acc = new NewAccount();
            acc.Show();
            this.Hide();
        }

        //Edit
        private void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            MyAccount myAcc = new MyAccount();
            //myAcc.txtAccNum.Text = accoNum.ToString();
            myAcc.Show();
            myAcc.Close();
        }

        //Deposit
        private void DepositFunds_Click(object sender, RoutedEventArgs e)
        {
            Deposit dep = new Deposit();
            dep.Show();
            this.Hide();
        }

        //Withdraw
        private void WithdrawFunds_Click(object sender, RoutedEventArgs e)
        {
            Withdraw w = new Withdraw();
            w.Show();
            this.Hide();
        }

        // Transfer
        private void TransferFunds_Click(object sender, RoutedEventArgs e)
        {
            transfer trans = new transfer();
            trans.Show();
            this.Hide();
        }

        // Transactions
        private void ViewTransactions_Click(object sender, RoutedEventArgs e)
        {
            transactions transac = new transactions();
            transac.Show();
            this.Hide();
        }

        //---------------------------------------------------------------------------

        //Btn Edit Click event
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            txtFullName.IsEnabled = false;
            txtEmail.IsReadOnly = false;
            txtPhone.IsReadOnly = false;
            txtAdd1.IsReadOnly = false;
            txtAdd2.IsReadOnly = false;
            txtCity.IsReadOnly = false;
            txtCy.IsReadOnly = false;
            txtAccType.IsEnabled = false;
            txtAccNum.IsEnabled = false;
            txtSortCode.IsEnabled = false;
            txtBal.IsEnabled = false;
            txtOverdraft.IsEnabled = false;
            btnUpdate.IsEnabled = true;
        }


        //Filling Account Details
        public void MyAccountDetails(int num)
        {
            int accNum = num;

            string fn = "";
            string sn = "";
            string email = "";
            string phone = "";
            string add1 = "";
            string add2 = "";
            string city = "";
            string cy = "";
            string accType = "";
            string sortCode = "";
            decimal bal = 0;
            decimal overdraft = 0;



            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspMyAccountDetails";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNum);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                fn = dr["Firstname"].ToString();
                sn = dr["Surname"].ToString();
                email = dr["Email"].ToString();
                phone = dr["Phone"].ToString();
                add1 = dr["AddressLine1"].ToString();
                add2 = dr["AddressLine2"].ToString();
                city = dr["City"].ToString();
                cy = dr["County"].ToString();
                accType = dr["AccountType"].ToString();
                accoNum = int.Parse(dr["AccountID"].ToString());
                sortCode = dr["SortCode"].ToString();
                bal = decimal.Parse(dr["InitialBalance"].ToString());
                overdraft = decimal.Parse(dr["OverdraftLimit"].ToString());
            }
            dao.CloseCon();

            txtFullName.Text = $"{fn} {sn}";
            txtEmail.Text = email;
            txtPhone.Text = phone;
            txtCity.Text = city;
            txtAdd1.Text = add1;
            txtAdd2.Text = add2;
            txtCity.Text = city;
            txtSortCode.Text = sortCode;
            txtCy.Text = cy;
            txtOverdraft.Text = overdraft.ToString("F2");
            txtBal.Text = bal.ToString("F2");
            txtAccNum.Text = accoNum.ToString();
            txtAccType.Text = accType;


        }

        //Filling Data Grid View
        private void FillingDataGrid()
        {
            int count = rtDB.countingAccounts();

            if (count != 0)
            {
                cs.Source = rtDB.allAccounts().DefaultView;
                dgvAccounts.ItemsSource = cs.View;
            }

        }

        //Grid Load event
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //MyAccountDetails();
            FillingDataGrid();
        }

        



        //Updating account information
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string add1 = txtAdd1.Text;
            string add2 = txtAdd2.Text;
            string city = txtCity.Text;
            string county = txtCy.Text;

            addToDB.updateAccountDetails(accoNum, email, phone, add1, add2, city, county);
            FillingDataGrid();
            MessageBox.Show("Account Succesfully Uptdated!");

            txtEmail.IsReadOnly = true;
            txtCity.IsReadOnly = true;
            txtAdd1.IsReadOnly = true;
            txtAdd2.IsReadOnly = true;
            txtCy.IsReadOnly = true;
            txtPhone.IsReadOnly = true;
            txtBal.IsEnabled = true;
            txtAccType.IsEnabled = true;
            txtOverdraft.IsEnabled = true;
            txtSortCode.IsEnabled = true;
            txtFullName.IsEnabled = true;
            txtAccNum.IsEnabled = true;
            btnUpdate.IsEnabled = false;
        }

        //Show Accounts click event
        private void btnShowAccounts_Click(object sender, RoutedEventArgs e)
        {
            FillingDataGrid();
        }

        

        //Changing row colors according to operation type
        private void dgvAccounts_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[2].ToString().Equals("Deposit")))
                {
                    e.Row.Background = new SolidColorBrush(Colors.LightGreen);
                }
                else if (Convert.ToBoolean(((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[2].ToString().Equals("Withdraw")))
                {
                    e.Row.Background = new SolidColorBrush(Colors.IndianRed);
                }
                else if (Convert.ToBoolean(((System.Data.DataRowView)(e.Row.DataContext)).Row.ItemArray[2].ToString().Equals("Transfer")))
                {
                    e.Row.Background = new SolidColorBrush(Colors.IndianRed);
                }

            }
            catch { }
        }

        //Populating fields when Selected Row changes
        private void dgvAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dv = (DataRowView)dgvAccounts.SelectedItem;


            if (dv != null)
            {
                accoNum = int.Parse(dv.Row.ItemArray[0].ToString());

            }

            MyAccountDetails(accoNum);
        }




        // Serialise & Deserialise
        private void btnSerialise_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
