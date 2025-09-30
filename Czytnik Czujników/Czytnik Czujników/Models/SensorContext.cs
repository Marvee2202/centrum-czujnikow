using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Czytnik_Czujników;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace Centrum_zarządzania.Models
{
    public class SensorContext : DbContext
    {
        public DbSet<Reading> readings { get; set; }
        public DbSet<Sensor> sensors { get; set; }
        public DbSet<Device> devices { get; set; } 

        private string _connectionString;

        public SensorContext(DbConnectionData connectionData)
        {
            _connectionString = connectionData.GetConnectionString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reading>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Value).IsRequired();
                entity.HasOne(e => e.Sensor).WithMany(s => s.Readings);
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.HasOne(e => e.Device).WithMany(d => d.Sensors);
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(e => e.Name);
            });
        }
    }
}
