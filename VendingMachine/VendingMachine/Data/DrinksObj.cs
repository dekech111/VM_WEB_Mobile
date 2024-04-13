using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Data
{
    internal class DrinksObj
    {
        public static int Id { get; set; }
        public static string Name { get; set; }
        public static byte[] Image { get; set; }
        public static decimal Cost { get; set; }
    }
}
