using System;
using System.Collections.Generic;
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
using VendingMachine.Pages;

namespace VendingMachine
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<drinkReport> сountDrinkInMachine;

        public static List<drinkReport> СountDrinkInMachine { get => сountDrinkInMachine; set => сountDrinkInMachine = value; }
        public MainWindow()
        {
            InitializeComponent();

            ClassDataBase.dbObject = new VendingMachinesEntities();

            СountDrinkInMachine = new List<drinkReport>();
            foreach (drinkReport drinks in ClassDataBase.dbObject.drinkReport)
            {
                СountDrinkInMachine.Add(drinks);
            }

            ClassFrame.frameObj = frameMain;
            ClassFrame.frameObj.Navigate(new PageShopper());
        }
    }
}
