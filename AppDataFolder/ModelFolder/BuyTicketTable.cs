//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MirzaevLibrary.AppDataFolder.ModelFolder
{
    using System;
    using System.Collections.Generic;
    
    public partial class BuyTicketTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BuyTicketTable()
        {
            this.TicketTable = new HashSet<TicketTable>();
        }
    
        public int PersonalnumberBuy { get; set; }
        public string NameBuy { get; set; }
        public decimal PriseBuy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketTable> TicketTable { get; set; }
    }
}