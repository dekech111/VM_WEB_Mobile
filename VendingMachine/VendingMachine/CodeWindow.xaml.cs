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
using VendingMachine.Data;
using VendingMachine.Pages;

namespace VendingMachine
{
    /// <summary>
    /// Логика взаимодействия для CodeWindow.xaml
    /// </summary>
    public partial class CodeWindow : Window
    {
        public CodeWindow()
        {
            InitializeComponent();
        }

        private void btnCode_Click(object sender, RoutedEventArgs e)
        {
            ushort secretKey = 0x0040;

            string code = tbCode.Text;

            code = EncodeDecrypt(code, secretKey);

            var admin = ClassDataBase.dbObject.VendingMachines.FirstOrDefault(
                    x => x.SecretCode == code);

            if (admin == null)
            {
                MessageBox.Show("Код введён неверно",
                            "Ошибка",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
            }
            else
            {
                ClassFrame.frameObj.Navigate(new PageAdmin());
                Close();
            }
        }

        public static string EncodeDecrypt(string str, ushort secretKey)
        {
            var ch = str.ToArray();
            string newStr = "";
            foreach (var c in ch)
                newStr += TopSecret(c, secretKey);
            return newStr;
        }

        public static char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey);
            return character;
        }
    }
}
