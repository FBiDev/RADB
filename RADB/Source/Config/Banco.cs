using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using App.Core;
using App.Data.SQLite;

namespace RADB
{
    public static class Banco
    {
        static DatabaseManager Database { get; set; }
        public static ListSynced<SqlLog> Log { get { return Database.Log; } }
        public static bool Loaded { get; set; }

        public static void Load()
        {
            Database = new DatabaseManager
            {
                DatabaseType = DatabaseType.SQLite,
                Connection = new SQLite { DefaultTimeout = DatabaseManager.DefaultCommandTimeout }.Connection(),
                ServerAddress = "",
                DatabaseName = "",
                DatabaseFile = Session.Options.SystemDatabaseFile,
                Username = "",
                Password = "",
                ConnectionString = ""
            };

            Loaded = true;
        }

        public async static Task<DataTable> ExecutarSelect(string sql, List<SqlParameter> parameters = null, string storedProcedure = default(string))
        {
            if (Loaded)
            {
                try { return await Database.ExecuteSelect(sql, parameters, storedProcedure); }
                catch (Exception ex) { ExceptionManager.Resolve(ex); }
            }
            return new DataTable();
        }

        public async static Task<SqlResult> Executar(string sql, DatabaseAction action, List<SqlParameter> parameters)
        {
            if (Loaded)
            {
                try { return await Database.Execute(sql, action, parameters); }
                catch (Exception ex) { ExceptionManager.Resolve(ex); }
            }
            return new SqlResult();
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