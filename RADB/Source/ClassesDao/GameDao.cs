using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Core;
using App.Core.Desktop;
using RADB.Properties;

namespace RADB
{
    public class GameDao : DaoBase
    {
        #region " _Select "
        public async Task<List<Game>> List()
        {
            var obj = new Game();
            return await Select(obj, true);
        }

        public async Task<List<Game>> Search(Game obj, bool allTables)
        {
            return await Select(obj, allTables);
        }

        public async Task<Game> Find(int id)
        {
            var obj = new Game { ID = id };
            return (await Select(obj, false)).First();
        }

        private async Task<List<Game>> Select(Game obj = null, bool allTables = false)
        {
            obj = obj ?? new Game();

            string query = Resources.GameList;
            query += " ORDER BY NumAchievements=0, Title ASC ";

            var sql = new SqlQuery(
                query,
                DatabaseAction.Select,
                P("@ID", obj.ID),
                P("@Title", obj.Title),
                P("@ConsoleID", obj.ConsoleID),
                P("@allTables", allTables));

            return Load(await Banco.ExecutarSelect(sql));
        }
        #endregion

        #region " _Select_Custom "
        public async Task<List<Game>> ListNotInReleasedDate(int consoleID)
        {
            var sql = new SqlQuery(
                Resources.GameNotInReleasedDate,
                DatabaseAction.Select,
                P("@ConsoleID", consoleID));

            return LoadReleasedDate(await Banco.ExecutarSelect(sql));
        }

        public ListBind<Game> OrderList(List<Game> list)
        {
            // Get NotOffical
            var lNotOffical = list.Where(x => RA.GameType.NotOfficial.Any(x.Title.ContainsExtend));

            // Remove NotOffical from Main List
            list = list.Except(lNotOffical).ToList();

            // Get Game with no cheevos from NotOffical
            var lNotOfficalNoCheevos = lNotOffical.Where(x => x.NumAchievements == 0);

            // Get Games Has Cheevos
            lNotOffical = lNotOffical.Where(x => x.NumAchievements > 0);

            // Get Game with no cheevos from Main List
            var lNoCheevos = list.Where(x => x.NumAchievements == 0);

            // Remove Games no Cheevos from Main List
            list = list.Except(lNoCheevos).ToList();

            // Join Ordered Lists
            list = list.OrderBy(x => x.Title).ToList();
            list.AddRange(lNotOffical.OrderBy(x => x.Title));
            list.AddRange(lNoCheevos.OrderBy(x => x.Title));
            list.AddRange(lNotOfficalNoCheevos.OrderBy(x => x.Title));

            return new ListBind<Game>(list);
        }
        #endregion

        #region " _Actions "
        public async Task<bool> Insert(Game obj)
        {
            var sql = new SqlQuery(
                Resources.GameInsert,
                DatabaseAction.Insert,
                P("@ID", obj.ID),
                P("@Title", obj.Title),
                P("@ConsoleID", obj.ConsoleID),
                P("@ImageIcon", obj.ImageIcon),
                P("@NumAchievements", obj.NumAchievements),
                P("@Points", obj.Points),
                P("@NumLeaderboards", obj.NumLeaderboards),
                P("@DateModified", obj.DateModified),
                P("@ForumTopicID", obj.ForumTopicID));

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }

        public async Task<bool> Delete(Game obj)
        {
            var sql = new SqlQuery(
                Resources.GameDelete,
                DatabaseAction.Delete,
                P("@ID", obj.ID),
                P("@ConsoleID", obj.ConsoleID));

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }
        #endregion

        #region " _Actions_Custom "
        public async Task<bool> InsertList(IList<Game> list)
        {
            string query = "INSERT INTO GameData " +
                "(ID, Title, ConsoleID, NumAchievements, Points, " +
                "NumLeaderboards, DateModified, ForumTopicID, ImageIcon)" +
            " VALUES " + Environment.NewLine;

            var s = new StringBuilder();
            int index = 0;

            foreach (var i in list)
            {
                s.Append("(" + i.ID +
                        ",'" + i.Title.Replace("'", "''") + "'" +
                        ", " + i.ConsoleID +
                        ", " + i.NumAchievements +
                        ", " + i.Points +
                        ", " + i.NumLeaderboards +
                        ", " + i.DateModified.ToDBquoted() + string.Empty +
                        ", " + i.ForumTopicID.ToDB() +
                        ", '" + i.ImageIcon + "')");

                index++;

                if (index < list.Count)
                {
                    s.Append("," + Environment.NewLine);
                }
            }

            query += s.ToString();

            var sql = new SqlQuery(query, DatabaseAction.Insert);

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }

