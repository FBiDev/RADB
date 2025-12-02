using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using App.Core;
using RADB.Properties;

namespace RADB
{
    public class ConsoleDao : DaoBase
    {
        #region " _Select "
        public async Task<List<Console>> List()
        {
            return await Select();
        }
        #endregion

        #region " _Actions_Custom "
        public async Task<bool> InsertList(IList<Console> list)
        {
            string query = "INSERT INTO Console (ID, Name) VALUES " + Environment.NewLine;

            var parameters = new List<SqlParameter>();

            int index = 0;
            foreach (var i in list)
            {
                parameters.AddRange(new List<SqlParameter>
                {
                    new SqlParameter("@ID" + index, i.ID),
                    new SqlParameter("@Name" + index, i.Name)
                });

                query += "(" + "@ID" + index + ", @Name" + index + ")";

                index++;
                if (index < list.Count)
                {
                    query += "," + Environment.NewLine;
                }
            }

            var sql = new SqlQuery(query, DatabaseAction.Insert);
            sql.Parameters.AddRange(parameters);

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }
        #endregion

        #region " _Actions "
        public async Task<bool> Insert(Console obj)
        {
            var sql = new SqlQuery(
                Resources.ConsoleInsert,
                DatabaseAction.Insert,
                P("@ID", obj.ID),
                P("@Name", obj.Name));

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }

        public async Task<bool> Delete(Console obj)
        {
            var sql = new SqlQuery(
                Resources.ConsoleDelete,
                DatabaseAction.Delete,
                P("@ID", obj.ID),
                P("@Name", obj.Name));

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }

        private async Task<List<Console>> Select(Console obj = null)
        {
            obj = obj ?? new Console();

            var sql = new SqlQuery(
                Resources.ConsoleList,
                DatabaseAction.Select,
                P("@ID", obj.ID),
                P("@Name", obj.Name));

            var list = Load(await Banco.ExecutarSelect(sql));
            if (list.Count == 0)
            {
                return list;
            }

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

        #region " _Load "
        private List<Console> Load(DataTable table)
        {
            return table.ProcessRows<Console>((row, lst) =>
            {
                var entity = new Console
                {
                    ID = row.Value<int>("ID"),
                    Company = row.Value<string>("Company"),
                    Name = row.Value<string>("CName"),
                    NumGames = row.Value<int>("NumGames"),
                    TotalGames = row.Value<int>("TotalGames")
                };

                lst.Add(entity);
            });
        }
        #endregion
    }
}