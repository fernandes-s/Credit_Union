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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
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

        }

        private void SignUp_click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
