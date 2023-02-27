using System;
using System.Collections.Generic;
//
using System.Data;
using System.Data.SQLite;
using GNX;
using System.Threading.Tasks;

namespace RADB
{
    public static class Banco
    {
        public static ListBind<cLogSQL> Log { get { return DB.Log; } set { DB.Log = value; } }
        static cDataBase DB { get; set; }
        public static bool Loaded { get; set; }

        public static void Carregar(string servidor = "", string database = "")
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
            if (Loaded) { return await DB.ExecuteSelect(sql, parameters, storedProcedure); }
            return new DataTable();
        }

        public async static Task<cSqlResult> Executar(string sql, DbAction movimento, List<cSqlParameter> parameters)
        {
            if (Loaded) { return await DB.Execute(sql, movimento, parameters); }
            return new cSqlResult();
        }

        public async static Task<int> GetLastID()
        {
            if (Loaded) { return await DB.GetLastID(); }
            return 0;
        }

        public async static Task<DateTime> DataServidor()
        {
            if (Loaded) { return await DB.DateTimeServer(); }
            return DateTime.MinValue;
        }
    }
}