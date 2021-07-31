using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

//DB CONNECTION
namespace HotelFinder.DataAccess
{
    public class HotelDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //conn string
            optionsBuilder.UseSqlServer("Server=DESKTOP-GT7EIMQ; Database=HotelDb;uid=nur;pwd=12345;");
        }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
