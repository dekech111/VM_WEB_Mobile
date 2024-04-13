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

namespace VendingMachine.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageCoins.xaml
    /// </summary>
    public partial class PageCoins : Page
    {
        public PageCoins()
        {
            InitializeComponent();

            tbCoin1.Text = ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 1).Select(x => x.Count).ToList()[0].ToString();
            tbCoin2.Text = ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 2).Select(x => x.Count).ToList()[0].ToString();
            tbCoin5.Text = ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 3).Select(x => x.Count).ToList()[0].ToString();
            tbCoin10.Text = ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 4).Select(x => x.Count).ToList()[0].ToString();

            if (ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 1).Select(x => x.IsActive).ToList()[0].ToString()
                == "1")
            {
                cbCoin1.IsChecked = true;
            }
            else
            {
                cbCoin1.IsChecked = false;
            }

            if (ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 2).Select(x => x.IsActive).ToList()[0].ToString()
                == "1")
            {
                cbCoin2.IsChecked = true;
            }
            else
            {
                cbCoin2.IsChecked = false;
            }

            if (ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 3).Select(x => x.IsActive).ToList()[0].ToString()
                == "1")
            {
                cbCoin5.IsChecked = true;
            }
            else
            {
                cbCoin5.IsChecked = false;
            }

            if (ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 4).Select(x => x.IsActive).ToList()[0].ToString()
                == "1")
            {
                cbCoin10.IsChecked = true;
            }
            else
            {
                cbCoin10.IsChecked = false;
            }
        }

        private void cbCoin1_Checked(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.ButtonMoneyOffAndOn(1, 1);
        }

        private void cbCoin1_Unchecked(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.ButtonMoneyOffAndOn(1, 0);
        }

        private void cbCoin2_Checked(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.ButtonMoneyOffAndOn(2, 1);
        }

        private void cbCoin2_Unchecked(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.ButtonMoneyOffAndOn(2, 0);
        }

        private void cbCoin5_Checked(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.ButtonMoneyOffAndOn(5, 1);
        }

        private void cbCoin5_Unchecked(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.ButtonMoneyOffAndOn(5, 0);
        }

        private void cbCoin10_Checked(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.ButtonMoneyOffAndOn(10, 1);
        }

        private void cbCoin10_Unchecked(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.ButtonMoneyOffAndOn(10, 0);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.CountCoinsUpdate(1, int.Parse(tbCoin1.Text));
            ClassDataBase.dbObject.CountCoinsUpdate(2, int.Parse(tbCoin2.Text));
            ClassDataBase.dbObject.CountCoinsUpdate(5, int.Parse(tbCoin5.Text));
            ClassDataBase.dbObject.CountCoinsUpdate(10, int.Parse(tbCoin10.Text));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameObj.GoBack();
        }
    }
}
