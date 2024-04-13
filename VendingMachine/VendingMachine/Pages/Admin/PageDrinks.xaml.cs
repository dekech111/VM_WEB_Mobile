using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    /// Логика взаимодействия для PageDrinks.xaml
    /// </summary>
    public partial class PageDrinks : Page
    {
        private static Drinks drinks;
        private static string imagePath;
        public PageDrinks()
        {
            InitializeComponent();
            DataContext = drinks;
            imagePath = null;
            lvDrinks.ItemsSource = ClassDataBase.dbObject.Drinks.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Drinks> drnk = ClassDataBase.dbObject.Drinks.Where(x => x.Id == DrinksObj.Id).AsEnumerable().
                Select(x =>
                {
                    x.Cost = decimal.Parse(txbPriceEdit.Text);
                    return x;
                });
            foreach (Drinks dr in drnk)
            {
                ClassDataBase.dbObject.Entry(dr).State = System.Data.Entity.EntityState.Modified;
            }

            IEnumerable<VendingMachineDrinks> vendingMachineDrinks = ClassDataBase.dbObject.VendingMachineDrinks.Where(x => x.DrinksId == DrinksObj.Id).AsEnumerable().
               Select(x =>
               {
                   x.Count = int.Parse(txbCountEdit.Text);
                   return x;
               });
            foreach (VendingMachineDrinks vmd in vendingMachineDrinks)
            {
                ClassDataBase.dbObject.Entry(vmd).State = System.Data.Entity.EntityState.Modified;
            }
            try
            {
                ClassDataBase.dbObject.SaveChanges();
                MessageBox.Show("Данные успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            Drinks drink = lvDrinks.SelectedItem as Drinks;
            try
            {


                if (drink == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                else if (MessageBox.Show("Вы точно хотите удалить запись?\n" + drink.Name,
                    "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ClassDataBase.dbObject.Drinks.Remove(drink);
                    ClassDataBase.dbObject.SaveChanges();
                    MessageBox.Show("Запись удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    lvDrinks.ItemsSource = ClassDataBase.dbObject.Drinks.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Критическая ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btnOtmend_Click(object sender, RoutedEventArgs e)
        {
            spAdd.Visibility = Visibility.Hidden;
            spEdit.Visibility = Visibility.Visible;
        }



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            spAdd.Visibility = Visibility.Visible;
            spEdit.Visibility = Visibility.Hidden;
        }

        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            var entObj = ClassDataBase.dbObject.Drinks.FirstOrDefault(x => x.Name.ToLower() == txbName.Text.ToLower());

            if (txbName.Text.Length == 0 && txbPriceAdd.Text.Length == 0 && txbCountAdd.Text.Length == 0)
                MessageBox.Show("Поля не могут быть пустыми!",
                    "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (txbName.Text.Length == 0)
            {
                MessageBox.Show("Поле: 'Название' не может быть пустым!",
                    "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txbName.Focus();
            }

            else if (txbPriceAdd.Text.Length == 0)
            {
                MessageBox.Show("Поле: 'Цена' не может быть пустым!",
                    "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txbPriceAdd.Focus();
            }
            else if (txbCountAdd.Text.Length == 0)
            {
                MessageBox.Show("Поле: 'Количество' не может быть пустым!",
                   "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                txbCountAdd.Focus();
            }
            else if (entObj != null)
                MessageBox.Show("Напиток с таким именем уже есть!",
                    "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
               if (imagePath == null)
                {
                    Drinks drink = new Drinks()
                    {
                        Name = txbName.Text,
                        Image = null,
                        Cost = Convert.ToDecimal(txbPriceAdd.Text)
                    };
                    ClassDataBase.dbObject.Drinks.Add(drink);
                    ClassDataBase.dbObject.SaveChanges();

                    VendingMachineDrinks countDrinks = new VendingMachineDrinks()
                    {
                        VendingMachineId = 1,
                        DrinksId = drink.Id,
                        Count = int.Parse(txbCountAdd.Text)
                    };
                    ClassDataBase.dbObject.VendingMachineDrinks.Add(countDrinks);
                    ClassDataBase.dbObject.SaveChanges();

                    MessageBox.Show("Напиток успешно добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    lvDrinks.ItemsSource = ClassDataBase.dbObject.Drinks.ToList();
                }
               else
                {
                    Drinks drink = new Drinks()
                    {
                        Name = txbName.Text,
                        Image = File.ReadAllBytes(imagePath),
                        Cost = Convert.ToDecimal(txbPriceAdd.Text)
                    };
                    ClassDataBase.dbObject.Drinks.Add(drink);
                    ClassDataBase.dbObject.SaveChanges();

                    VendingMachineDrinks countDrinks = new VendingMachineDrinks()
                    {
                        VendingMachineId = 1,
                        DrinksId = drink.Id,
                        Count = int.Parse(txbCountAdd.Text)
                    };
                    ClassDataBase.dbObject.VendingMachineDrinks.Add(countDrinks);
                    ClassDataBase.dbObject.SaveChanges();

                    MessageBox.Show("Напиток успешно добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    lvDrinks.ItemsSource = ClassDataBase.dbObject.Drinks.ToList();
                }               
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameObj.GoBack();
        }
        private void btnOpenPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {

                imgPhoto.Source = new BitmapImage(new Uri(dlg.FileName));

                lNamePhoto.Content = dlg.FileName;
                imagePath = lNamePhoto.Content.ToString();

                lNamePhoto.Content = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            spAdd.Visibility = Visibility.Hidden;
            spEdit.Visibility = Visibility.Visible;


            Drinks drinks = lvDrinks.SelectedItem as Drinks;
            var count = ClassDataBase.dbObject.VendingMachineDrinks.FirstOrDefault(x => x.DrinksId == drinks.Id);
            if (drinks == null)
                MessageBox.Show("Запись не выбрана!\nВыберите запись!", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                lNameProd.Content = drinks.Name;
                txbPriceEdit.Text = drinks.Cost.ToString();
                txbCountEdit.Text = count.Count.ToString();
                DrinksObj.Id = drinks.Id;
            }
        }


        private void lvDrinks_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            spAdd.Visibility = Visibility.Hidden;
            spEdit.Visibility = Visibility.Visible;

            Drinks drinks = lvDrinks.SelectedItem as Drinks;
            var count = ClassDataBase.dbObject.VendingMachineDrinks.FirstOrDefault(x => x.DrinksId == drinks.Id);
            if (drinks == null)
                MessageBox.Show("Запись не выбрана!\nВыберите запись!", "Ошибка редактирования", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                lNameProd.Content = drinks.Name;
                txbPriceEdit.Text = drinks.Cost.ToString();
                txbCountEdit.Text = count.Count.ToString();
                DrinksObj.Id = drinks.Id;
            }
        }

    }
}
