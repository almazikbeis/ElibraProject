using ELibra.DBModel;
using ELibra.DBModel.Models;
using System;
using System.Data.SQLite;
using System.Linq;


namespace ELibra.Classes.Update
{
    class CreateScalesTable
    {
        public static void Create()
        {
            bool execute = false;
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "database" + ".db3;Version=3;MultipleActiveResultSets=true;"))
            {
                connection.Open();
                string stringTableExist = "SELECT count( name) FROM sqlite_sequence WHERE name = 'Scales'";
                SQLiteCommand cmdTableExist = new SQLiteCommand(stringTableExist, connection);
                var isExist = cmdTableExist.ExecuteScalar();
                if (Convert.ToInt32(isExist) == 0)
                {
                    string stringCreate = "CREATE TABLE \"Scales\"(" +
                                                    "\"id\"             INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                    "\"name\"           TEXT UNIQUE," +
                                                    "\"nameUser\"       TEXT," +
                                                    "\"Model\"          TEXT DEFAULT \"IP65\"," +
                                                    "\"PortName\"       TEXT," +
                                                    "\"BaudRate\"       TEXT DEFAULT \"19200\"," +
                                                    "\"DataBits\"       TEXT DEFAULT \"8\"," +
                                                    "\"Parity\"         TEXT DEFAULT \"Нет\"," +
                                                    "\"RtsEnable\"      TEXT DEFAULT \"false\"," + 
                                                    "\"StopBits\"       TEXT DEFAULT \"1\"," +
                                                    "\"Handshake\"      TEXT DEFAULT \"Аппаратное и Xon/Xoff\"," +
                                                    "\"MaxWeight\"      TEXT DEFAULT \"5\"," +
                                                    "\"linkedCameras\"  TEXT," +
                                                    "\"MinWeightVideo\" TEXT DEFAULT \"500\"," +
                                                    "\"isSave\"         TEXT DEFAULT \"false\"," +
                                                    "\"RecognizeDelay\" REAL DEFAULT \"0\" )";
                    SQLiteCommand cmdCreate = new SQLiteCommand(stringCreate, connection);
                    cmdCreate.ExecuteNonQuery();
                    execute = true;
                }
            }
            if (execute)
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    var settingsTable = (from i in context.Settings
                                         select i).FirstOrDefault();
                    var scaleRow = new Scales
                    {
                        name            = "Scales1",
                        nameUser        = "Весы 1",
                        Model           = settingsTable.Model,
                        PortName        = settingsTable.PortName,
                        BaudRate        = settingsTable.BaudRate,
                        DataBits        = settingsTable.DataBits,
                        Parity          = settingsTable.Parity,
                        RtsEnable       = settingsTable.RtsEnable,
                        StopBits        = settingsTable.StopBits,
                        Handshake       = settingsTable.Handshake,
                        MaxWeight       = settingsTable.MaxWeight,
                        MinWeightVideo  = settingsTable.MinWeightVideo,
                        LinkedCameras   = settingsTable.UrlVideoCam,
                        isSave          = settingsTable.isSave,
                        RecognizeDelay  = settingsTable.RecognizeDelay
                    };
                    context.Scales.Add(scaleRow);
                    context.SaveChanges();
                }
            }
        }

    }
}