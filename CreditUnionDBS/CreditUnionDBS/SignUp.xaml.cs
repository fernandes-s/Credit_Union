using System;
using System.Collections;
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
using BIZ;
using DAL;

namespace CreditUnionDBS
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        HashCode hc = new HashCode();
        AddToDataBase addToDB = new AddToDataBase();
        RetrievingFromDataBase rtDB = new RetrievingFromDataBase();
        public SignUp()
        {
            InitializeComponent();
        }
        private void btnBackwards_SignIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Hide();
        }
        private void Login_click(object sender, RoutedEventArgs e)
        {
            SignIn login = new SignIn();
            login.Show();
            this.Hide();
        }


        private void Exit_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting the application!");
            this.Close();
        }

        private void btnSignUp_click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            if (rtDB.validUsername(username))
            {
                string password = hc.PassHash(txtPassword.Text);

                addToDB.addLoginDetais(username, password);

                MyAccount myAcc = new MyAccount();
                myAcc.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("This username is not available! Please, select a different one.");
                txtUsername.Focus();
                txtUsername.Clear();
            }
        }
    }
}
