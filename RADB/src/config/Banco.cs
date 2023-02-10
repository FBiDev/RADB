using System;
using System.Collections.Generic;
//
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using GNX;
using System.Threading.Tasks;

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
                DataBaseFile = @"Data\database.db",
                Username = "",
                Password = "",
                ConnectionString = ""
            };
        }

        public async static Task<DataTable> ExecutarSelect(string sql, List<cSqlParameter> parameters = null, string storedProcedure = default(string))
        {
            if (ConfigLoaded) { return await DB.ExecuteSelect(sql, parameters, storedProcedure); }
            return new DataTable();
        }

        public async static Task<cSqlResult> Executar(string sql, DbAction movimento, List<cSqlParameter> parameters)
        {
            if (ConfigLoaded) { return await DB.Execute(sql, ((DbAction)movimento), parameters); }
            return new cSqlResult();
        }

        public async static Task<int> GetLastID()
        {
            if (ConfigLoaded) { return await DB.GetLastID(); }
            return 0;
        }

        public async static Task<DateTime> DataServidor()
        {
            if (ConfigLoaded) { return await DB.DateTimeServer(); }
            return DateTime.MinValue;
        }
    }
}
