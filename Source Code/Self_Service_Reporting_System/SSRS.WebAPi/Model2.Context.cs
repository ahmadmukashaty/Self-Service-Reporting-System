﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSRS.WebAPi
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_A4FAA3_SelfServiceRSEntities : DbContext
    {
        public DB_A4FAA3_SelfServiceRSEntities()
            : base("name=DB_A4FAA3_SelfServiceRSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BuyingGroup> BuyingGroups { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerCategory> CustomerCategories { get; set; }
        public virtual DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        public virtual DbSet<SpecialDeal> SpecialDeals { get; set; }
        public virtual DbSet<SSRS_ATTRIBUTE> SSRS_ATTRIBUTE { get; set; }
        public virtual DbSet<SSRS_CATEGORY> SSRS_CATEGORY { get; set; }
        public virtual DbSet<SSRS_CLASSIFICATION> SSRS_CLASSIFICATION { get; set; }
        public virtual DbSet<SSRS_CLASSIFICATION_CATEGORY> SSRS_CLASSIFICATION_CATEGORY { get; set; }
        public virtual DbSet<SSRS_CLASSIFICATION_TABLE> SSRS_CLASSIFICATION_TABLE { get; set; }
        public virtual DbSet<SSRS_CLASSIFICATION_TABLE_TYPE> SSRS_CLASSIFICATION_TABLE_TYPE { get; set; }
        public virtual DbSet<SSRS_IS_MAIN> SSRS_IS_MAIN { get; set; }
        public virtual DbSet<SSRS_MODULE> SSRS_MODULE { get; set; }
        public virtual DbSet<SSRS_RELATION_TYPE> SSRS_RELATION_TYPE { get; set; }
        public virtual DbSet<SSRS_TABLE> SSRS_TABLE { get; set; }
        public virtual DbSet<SSRS_TYPE> SSRS_TYPE { get; set; }
        public virtual DbSet<StateProvince> StateProvinces { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierCategory> SupplierCategories { get; set; }
        public virtual DbSet<SupplierTransaction> SupplierTransactions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
    }
}
