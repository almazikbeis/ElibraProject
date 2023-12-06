using System;
using System.Data.SQLite;


namespace ELibra.Classes.Update
{
    class AddWeigingColumn
    {
        public static void AddScaleNameColumn()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "database" + ".db3;Version=3;MultipleActiveResultSets=true;"))
            {
                connection.Open();
                string stringColumnExist = "SELECT name from pragma_table_info('Weighings') where name = 'taraScale'";

                SQLiteCommand cmdColumnExist = new SQLiteCommand(stringColumnExist, connection);
                var isExist = cmdColumnExist.ExecuteScalar();
                if (isExist == null)
                {
                    string stringCreate = "ALTER TABLE Weighings ADD taraScale TEXT";
                    SQLiteCommand cmdCrete = new SQLiteCommand(stringCreate, connection);
                    cmdCrete.ExecuteNonQuery();
                }

                stringColumnExist = "SELECT name from pragma_table_info('Weighings') where name = 'fullWeightScale'";

                cmdColumnExist = new SQLiteCommand(stringColumnExist, connection);
                isExist = cmdColumnExist.ExecuteScalar();
                if (isExist == null)
                {
                    string stringCreate = "ALTER TABLE Weighings ADD fullWeightScale TEXT";
                    SQLiteCommand cmdCrete = new SQLiteCommand(stringCreate, connection);
                    cmdCrete.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public static void AddRedactorColumn()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "database" + ".db3;Version=3;MultipleActiveResultSets=true;"))
            {
                connection.Open();
                string stringColumnExist = "SELECT name from pragma_table_info('Weighings') where name = 'edited'";

                SQLiteCommand cmdColumnExist = new SQLiteCommand(stringColumnExist, connection);
                var isExist = cmdColumnExist.ExecuteScalar();
                if (isExist == null)
                {
                    string stringCreate = "ALTER TABLE Weighings ADD edited TEXT";
                    SQLiteCommand cmdCrete = new SQLiteCommand(stringCreate, connection);
                    cmdCrete.ExecuteNonQuery();
                }

                stringColumnExist = "SELECT name from pragma_table_info('Weighings') where name = 'redactor'";

                cmdColumnExist = new SQLiteCommand(stringColumnExist, connection);
                isExist = cmdColumnExist.ExecuteScalar();
                if (isExist == null)
                {
                    string stringCreate = "ALTER TABLE Weighings ADD redactor TEXT";
                    SQLiteCommand cmdCrete = new SQLiteCommand(stringCreate, connection);
                    cmdCrete.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
