using System;
using System.Data;
using System.Threading.Tasks;
using App.Core;
using DbConnection = App.Data.SQLite;

namespace RADB
{
    public static class Banco
    {
        public static ListSynced<SqlLog> Log
        {
            get { return Database.Log; }
        }

        private static DatabaseManager Database { get; set; }

        private static bool IsLoaded { get; set; }

        public static void Load()
        {
            IsLoaded = false;
            Database = new DatabaseManager { };
            Reload();
            IsLoaded = true;
        }

        public static void Reload()
        {
            Database.DatabaseName = string.Empty;
            Database.DatabaseType = DatabaseType.SQLite;
            Database.Connection = new DbConnection.SQLite { DefaultTimeout = DatabaseManager.DefaultCommandTimeout }.Connection();
            Database.ServerAddress = string.Empty;

            Database.Username = string.Empty;
            Database.Password = string.Empty;
            Database.DatabaseFile = Session.Options.SystemDatabaseFile;
            Database.ConnectionString = string.Empty;
        }

        public static async Task<DataTable> ExecutarSelect(SqlQuery sql)
        {
            if (IsLoaded)
            {
                try
                {
                    return await Database.ExecuteSelect(sql);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Resolve(ex);
                }
            }

            return new DataTable();
        }

        public static async Task<SqlResult> Executar(SqlQuery sql)
        {
            if (IsLoaded)
            {
                try
                {
                    return await Database.Execute(sql);
                }
                catch (Exception ex)
                {
                    ExceptionManager.Resolve(ex);
                }
            }

            return new SqlResult();
        }

        public static async Task<DateTime> DataServidor()
        {
            if (IsLoaded)
            {
                try
                {
                    return await Database.DateTimeServer();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Resolve(ex);
                }
            }

            return DateTime.MinValue;
        }
    }
}