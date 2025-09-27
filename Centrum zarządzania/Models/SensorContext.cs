using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centrum_zarządzania.ViewModels;
using Microsoft.EntityFrameworkCore;
//using MySql.EntityFrameworkCore.Extensions;

namespace Centrum_zarządzania.Models
{
    public class SensorContext : DbContext
    {
        public DbSet<Reading> readings { get; set; }
        //public DbSet<SensorViewModel> sensors { get; set; }
        public string DbPath { get; }

        public SensorContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "readings.db");
            Debug.WriteLine(DbPath);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(new DbConnectionData().GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reading>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Value).IsRequired();
            });
        }
    }
}
