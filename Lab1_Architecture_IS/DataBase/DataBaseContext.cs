using Lab1_Architecture_IS.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1_Architecture_IS.DataBase
{
    public class DataBaseContext: DbContext
    {
        public DbSet<CSVModel> Models { get; set; }

        public DataBaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ModelDataBase;Trusted_Connection=True;");
        }
    }
}
