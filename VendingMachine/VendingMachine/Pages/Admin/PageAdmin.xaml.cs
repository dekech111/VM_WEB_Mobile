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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VendingMachine.Data;
using VendingMachine.Pages;

namespace VendingMachine.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        public PageAdmin()
        {
            InitializeComponent();
        }

        private void btnCoins_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameObj.Navigate(new PageCoins());
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameObj.Navigate(new PageReport());
        }

        private void btnDrinks_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameObj.Navigate(new PageDrinks());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameObj.GoBack();
        }
    }
}
