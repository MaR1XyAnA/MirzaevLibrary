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
    
    public partial class AuthorsTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuthorsTable()
        {
            this.BookTable = new HashSet<BookTable>();
        }
    
        public int PersonalNumberAuthor { get; set; }
        public string SurnameAuthor { get; set; }
        public string NameAuthor { get; set; }
        public string MiddlenameAuthor { get; set; }
        public System.DateTime DateofbrichAuthor { get; set; }
        public Nullable<System.DateTime> DateofendAuthor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookTable> BookTable { get; set; }
    }
}
