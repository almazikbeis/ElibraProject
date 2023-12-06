using ELibra.DBModel;
using ELibra.DBModel.Models;
using System;
using System.Data.SQLite;
using System.Linq;


namespace ELibra.Classes.Update
{
    class CreateClientTable
    {

        public static void Create()
        {
            bool execute = false;
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "database" + ".db3;Version=3;MultipleActiveResultSets=true;"))
            {
                connection.Open();
                string stringColumnExist = "SELECT type from pragma_table_info('Clients') where name = 'factor'";
                SQLiteCommand cmdColumnExist = new SQLiteCommand(stringColumnExist, connection);
                var isExist = cmdColumnExist.ExecuteScalar();
                if (isExist.Equals("INTEGER"))
                {
                    string stringCreate = "CREATE TABLE \"newClients\"(" +
                                                    "\"id\"    INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                    "\"name\"  TEXT NOT NULL," +
                                                    "\"RNN\"   TEXT," +
                                                    "\"address\"   TEXT," +
                                                    "\"phone\" TEXT," +
                                                    "\"description\"   TEXT," +
                                                    "\"factor\"    REAL DEFAULT 1," +
                                                    "\"startDate\" TEXT," +
                                                    "\"endDate\"   TEXT," +
                                                    "\"updated\"   TEXT)";

                    SQLiteCommand cmdCrete = new SQLiteCommand(stringCreate, connection);
                    cmdCrete.ExecuteNonQuery();
                    execute = true;
                }               
                connection.Close();
            }

            if (execute)
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    var oldTable = from i in context.Clients
                                   select i;
                    var test = oldTable.ToArray();
                    int count = 1;
                    foreach (var item in oldTable)
                    {
                        var newRow = new newClients();
                        while (item.id > count)
                        {
                            newRow.name = "temp";
                            context.newClients.Add(newRow);
                            context.SaveChanges();
                            context.newClients.Remove(newRow);
                            context.SaveChanges();
                            count++;
                        }
                        newRow.id = item.id;
                        newRow.name = item.name;
                        newRow.RNN = item.RNN;
                        newRow.address = item.address;
                        newRow.phone = item.phone;
                        newRow.description = item.description;
                        newRow.factor = Convert.ToInt32(item.factor);
                        // TODO
                        newRow.startDate =  item.startDate;
                        newRow.endDate = item.endDate;
                        newRow.updated = item.updated;
                        context.newClients.Add(newRow);
                        context.SaveChanges();
                        count++;
                    }
                }

                using (SQLiteConnection connection = new SQLiteConnection("Data Source=database.db3;"))
                {
                    connection.Open();
                    string stringDelete = "DROP TABLE Clients;";
                    SQLiteCommand cmdDelete = new SQLiteCommand(stringDelete, connection);
                    cmdDelete.ExecuteNonQuery();
                    string stringRename = "ALTER TABLE newClients RENAME TO Clients;";
                    SQLiteCommand cmdRename = new SQLiteCommand(stringRename, connection);
                    cmdRename.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
