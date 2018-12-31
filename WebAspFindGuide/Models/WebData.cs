namespace WebAspFindGuide.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WebData : DbContext
    {
        public WebData()
            : base("name=WebData")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<BankCard> BankCards { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Billing_App> Billing_App { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<OrderTour> OrderTours { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.AccountID)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Account_Pass)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Account_Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Account_Facebook)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Account_Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Account_Avarta)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Account_CodeConfig)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.BankCards)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.BankCard_AccountID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.OrderTours)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.OrderTour_Guide_ID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.OrderTours1)
                .WithOptional(e => e.Account1)
                .HasForeignKey(e => e.OrderTour_Tourists_ID);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Languages)
                .WithMany(e => e.Accounts)
                .Map(m => m.ToTable("Account_Lauguages").MapLeftKey("AccountID").MapRightKey("LanguageID"));

            modelBuilder.Entity<Area>()
                .HasMany(e => e.Accounts)
                .WithOptional(e => e.Area)
                .HasForeignKey(e => e.Account_Area);

            modelBuilder.Entity<BankCard>()
                .Property(e => e.BankCard_AccountID)
                .IsUnicode(false);

            modelBuilder.Entity<BankCard>()
                .Property(e => e.BankCard_Number)
                .IsUnicode(false);

            modelBuilder.Entity<BankCard>()
                .Property(e => e.BankCard_cnv)
                .IsUnicode(false);

            modelBuilder.Entity<BankCard>()
                .Property(e => e.BankCard_Type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BankCard>()
                .Property(e => e.BankCard_Time_lock)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Bank_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Bank_Swift)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Bank_Intermediate)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .Property(e => e.Bank_Swift_imd)
                .IsUnicode(false);

            modelBuilder.Entity<Bank>()
                .HasMany(e => e.BankCards)
                .WithOptional(e => e.Bank)
                .HasForeignKey(e => e.BankCard_BankID);

            modelBuilder.Entity<Billing_App>()
                .Property(e => e.BllingApp_KeySale)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .Property(e => e.LanguageKey)
                .IsUnicode(false);

            modelBuilder.Entity<OrderTour>()
                .Property(e => e.OrderTour_Guide_ID)
                .IsUnicode(false);

            modelBuilder.Entity<OrderTour>()
                .Property(e => e.OrderTour_Tourists_ID)
                .IsUnicode(false);

            modelBuilder.Entity<OrderTour>()
                .Property(e => e.OrderTour_Status)
                .IsUnicode(false);

            modelBuilder.Entity<OrderTour>()
                .HasMany(e => e.Billing_App)
                .WithOptional(e => e.OrderTour)
                .HasForeignKey(e => e.BillingApp_TourID);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Accounts)
                .WithOptional(e => e.Role)
                .HasForeignKey(e => e.Account_RoleID);

            modelBuilder.Entity<Sale>()
                .Property(e => e.Sale_Key)
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .HasMany(e => e.Billing_App)
                .WithOptional(e => e.Sale)
                .HasForeignKey(e => e.BllingApp_KeySale);
        }
    }
}
