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
    
    public partial class BookTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookTable()
        {
            this.TicketTable = new HashSet<TicketTable>();
        }
    
        public int PersonalNumber_Book { get; set; }
        public string Name_Book { get; set; }
        public int Instance_Book { get; set; }
        public int pnPublishing_House { get; set; }
        public System.DateTime DatePublishing_Book { get; set; }
        public int QuantityPublishing_Book { get; set; }
        public int pnCategory_Book { get; set; }
        public int pnGo_Book { get; set; }
        public int pnCash_Book { get; set; }
        public int Like_Book { get; set; }
        public int Dislike_Book { get; set; }
        public int pnAuthor_Book { get; set; }
        public Nullable<int> pnImage_Book { get; set; }
    
        public virtual AuthorsTable AuthorsTable { get; set; }
        public virtual CashTable CashTable { get; set; }
        public virtual CategoryTable CategoryTable { get; set; }
        public virtual GoTable GoTable { get; set; }
        public virtual ImagebookTable ImagebookTable { get; set; }
        public virtual PublishingHouseTable PublishingHouseTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TicketTable> TicketTable { get; set; }
    }
}
