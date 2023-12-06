using System;
using System.Linq;
using ELibra.DBModel;


namespace ELibra.Classes.Update
{
    class UpdateDB
    {
        static public void UpdateAll()
        {
            try
            {
                //UpdateWeighings();
                UpdateSettings();
                UpdateAutoWeighingFacts();
                UpdateCarMarks();
                UpdateCarriers();
                UpdateCars();
                UpdateClients();
                UpdateConsignee();
                UpdateConsignor();
                UpdateDrivers();
                UpdateGoods();
                UpdateStorages();
                UpdateUsers();
                UpdateWeighingFactLinks();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "\nВозникла ошибка при обновлении таблицы, либо обновление не требуется");
            }


        }

        static private string ConvertDate(String oldDate)
        {
            if (oldDate != null)
            {
                if (oldDate[4] != '-')
                {
                    string date = oldDate.Substring(0, 2);
                    string month = oldDate.Substring(3, 2);
                    string year = oldDate.Substring(6, 4);
                    string time = oldDate.Remove(0, 11);
                    string newDate = year + "-" + month + "-" + date + " " + time;
                    return newDate;
                }
                else
                {
                    return oldDate;
                }
            }
            else
            {
                return oldDate;
            }

        }

        private static void UpdateSettings()
        {
            IQueryable<DBModel.ModelsTimeString.Settings> settingsString;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                settingsString = (from i in context.SettingsString select i);
                foreach (var item in settingsString)
                {
                    item.licenseDate = ConvertDate(item.licenseDate);
                    
                    item.lastUpload = ConvertDate(item.lastUpload);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateAutoWeighingFacts()
        {
            IQueryable<DBModel.ModelsTimeString.AutoWeighingFacts> autoWeighingFactsString;
            using(DataBaseContextString context = new DataBaseContextString())
            {
                autoWeighingFactsString = (from i in context.AutoWeighingFactsString select i);
                foreach (var item in autoWeighingFactsString)
                {
                    item.startDate = ConvertDate(item.startDate);
                    item.finishDate = ConvertDate(item.finishDate);                    
                }
                context.SaveChanges();
            }
        }

        static private void UpdateCarMarks()
        {
            IQueryable<DBModel.ModelsTimeString.CarMarks> carMarks;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                carMarks = (from i in context.CarMarksString select i);
                foreach (var item in carMarks)
                {
                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateCarriers()
        {
            IQueryable<DBModel.ModelsTimeString.Carriers> carriers;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                carriers = (from i in context.CarriersString select i);
                foreach (var item in carriers)
                {
                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateCars()
        {
            IQueryable<DBModel.ModelsTimeString.Cars> cars;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                cars = (from i in context.CarsString select i);
                foreach (var item in cars)
                {
                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateClients()
        {
            IQueryable<DBModel.ModelsTimeString.Clients> clients;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                clients = (from i in context.ClientsString select i);
                foreach (var item in clients)
                {
                    item.startDate = ConvertDate(item.startDate);
                    item.endDate = ConvertDate(item.endDate);
                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateConsignee()
        {
            IQueryable<DBModel.ModelsTimeString.Consignee> consignees;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                consignees = (from i in context.ConsigneeString select i);
                foreach (var item in consignees)
                {

                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateConsignor()
        {
            IQueryable<DBModel.ModelsTimeString.Consignor> consignors;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                consignors = (from i in context.ConsignorString select i);
                foreach (var item in consignors)
                {
                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateDrivers()
        {
            IQueryable<DBModel.ModelsTimeString.Drivers> drivers;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                drivers = (from i in context.DriversString select i);
                foreach (var item in drivers)
                {
                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateGoods()
        {
            IQueryable<DBModel.ModelsTimeString.Goods> goods;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                goods = (from i in context.GoodsString select i);
                foreach (var item in goods)
                {
                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateStorages()
        {
            IQueryable<DBModel.ModelsTimeString.Storages> storages;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                storages = (from i in context.StoragesString select i);
                foreach (var item in storages)
                {
                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateUsers()
        {
            IQueryable<DBModel.ModelsTimeString.Users> users;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                users = (from i in context.UsersString select i);
                foreach (var item in users)
                {
                    item.updated = ConvertDate(item.updated);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateWeighingFactLinks()
        {
            IQueryable<DBModel.ModelsTimeString.WeighingFactLinks> weighingFactLinks;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                weighingFactLinks = (from i in context.WeighingFactLinksString select i);
                foreach (var item in weighingFactLinks)
                {
                    item.dateWeight = ConvertDate(item.dateWeight);
                }
                context.SaveChanges();
            }
        }

        static private void UpdateWeighings()
        {
            IQueryable<DBModel.ModelsTimeString.Weighings> weighings;
            using (DataBaseContextString context = new DataBaseContextString())
            {
                weighings = (from i in context.WeighingsString select i);
                var test = weighings.ToArray();
                var iter = 0;
                foreach (var item in weighings)
                {
                    item.dateWeight = item.dateWeight == "NULL" ? null : ConvertDate(item.dateWeight);
                    item.dateOut = item.dateOut == "NULL"? null : ConvertDate(item.dateOut);
                    item.updated = item.updated == "NULL" ? null : ConvertDate(item.updated);
                    context.SaveChanges();
                    Console.WriteLine(iter++);
                }
               
            }
        }
    }
}
