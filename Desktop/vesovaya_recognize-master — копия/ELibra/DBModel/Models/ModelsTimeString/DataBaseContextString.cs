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
    public class DataBaseContextString : DbContext
    {
        public DataBaseContextString() :
            base(new SQLiteConnection()
            {
                ConnectionString = new SQLiteConnectionStringBuilder()
                {
                    DataSource = Environment.CurrentDirectory + "\\database.db3",
                    ForeignKeys = true
                }.ConnectionString
            }, true)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<ModelsTimeString.AutoWeighingFacts> AutoWeighingFactsString { get; set; }


        public DbSet<ModelsTimeString.CarMarks> CarMarksString { get; set; }

        public DbSet<ModelsTimeString.Carriers> CarriersString { get; set; }

        public DbSet<ModelsTimeString.Cars> CarsString { get; set; }

        public DbSet<ModelsTimeString.Clients> ClientsString { get; set; }

        public DbSet<ModelsTimeString.Consignee> ConsigneeString { get; set; }

        public DbSet<ModelsTimeString.Consignor> ConsignorString { get; set; }

        public DbSet<ModelsTimeString.Drivers> DriversString { get; set; }

        public DbSet<ModelsTimeString.Goods> GoodsString { get; set; }


        public DbSet<ModelsTimeString.Settings> SettingsString { get; set; }

        public DbSet<ModelsTimeString.Storages> StoragesString { get; set; }

        public DbSet<ModelsTimeString.Users> UsersString { get; set; }

        public DbSet<ModelsTimeString.WeighingFactLinks> WeighingFactLinksString { get; set; }

        public DbSet<ModelsTimeString.Weighings> WeighingsString { get; set; }
    }
}
