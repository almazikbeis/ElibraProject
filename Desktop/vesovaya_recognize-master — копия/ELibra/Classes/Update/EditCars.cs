using ELibra.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.Classes.Update
{
    class EditCars
    {
        public static void RemoveSpace()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var editTable = from i in context.Cars
                                select i;
                foreach (var item in editTable)
                {
                    if (item.number != null)
                    {
                        item.number = item.number.Replace(" ", "");
                    }
                }
                context.SaveChanges();

                var editWeighings = from i in context.Weighings
                                    select i;
                foreach (var item in editWeighings)
                {
                    if (item.carNumber != null)
                    {
                        item.carNumber = item.carNumber.Replace(" ", "");
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
