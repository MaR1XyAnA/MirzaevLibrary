﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LibraryMirzayevaEntities : DbContext
    {
        public LibraryMirzayevaEntities()
            : base("name=LibraryMirzayevaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AuthorsTable> AuthorsTable { get; set; }
        public virtual DbSet<BookTable> BookTable { get; set; }
        public virtual DbSet<CashTable> CashTable { get; set; }
        public virtual DbSet<CategoryTable> CategoryTable { get; set; }
        public virtual DbSet<GoTable> GoTable { get; set; }
        public virtual DbSet<ImagebookTable> ImagebookTable { get; set; }
        public virtual DbSet<PublishingHouseTable> PublishingHouseTable { get; set; }
        public virtual DbSet<RoleTable> RoleTable { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TicketTable> TicketTable { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }
    }
}
