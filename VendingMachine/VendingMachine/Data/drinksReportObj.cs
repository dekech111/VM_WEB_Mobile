using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Data
{
    internal class drinksReportObj
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int countInMachineBF { get; set; }
        public int countInMachineAFT { get; set; }
        public decimal Profit { get; set; }
        public DateTime dateToDay { get; set; }
    }
}
