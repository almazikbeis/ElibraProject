using System;
using System.Data.SQLite;

namespace ELibra.Classes.Update
{
    class AddSetingsColumn
    {
        
        public static void AddDelayColumn()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "database" + ".db3;Version=3;MultipleActiveResultSets=true;"))
            {
                connection.Open();
                string stringColumnExist = "SELECT name from pragma_table_info('Settings') where name = 'RecognizeDelay'";

                SQLiteCommand cmdColumnExist = new SQLiteCommand(stringColumnExist, connection);
                var isExist = cmdColumnExist.ExecuteScalar();
                if (isExist == null)
                {
                    string stringCreate = "ALTER TABLE Settings ADD RecognizeDelay REAL";
                    SQLiteCommand cmdCrete = new SQLiteCommand(stringCreate, connection);
                    cmdCrete.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

    }
}