        public async Task<bool> InsertReleasedDate(Game obj)
        {
            var sql = new SqlQuery(
                Resources.GameInsertReleasedDate,
                DatabaseAction.Insert,
                P("@ID", obj.ID),
                P("@ConsoleID", obj.ConsoleID),
                P("@ReleasedDate", obj.ReleasedDate));

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }
        #endregion

        #region _ToHide
        public async Task<List<Game>> ListToHide()
        {
            var sql = new SqlQuery(
                Resources.GameListToHide,
                DatabaseAction.Select);

            return Load(await Banco.ExecutarSelect(sql));
        }

        public async Task<bool> InsertToHide(Game obj)
        {
            var sql = new SqlQuery(
                Resources.GameInsertToHide,
                DatabaseAction.Insert,
                P("@ID", obj.ID),
                P("@Title", obj.Title),
                P("@ConsoleID", obj.ConsoleID),
                P("@ImageIcon", obj.ImageIcon),
                P("@NumAchievements", obj.NumAchievements),
                P("@Points", obj.Points),
                P("@NumLeaderboards", obj.NumLeaderboards),
                P("@DateModified", obj.DateModified),
                P("@ForumTopicID", obj.ForumTopicID));

            return await Task.Run(async () => (await Banco.Executar(sql)).AffectedRows > 0);
        }

        public async Task<bool> DeleteFromHide(Game obj)
        {
            var sql = new SqlQuery(
                Resources.GameDeleteFromHide,
                DatabaseAction.Delete,
                P("@ID", obj.ID));

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }
        #endregion

        #region _ToPlay
        public async Task<List<Game>> ListToPlay()
        {
            var sql = new SqlQuery(Resources.GameListToPlay, DatabaseAction.Select);
            return Load(await Banco.ExecutarSelect(sql));
        }

        public async Task<bool> InsertToPlay(Game obj)
        {
            var sql = new SqlQuery(
                Resources.GameInsertToPlay,
                DatabaseAction.Insert,
                P("@ID", obj.ID),
                P("@Title", obj.Title),
                P("@ConsoleID", obj.ConsoleID),
                P("@ImageIcon", obj.ImageIcon),
                P("@NumAchievements", obj.NumAchievements),
                P("@Points", obj.Points),
                P("@NumLeaderboards", obj.NumLeaderboards),
                P("@DateModified", obj.DateModified),
                P("@ForumTopicID", obj.ForumTopicID));

            return await Task.Run(async () => (await Banco.Executar(sql)).AffectedRows > 0);
        }

        public async Task<bool> DeleteFromPlay(Game obj)
        {
            var sql = new SqlQuery(
                Resources.GameDeleteFromPlay,
                DatabaseAction.Delete,
                P("@ID", obj.ID));

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }
        #endregion

        #region " _Load "
        private List<Game> LoadReleasedDate(DataTable table)
        {
            return table.ProcessRows<Game>((row, lst) =>
            {
                var entity = new Game
                {
                    ID = row.Value<int>("ID"),
                    ConsoleID = row.Value<int>("ConsoleID")
                };

                lst.Add(entity);
            });
        }

        private List<Game> Load(DataTable table)
        {
            return table.ProcessRows<Game>((row, lst) =>
            {
                var entity = new Game
                {
                    ID = row.Value<int>("ID"),
                    Title = row.Value<string>("Title"),
                    ConsoleID = row.Value<int>("ConsoleID"),
                    ConsoleName = row.Value<string>("ConsoleName"),
                    ConsoleNameShort = row.Value<string>("ConsoleNameShort"),
                    ImageIcon = row.Value<string>("ImageIcon"),
                    NumAchievements = row.Value<int>("NumAchievements"),
                    Points = row.Value<int>("Points"),
                    NumLeaderboards = row.Value<int>("NumLeaderboards"),
                    DateModified = row.ValueNullable<DateTime>("DateModified"),
                    ForumTopicID = row.ValueNullable<int>("ForumTopicID")
                };

                entity.SetYear(row.ValueNullable<DateTime>("ReleasedDate"));
                lst.Add(entity);
            });
        }
        #endregion
    }
}