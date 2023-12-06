using ELibra.DBModel.Models;
using ELibra.DBModel.ModelsTimeString;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ELibra.DBModel
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder()
                {
                    DataSource = Environment.CurrentDirectory + "\\database.db3",
                    ForeignKeys = true,
                    //Password = ""
                }.ConnectionString
            }, true)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Models.AutoWeighingFacts> AutoWeighingFacts { get; set; }

        public DbSet<Models.AutoWeighingMovies> AutoWeighingMovies { get; set; }

        public DbSet<Models.CarMarks> CarMarks { get; set; }

        public DbSet<Models.Carriers> Carriers { get; set; }

        public DbSet<Models.Cars> Cars { get; set; }

        public DbSet<Models.Clients> Clients { get; set; }

        public DbSet<Models.newClients> newClients { get; set; }

        public DbSet<Models.Consignee> Consignee { get; set; }

        public DbSet<Models.Consignor> Consignor { get; set; }

        public DbSet<Models.Drivers> Drivers { get; set; }

        public DbSet<Models.Goods> Goods { get; set; }

        public DbSet<Models.Rols> Rols { get; set; }

        public DbSet<Models.Settings> Settings { get; set; }

        public DbSet<Models.Storages> Storages { get; set; }

        public DbSet<Models.Users> Users { get; set; }

        public DbSet<Models.WeighingFactLinks> WeighingFactLinks { get; set; }

        public DbSet<Models.Weighings> Weighings { get; set; }

        public DbSet<Models.Scales> Scales { get; set; }

    }
}
