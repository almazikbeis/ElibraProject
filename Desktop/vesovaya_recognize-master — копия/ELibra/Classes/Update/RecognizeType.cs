using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.Classes.Update
{
    class RecognizeType
    {
        public static void AddTypeColumn()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "database" + ".db3;Version=3;MultipleActiveResultSets=true;"))
            {
                connection.Open();
                string stringColumnExist = "SELECT name from pragma_table_info('Settings') where name = 'RecognizeType'";

                SQLiteCommand cmdColumnExist = new SQLiteCommand(stringColumnExist, connection);
                var isExist = cmdColumnExist.ExecuteScalar();
                if (isExist == null)
                {
                    string stringCreate = "ALTER TABLE Settings ADD RecognizeType STRING";
                    SQLiteCommand cmdCrete = new SQLiteCommand(stringCreate, connection);
                    cmdCrete.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
