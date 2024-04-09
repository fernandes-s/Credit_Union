using BIZ;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CreditUnionDBS
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {
        RetrievingFromDataBase rtDB = new RetrievingFromDataBase();

        public NewAccount()
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

        //Edit
        private void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            MyAccount myAcc = new MyAccount();
            myAcc.Show();
            this.Hide();
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

        private void btnCreateAcc_Click(object sender, RoutedEventArgs e)
        {
            int accNum = int.Parse(txtAccNum.Text);
            string firstName = txtFN.Text;
            string surname = txtSN.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address1 = txtAdd1.Text;
            string address2 = txtAdd2.Text;
            string city = txtCity.Text;
            string county = cboCounty.SelectedItem.ToString();
            string accType = "Current";
            int accountNumber = int.Parse(txtAccNum.Text);
            string username = firstName + surname;
            if (rdoSavings.IsChecked == true)
            {
                accType = "Savings";
            }
            int sortCode = int.Parse(txtSortCode.Text);

            decimal initialBalance = Balance();
            if (initialBalance > 0 && rtDB.ValidadeAccountNumber(accountNumber))
            {
                decimal overdraft = OverdraftCalculation(initialBalance);

                Account newAcc = new Account(username, firstName, surname, email, phone, address1, address2, city, county, accType, accountNumber, sortCode, initialBalance, overdraft);
                newAcc.CreateAccount();
                MyAccount myAcc = new MyAccount();
                myAcc.txtAccNum.Text = accNum.ToString();
                MessageBox.Show("Account successfully created!");
                myAcc.Show();
                this.Hide();
            }
            else
            {
                if(initialBalance <= 0)
                {
                    MessageBox.Show("Your initial balance must be greater than 0.");
                    txtInitialBalance.Focus();
                    txtInitialBalance.Clear();
                    txtOverdraftLimit.Text = "0";
                }
                else if(!rtDB.ValidadeAccountNumber(accountNumber)) {
                    MessageBox.Show("The chosen account number is already been used");
                    txtAccNum.Clear();
                }
            }
        }


        //Calculating the Overdraft Limit
        public decimal OverdraftCalculation(decimal initialBalance)
        {
            decimal overdraftLimit = 0;
            if (initialBalance == 0)
            {
                return overdraftLimit;
            }
            else overdraftLimit = initialBalance / 10;

            return overdraftLimit;
        }

        //Calculating and displaying overdraft value according to Balance value
        private void txtInitialBalance_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal overdraftLimit;
            if (rdoSavings.IsChecked == true)
            {
                overdraftLimit = 0;
                txtOverdraftLimit.Text = overdraftLimit.ToString();
            }
            else if (decimal.TryParse(txtInitialBalance.Text, out overdraftLimit))
            {
                txtOverdraftLimit.Text = OverdraftCalculation(overdraftLimit).ToString();
            }
            else txtOverdraftLimit.Clear();
        }

        //Populating Fields for when the form is initialized
        private void PopulatingFields()
        {
            txtSortCode.Text = ConfigurationManager.AppSettings.Get("SortCode");
            cboCounty.ItemsSource = Enum.GetValues(typeof(County));
        }

        //Checking if Balance value is acceptable
        private decimal Balance()
        {
            decimal initialBalance = 0;
            try
            {
                initialBalance = decimal.Parse(txtInitialBalance.Text);
                return initialBalance;
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert string to decimal! Please, enter a numerical value");
            }
        }

        //Setting overdraft limit to 0 if is a savings account
        private void rdoSavings_Checked(object sender, RoutedEventArgs e)
        {
            if (rdoSavings.IsChecked == true)
            {
                txtOverdraftLimit.Text = "0";
            }
        }

        //Setting overdraft if is a Current account
        private void rdoCurrent_Checked(object sender, RoutedEventArgs e)
        {
            if (rdoCurrent.IsChecked == true && txtInitialBalance.Text != "")
            {
                decimal overdraftLimit;
                if (decimal.TryParse(txtInitialBalance.Text, out overdraftLimit))
                {
                    txtOverdraftLimit.Text = OverdraftCalculation(overdraftLimit).ToString();
                }
                else txtOverdraftLimit.Text = "0";
            }
        }

        //Grid Load event
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PopulatingFields();
        }

    }
}
