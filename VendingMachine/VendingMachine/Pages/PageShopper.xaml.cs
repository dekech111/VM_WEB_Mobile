using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для PageShopper.xaml
    /// </summary>
    public partial class PageShopper : Page
    {
        public int clickCount = 0;
        public bool machineActive = true;
        List<Drinks> drinks = ClassDataBase.dbObject.Drinks.Where(x => x.Id == 1).ToList();
        public PageShopper()
        {
            InitializeComponent(); 

            Update();
        }

        public void Update()
        {
            lvDrinks.ItemsSource = ClassDataBase.dbObject.Drinks.ToList();

            if (ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 1).Select(x => x.IsActive).ToList()[0].ToString() == "1")
            {
                btnCoin1.IsEnabled = true;
            }
            else
            {
                btnCoin1.IsEnabled = false;
            }

            if (ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 2).Select(x => x.IsActive).ToList()[0].ToString() == "1")
            {
                btnCoin2.IsEnabled = true;
            }
            else
            {
                btnCoin2.IsEnabled = false;
            }

            if (ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 3).Select(x => x.IsActive).ToList()[0].ToString() == "1")
            {
                btnCoin5.IsEnabled = true;
            }
            else
            {
                btnCoin5.IsEnabled = false;
            }

            if (ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 4).Select(x => x.IsActive).ToList()[0].ToString() == "1")
            {
                btnCoin10.IsEnabled = true;
            }
            else
            {
                btnCoin10.IsEnabled = false;
            }
            if (int.Parse(ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 1).Select(x => x.Count).ToList()[0].ToString()) < 10 ||
                int.Parse(ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 2).Select(x => x.Count).ToList()[0].ToString()) < 5 ||
                int.Parse(ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 3).Select(x => x.Count).ToList()[0].ToString()) < 2 ||
                int.Parse(ClassDataBase.dbObject.VendingMachineCoins.Where(x => x.CoinsId == 1).Select(x => x.Count).ToList()[0].ToString()) < 1)
            {
                tblShopper.Foreground = new SolidColorBrush(Color.FromRgb(0xfa, 0x4b, 0x4b));
                tblShopper.Text = "Автомат сдачи не выдаёт";
                machineActive = false;
            }
            else
            {
                if (btnCoin1.IsEnabled == false && btnCoin2.IsEnabled == false && btnCoin5.IsEnabled == false && btnCoin10.IsEnabled == false)
                {
                    lvDrinks.IsEnabled = false;
                    tblShopper.Foreground = new SolidColorBrush(Color.FromRgb(0xfa, 0x4b, 0x4b));
                    tblShopper.Text = "! Автомат не работает !";
                }
                else
                {
                    lvDrinks.IsEnabled = true;
                    tblShopper.Foreground = new SolidColorBrush(Color.FromRgb(0x97, 0xe5, 0x9a));
                    tblShopper.Text = "Автомат готов к работе";
                    btnChange.IsEnabled = true;
                    machineActive = true;
                }
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = new SolidColorBrush(Color.FromRgb(0x03, 0x03, 0x03));
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = new SolidColorBrush(Color.FromRgb(0xd6, 0xd6, 0xd6));
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(tblSumma.Text) == 0)
            {
                clickCount += 1;
            }
            else
            {
                clickCount = 0;
                if (machineActive == false)
                {
                    MessageBox.Show("Автомат сдачи не выдаёт", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tblSumma.Text = "0";
                    return;
                }
            }
            if (int.Parse(tblSumma.Text) == 0 && clickCount == 10)
            {
                CodeWindow window = new CodeWindow();
                clickCount = 0;
                window.ShowDialog();
            }
            else
            {
                if (int.Parse(tblSumma.Text) > 0)
                {
                    while (int.Parse(tblSumma.Text) / 10 >= 1)
                    {
                        tblSumma.Text = (int.Parse(tblSumma.Text) - 10).ToString();
                        ClassDataBase.dbObject.CoinsDrop(10);
                    }
                    while (int.Parse(tblSumma.Text) / 5 >= 1)
                    {
                        tblSumma.Text = (int.Parse(tblSumma.Text) - 5).ToString();
                        ClassDataBase.dbObject.CoinsDrop(5);
                    }
                    while (int.Parse(tblSumma.Text) / 2 >= 1)
                    {
                        tblSumma.Text = (int.Parse(tblSumma.Text) - 2).ToString();
                        ClassDataBase.dbObject.CoinsDrop(2);
                    }
                    while (int.Parse(tblSumma.Text) > 0)
                    {
                        tblSumma.Text = (int.Parse(tblSumma.Text) - 1).ToString();
                        ClassDataBase.dbObject.CoinsDrop(1);
                    }
                    Update();
                }
            }
        }

        private void btnCoin1_Click(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.CoinsAdd(1);
            tblSumma.Text = (Convert.ToInt32(tblSumma.Text) + Convert.ToInt32(((Button)sender).Content)).ToString();
        }

        private void btnCoin5_Click(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.CoinsAdd(5);
            tblSumma.Text = (Convert.ToInt32(tblSumma.Text) + Convert.ToInt32(((Button)sender).Content)).ToString();
        }

        private void btnCoin2_Click(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.CoinsAdd(2);
            tblSumma.Text = (Convert.ToInt32(tblSumma.Text) + Convert.ToInt32(((Button)sender).Content)).ToString();
        }

        private void btnCoin10_Click(object sender, RoutedEventArgs e)
        {
            ClassDataBase.dbObject.CoinsAdd(10);
            tblSumma.Text = (Convert.ToInt32(tblSumma.Text) + Convert.ToInt32(((Button)sender).Content)).ToString();
        }

        private void lvDrinks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drinks drinks = lvDrinks.SelectedItem as Drinks;

            if (drinks.Cost > int.Parse(tblSumma.Text))
            {
                tblShopper.Foreground = new SolidColorBrush(Color.FromRgb(0xfa, 0x4b, 0x4b));
                tblShopper.Text = "Недостаточно средств";
            }
            else
            {
                ClassDataBase.dbObject.DrinksDrop(drinks.Id);
                tblSumma.Text = (int.Parse(tblSumma.Text) - Convert.ToInt32(drinks.Cost)).ToString();
                tblShopper.Foreground = new SolidColorBrush(Color.FromRgb(0x97, 0xe5, 0x9a));
                // tblShopper.Text = "Спасибо за покупку!";
                Update();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           Update();
        }
    }
}
