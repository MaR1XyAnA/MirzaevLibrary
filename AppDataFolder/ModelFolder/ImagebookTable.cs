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
    
    public partial class ImagebookTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImagebookTable()
        {
            this.BookTable = new HashSet<BookTable>();
        }
    
        public int PersonalnumberImage { get; set; }
        public byte[] ImageImage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookTable> BookTable { get; set; }
    }
}
