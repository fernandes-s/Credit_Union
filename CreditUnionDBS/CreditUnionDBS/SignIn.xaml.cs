using DAL;
using BIZ;
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

namespace CreditUnionDBS
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        RetrievingFromDataBase rtDB = new RetrievingFromDataBase();
        HashCode hc = new HashCode();

        public SignIn()
        {
            InitializeComponent();
        }
        private void btnBackwards_SignIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Hide();
        }
        private void btnSignIn_click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = hc.PassHash(pbPassword.Password);
            string exists = rtDB.validLogn(username, password);

            if (exists.Equals("true"))
            {
                MessageBox.Show("Sucessfully logged in");
                MyAccount myAcc = new MyAccount();
                myAcc.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username and passowrd. Please try again");
                txtUsername.Clear();
                pbPassword.Clear();
            }
        }
        private void Exit_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting the application!");
            this.Close();
        }
        private void Login_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You are already on the Login screen!");
        }

        private void menuItemFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
