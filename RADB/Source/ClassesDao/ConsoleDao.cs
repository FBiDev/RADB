using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using RADB.Properties;
using GNX;
using GNX.Desktop;

namespace RADB
{
    public static class ConsoleDao
    {
        #region " _Load "
        static T Load<T>(DataTable table) where T : IList, new()
        {
            var list = new T();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Console
                {
                    ID = row.Value<int>("ID"),
                    Company = row.Value<string>("Company"),
                    Name = row.Value<string>("CName"),
                    NumGames = row.Value<int>("NumGames"),
                    TotalGames = row.Value<int>("TotalGames")
                });
            }
            return list;
        }
        #endregion

        #region " _MountFilters "
        static List<SqlParameter> MountFilters(Console obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@Name", obj.Name)
            };
        }
        #endregion

        #region " _MountParameters "
        static List<SqlParameter> MountParameters(Console obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@Name", obj.Name)
            };
        }
        #endregion

        #region " _List "
        public async static Task<List<Console>> List()
        {
            string sql = Resources.ConsoleList;
            var obj = new Console();

            var list = Load<List<Console>>(await Banco.ExecutarSelect(sql, MountFilters(obj)));
            if (list.Count == 0) return list;

            var allGames = new Console
            {
                Name = "All Games",
                Company = "All Games",
                NumGames = list.Sum(x => x.NumGames),
                TotalGames = list.Sum(x => x.TotalGames)
            };

            list.Insert(0, allGames);

            return list;
        }
        #endregion

        #region " _Insert "
        public async static Task<bool> Insert(Console obj)
        {
            string sql = Resources.ConsoleInsert;

            var parameters = MountParameters(obj);

            return (await Banco.Executar(sql, DatabaseAction.Insert, parameters)).AffectedRows > 0;
        }

        public async static Task<bool> InsertList(IList<Console> list)
        {
            string sql = "INSERT INTO Console (ID, Name) VALUES " + Environment.NewLine;
            var parameters = new List<SqlParameter>();

            int index = 0;
            foreach (var i in list)
            {
                parameters.AddRange(new List<SqlParameter>
                {
                    new SqlParameter("@ID" + index, i.ID),
                    new SqlParameter("@Name" + index, i.Name)
                });

                sql += "(" + "@ID" + index + ", @Name" + index + ")";

                index++;
                if (index < list.Count) { sql += "," + Environment.NewLine; }
            }

            return (await Banco.Executar(sql, DatabaseAction.Insert, parameters)).AffectedRows > 0;
        }
        #endregion

        #region " _Delete "
        public async static Task<bool> Delete(Console obj)
        {
            string sql = Resources.ConsoleDelete;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@Name", obj.Name)
            };

            return (await Banco.Executar(sql, DatabaseAction.Delete, parameters)).AffectedRows > 0;
        }
        #endregion
    }
}