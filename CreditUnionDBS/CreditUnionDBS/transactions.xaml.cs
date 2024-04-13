using System;
using System.Collections.Generic;
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
    /// Interaction logic for transactions.xaml
    /// </summary>
    public partial class transactions : Window
    {

        private int accoNum = 0;
        RetrievingFromDataBase rtDB = new RetrievingFromDataBase();
        CollectionViewSource cs = new CollectionViewSource();

        public transactions()
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

        //Transfer
        private void TransferFunds_Click(object sender, RoutedEventArgs e)
        {
            transfer trans = new transfer();
            trans.Show();
            this.Hide();
        }

        //--------------------------------------------------------


        //Grid Load event
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            dgvTransactions.ItemsSource = cs.View;
            cboFilter.ItemsSource = Enum.GetValues(typeof(Filters));
        }

        private void cboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = cboFilter.SelectedItem.ToString();
            switch (selectedItem)
            {
                case "Current":
                    selectedItem = "Current";
                    cs.Source = rtDB.FilterByAccType(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
                case "Savings":
                    cs.Source = rtDB.FilterByAccType(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
                case "Deposit":
                    cs.Source = rtDB.GetDeposits(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
                case "Withdraw":
                    cs.Source = rtDB.GetWithdrawals(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
                case "Transfer":
                    cs.Source = rtDB.GetTransfers(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
            }

        }
        
    }
}
