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
    
    public partial class VendingMachineCoins
    {
        public int Id { get; set; }
        public int VendingMachineId { get; set; }
        public int CoinsId { get; set; }
        public int Count { get; set; }
        public int IsActive { get; set; }
    
        public virtual Coins Coins { get; set; }
        public virtual VendingMachines VendingMachines { get; set; }
    }
}
