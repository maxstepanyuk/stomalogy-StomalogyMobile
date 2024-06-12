﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomalogyMobile
{
    class BaseContext : DbContext
    {
        public DbSet<Patient> Pats { get; set; } = null!;
        public DbSet<Appointment> Apps { get; set; } = null!;
        public DbSet<Dentist> Dents { get; set; } = null!;
        public DbSet<Reason> Reasons { get; set; } = null!;
        public DbSet<Cabinet> Cabs { get; set; } = null!;

        public BaseContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
                       //optionsBuilder.UseSqlite("Data Source = D:\\Base.db");
                       optionsBuilder.UseMySql("server=auris.cityhost.com.ua;" +
                    "user=ch63daa396_ForStomatology; password=jf473hjsf389;database=ch63daa396_ForStomatology;",
                    new MySqlServerVersion(new Version(8, 0, 25)));
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }


    }
}
