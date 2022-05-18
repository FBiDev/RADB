using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using GNX;

namespace RADB
{
    public class Banco
    {
        public static ListBind<cLogSQL> Log { get { return DB.Log; } set { DB.Log = value; } }
        private static cDataBase DB { get; set; }
        public static bool ConfigLoaded { get; set; }

        public static void Carregar(string servidor, string database = "")
        {
            DB = new cDataBase
            {
                DatabaseSystem = DbSystem.SQLite,
                Connection = new SQLiteConnection(),
                ServerAddress = servidor,
                DatabaseName = database,
                DataBaseFile = "database.db",
                Username = "",
                Password = "",
                ConnectionString = ""
            };
        }

        public static DataTable ExecutarSelect(string sql, List<cSqlParameter> parameters = null, string storedProcedure = default(string))
        {
            if (ConfigLoaded) { return DB.ExecuteSelect(sql, parameters, storedProcedure); }
            return new DataTable();
        }

        public static cSqlResult Executar(string sql, MovimentoLog movimento, List<cSqlParameter> parameters)
        {
            if (ConfigLoaded) { return DB.Execute(sql, ((DbAction)movimento), parameters); }
            return new cSqlResult();
        }

        public static int GetLastID()
        {
            if (ConfigLoaded) { return DB.GetLastID(); }
            return 0;
        }

        public static DateTime DataServidor()
        {
            if (ConfigLoaded) { return DB.DateTimeServer(); }
            return DateTime.MinValue;
        }
    }
}
