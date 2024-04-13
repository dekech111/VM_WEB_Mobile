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
    /// Логика взаимодействия для PageReport.xaml
    /// </summary>
    public partial class PageReport : Page
    {

        public PageReport()
        {
            InitializeComponent();
            List<drinksReportObj> drinksReportObj = new List<drinksReportObj>();
            int i = 0;
            foreach (drinkReport drinks in ClassDataBase.dbObject.drinkReport)
            {
                int countDrinkInMachine = MainWindow.СountDrinkInMachine[i].Count;
                drinksReportObj.Add(new drinksReportObj
                {
                    Name = drinks.Name,
                    Cost = drinks.Cost,
                    countInMachineAFT = drinks.Count,
                    countInMachineBF = countDrinkInMachine,
                    Profit = (countDrinkInMachine - drinks.Count) * drinks.Cost,
                    dateToDay = DateTime.Today
            });
                i++;
            }
            txbkDateToDay.Text = DateTime.Today.ToString("dd.MM.yyyy");
            dgOtchet.ItemsSource = drinksReportObj;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frameObj.GoBack();
        }
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            dgOtchet.SelectAllCells();
            dgOtchet.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;

            ApplicationCommands.Copy.Execute(null, dgOtchet);
            String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            String result = (string)Clipboard.GetData(DataFormats.Text);


            dgOtchet.UnselectAllCells();
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Отчет по продажам";
            dlg.DefaultExt = ".text";
            dlg.Filter = "(.xls)|*.xls";

            Nullable<bool> result1 = dlg.ShowDialog();
            if (result1 == true)
            {

                string filename = dlg.FileName;

                System.IO.StreamWriter file = new System.IO.StreamWriter(filename, false, Encoding.Default);
                file.WriteLine(result);
                file.Close();

                MessageBox.Show("Экспорт данных успешно завершен");
            }
        }
    }
}
