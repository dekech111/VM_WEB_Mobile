//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VendingMachine.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class drinkReport
    {
        public int VendingMachineId { get; set; }
        public int DrinksId { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public decimal Cost { get; set; }
    }
}
