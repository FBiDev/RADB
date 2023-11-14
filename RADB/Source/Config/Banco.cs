using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using GNX;
using GNX.Desktop;

namespace RADB
{
    public static class Banco
    {
        static DatabaseManager Database { get; set; }
        public static ListSynced<cLogSQL> Log { get { return Database.Log; } }
        public static bool Loaded { get; set; }

        public static void Load()
        {
            Database = new DatabaseManager
            {
                DatabaseSystem = DbSystem.SQLite,
                Connection = new SQLiteConnection(),
                ServerAddress = "",
                DatabaseName = "",
                DataBaseFile = Session.Options.SystemDatabaseFile,
                Username = "",
                Password = "",
                ConnectionString = ""
            };

            Loaded = true;
        }

        public async static Task<DataTable> ExecutarSelect(string sql, List<cSqlParameter> parameters = null, string storedProcedure = default(string))
        {
            if (Loaded)
            {
                try { return await Database.ExecuteSelect(sql, parameters, storedProcedure); }
                catch (Exception ex) { ExceptionManager.Resolve(ex); }
            }
            return new DataTable();
        }

        public async static Task<cSqlResult> Executar(string sql, DbAction action, List<cSqlParameter> parameters)
        {
            if (Loaded)
            {
                try { return await Database.Execute(sql, action, parameters); }
                catch (Exception ex) { ExceptionManager.Resolve(ex); }
            }
            return new cSqlResult();
        }

        public async static Task<DateTime> DataServidor()
        {
            if (Loaded)
            {
                try { return await Database.DateTimeServer(); }
                catch (Exception ex) { ExceptionManager.Resolve(ex); }
            }
            return DateTime.MinValue;
        }
    }
}