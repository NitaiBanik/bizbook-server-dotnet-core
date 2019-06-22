﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.Model;
using Model.Model.Customers;
using Model.Model.Products;
using Model.Model.Purchases;
using Model.Model.Sales;
using Model.Model.Transactions;
using Model.Shops;

namespace Model
{
    public class BizBookInventoryContext : DbContext
    {
        #region Products
        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        public virtual DbSet<ProductGroup> ProductGroups { get; set; }

        public virtual DbSet<ProductDetail> ProductDetails { get; set; }

        #endregion

        #region Sales
        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Dealer> Dealers { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleDetail> SaleDetails { get; set; }
        public virtual DbSet<SaleState> SaleStates { get; set; }
        #endregion

        #region Purchases
        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<Purchase> Purchases { get; set; }

        public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        #endregion

        #region Transactions

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<AccountHead> AccountHeads { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

        #endregion


        public virtual DbSet<District> Districts { get; set; }

        public virtual DbSet<Shop> Shops { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public BizBookInventoryContext()
        {

        }

        public BizBookInventoryContext(DbContextOptions<BizBookInventoryContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=.;Database=BizBookCoreDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //BizBookModelBuilder.BuildModels(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
