using InventoryManagement.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
            DbContextOptions = dbContextOptions;
        }
        public DbSet<Item> Items { get; set; }

        public DbSet<ReportHistory> Reports { get; set; }

        public DbSet<Command> CommandTypes { get; set; }

        public DbContextOptions<AppDbContext> DbContextOptions { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Item>(ConfigureItem);
            builder.Entity<Command>(ConfigureCommandType);
            builder.Entity<ReportHistory>(ConfigureReportHistory);

        }
        private void ConfigureItem(EntityTypeBuilder<Item> builder)
        {
            builder.Property(bi => bi.CostPrice).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(bi => bi.SellPrice).IsRequired(true).HasColumnType("decimal(18,2)");
         
        }
        private void ConfigureCommandType(EntityTypeBuilder<Command> builder)
        {
            builder.ToTable("CommandType");
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id)
               .ForSqlServerUseSequenceHiLo("command_type_hilo")
               .IsRequired();
            builder.Property(cb => cb.Type)
                .IsRequired()
                .HasMaxLength(100);
        }
        private void ConfigureReportHistory(EntityTypeBuilder<ReportHistory> builder)
        {
            builder.Property(bi => bi.CurrentProfit)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder.Property(bi => bi.LastProfit)
             .IsRequired(true)
             .HasColumnType("decimal(18,2)");

            builder.Property(bi => bi.LastTotal)
           .IsRequired(true)
           .HasColumnType("decimal(18,2)");

        }

    }
}
