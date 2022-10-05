using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.Data;
using RADB.Properties;
using GNX;

namespace RADB
{
    public class ConsoleDao
    {
        #region " _Load "
        private static T Load<T>(DataTable table) where T : IList, new()
        {
            T list = new T();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Console()
                {
                    ID = row.Value<int>("ID"),
                    Company = row.Value<string>("Company"),
                    Name = row.Value<string>("CName"),
                    NumGames = row.Value<int>("NumGames"),
                    TotalGames = row.Value<int>("TotalGames"),
                });
            }
            return list;
        }
        #endregion

        #region " _MountFilters "
        private static List<cSqlParameter> MountFilters(Console obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Name", obj.Name),
            };
        }
        #endregion

        #region " _MountParameters "
        private static List<cSqlParameter> MountParameters(Console obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Name", obj.Name),
            };
        }
        #endregion

        #region " _Listar "
        public static Task<List<Console>> List()
        {
            return Task<List<Console>>.Run(() =>
            {
                var obj = new Console();

                string sql = Resources.ConsoleList;

                return Load<List<Console>>(Banco.ExecutarSelect(sql, MountFilters(obj)));
            });
        }
        #endregion

        #region " _Insert "
        public static bool Insert(Console obj)
        {
            string sql = Resources.ConsoleInsert;

            var parameters = MountParameters(obj);

            return Banco.Executar(sql, DbAction.Insert, parameters).AffectedRows > 0;
        }

        public static Task<bool> InsertList(IList<Console> list)
        {
            return Task<bool>.Run(() =>
            {
                string sql = "INSERT INTO Console (ID, Name) VALUES " + Environment.NewLine; ;

                var parameters = new List<cSqlParameter>();

                int index = 0;
                foreach (var i in list)
                {
                    parameters.AddRange(new List<cSqlParameter>
                    {
                        new cSqlParameter("@ID" + index, i.ID),
                        new cSqlParameter("@Name" + index, i.Name),
                    });

                    sql += "(" + "@ID" + index + ", @Name" + index + ")";

                    index++;
                    if (index < list.Count) { sql += "," + Environment.NewLine; }
                }

                return Banco.Executar(sql, DbAction.Insert, parameters).AffectedRows > 0;
            });
        }
        #endregion

        #region " _Delete "
        public static Task<bool> Delete(Console obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.ConsoleDelete;

                var parameters = new List<cSqlParameter> 
                {
                    new cSqlParameter("@ID", obj.ID),
                    new cSqlParameter("@Name", obj.Name),
                };

                return Banco.Executar(sql, DbAction.Delete, parameters).AffectedRows > 0;
            });
        }
        #endregion
    }
}
