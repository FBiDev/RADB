using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using System.Data;
using RADB.Properties;
using GNX;

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
        static List<cSqlParameter> MountFilters(Console obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Name", obj.Name)
            };
        }
        #endregion

        #region " _MountParameters "
        static List<cSqlParameter> MountParameters(Console obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Name", obj.Name)
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

            return (await Banco.Executar(sql, DbAction.Insert, parameters)).AffectedRows > 0;
        }

        public async static Task<bool> InsertList(IList<Console> list)
        {
            string sql = "INSERT INTO Console (ID, Name) VALUES " + Environment.NewLine;
            var parameters = new List<cSqlParameter>();

            int index = 0;
            foreach (var i in list)
            {
                parameters.AddRange(new List<cSqlParameter>
                {
                    new cSqlParameter("@ID" + index, i.ID),
                    new cSqlParameter("@Name" + index, i.Name)
                });

                sql += "(" + "@ID" + index + ", @Name" + index + ")";

                index++;
                if (index < list.Count) { sql += "," + Environment.NewLine; }
            }

            return (await Banco.Executar(sql, DbAction.Insert, parameters)).AffectedRows > 0;
        }
        #endregion

        #region " _Delete "
        public async static Task<bool> Delete(Console obj)
        {
            string sql = Resources.ConsoleDelete;
            var parameters = new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Name", obj.Name)
            };

            return (await Banco.Executar(sql, DbAction.Delete, parameters)).AffectedRows > 0;
        }
        #endregion
    }
}