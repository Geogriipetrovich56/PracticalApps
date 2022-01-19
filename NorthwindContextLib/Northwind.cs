using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Packt.Shared
{
    public partial class Northwind : DbContext
    {
        public Northwind()
        {
        }

        public Northwind(DbContextOptions<Northwind> options)
            : base(options)
        {
        }

        public virtual DbSet<AlphabeticalListOfProduct> AlphabeticalListOfProducts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategorySalesFor1997> CategorySalesFor1997s { get; set; } = null!;
        public virtual DbSet<CurrentProductList> CurrentProductLists { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get; set; } = null!;
        public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<OrderDetailsExtended> OrderDetailsExtendeds { get; set; } = null!;
        public virtual DbSet<OrderSubtotal> OrderSubtotals { get; set; } = null!;
        public virtual DbSet<OrdersQry> OrdersQries { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductSalesFor1997> ProductSalesFor1997s { get; set; } = null!;
        public virtual DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrices { get; set; } = null!;
        public virtual DbSet<ProductsByCategory> ProductsByCategories { get; set; } = null!;
        public virtual DbSet<QuarterlyOrder> QuarterlyOrders { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<SalesByCategory> SalesByCategories { get; set; } = null!;
        public virtual DbSet<SalesTotalsByAmount> SalesTotalsByAmounts { get; set; } = null!;
        public virtual DbSet<Shipper> Shippers { get; set; } = null!;
        public virtual DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters { get; set; } = null!;
        public virtual DbSet<SummaryOfSalesByYear> SummaryOfSalesByYears { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<Territory> Territories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
*/

                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Northwind;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlphabeticalListOfProduct>(entity =>
            {
                entity.ToView("Alphabetical list of products");
            });

            modelBuilder.Entity<CategorySalesFor1997>(entity =>
            {
                entity.ToView("Category Sales for 1997");
            });

            modelBuilder.Entity<CurrentProductList>(entity =>
            {
                entity.ToView("Current Product List");

                entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            });
                        
            modelBuilder.Entity<CustomerAndSuppliersByCity>(entity =>
            {
                entity.ToView("Customer and Suppliers by City");
            });

            modelBuilder.Entity<CustomerDemographic>(entity =>
            {
                entity.HasKey(e => e.CustomerTypeId)
                    .IsClustered(false);

                entity.Property(e => e.CustomerTypeId).IsFixedLength();
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToView("Invoices");

                entity.Property(e => e.CustomerId).IsFixedLength();
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderID, e.ProductID })
                    .HasName("PK_Order_Details");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Products");
            });

            modelBuilder.Entity<OrderDetailsExtended>(entity =>
            {
                entity.ToView("Order Details Extended");
            });

            modelBuilder.Entity<OrderSubtotal>(entity =>
            {
                entity.ToView("Order Subtotals");
            });

            modelBuilder.Entity<OrdersQry>(entity =>
            {
                entity.ToView("Orders Qry");

                entity.Property(e => e.CustomerId).IsFixedLength();
            });

            modelBuilder.Entity<ProductSalesFor1997>(entity =>
            {
                entity.ToView("Product Sales for 1997");
            });

            modelBuilder.Entity<ProductsAboveAveragePrice>(entity =>
            {
                entity.ToView("Products Above Average Price");
            });

            modelBuilder.Entity<ProductsByCategory>(entity =>
            {
                entity.ToView("Products by Category");
            });

            modelBuilder.Entity<QuarterlyOrder>(entity =>
            {
                entity.ToView("Quarterly Orders");

                entity.Property(e => e.CustomerId).IsFixedLength();
            });

            modelBuilder.Entity<SalesByCategory>(entity =>
            {
                entity.ToView("Sales by Category");
            });

            modelBuilder.Entity<SalesTotalsByAmount>(entity =>
            {
                entity.ToView("Sales Totals by Amount");
            });

            modelBuilder.Entity<SummaryOfSalesByQuarter>(entity =>
            {
                entity.ToView("Summary of Sales by Quarter");
            });

            modelBuilder.Entity<SummaryOfSalesByYear>(entity =>
            {
                entity.ToView("Summary of Sales by Year");
            });

            modelBuilder.Entity<Territory>(entity =>
            {
                entity.HasKey(e => e.TerritoryID)
                    .IsClustered(false);

                entity.Property(e => e.TerritoryDescription).IsFixedLength();

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Territories)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Territories_Region");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
