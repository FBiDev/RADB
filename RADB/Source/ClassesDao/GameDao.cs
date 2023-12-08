using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RADB.Properties;
using GNX;
using GNX.Desktop;

namespace RADB
{
    public static class GameDao
    {
        #region " _Load "
        static T Load<T>(DataTable table) where T : IList, new()
        {
            var list = new T();
            foreach (DataRow row in table.Rows)
            {
                var obj = new Game
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

                obj.SetYear(row.ValueNullable<DateTime>("ReleasedDate"));
                list.Add(obj);
            }
            return list;
        }
        #endregion

        #region " _MountFilters "
        static List<SqlParameter> MountFilters(Game obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@Title", obj.Title),
                new SqlParameter("@ConsoleID", obj.ConsoleID)
            };
        }
        #endregion

        #region " _MountParameters "
        static List<SqlParameter> MountParameters(Game obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@Title", obj.Title),
                new SqlParameter("@ConsoleID", obj.ConsoleID),
                new SqlParameter("@ImageIcon", obj.ImageIcon),
                new SqlParameter("@NumAchievements", obj.NumAchievements),
                new SqlParameter("@Points", obj.Points),
                new SqlParameter("@NumLeaderboards", obj.NumLeaderboards),
                new SqlParameter("@DateModified", obj.DateModified),
                new SqlParameter("@ForumTopicID", obj.ForumTopicID)
            };
        }
        #endregion

        #region " _List "
        public static async Task<List<Game>> List()
        {
            var obj = new Game();
            return (await Search(obj, true));
        }

        public static async Task<Game> Find(int id)
        {
            var obj = new Game { ID = id };
            return (await Search(obj, false)).FirstOrNew();
        }

        public async static Task<List<Game>> Search(Game obj, bool allTables)
        {
            string sql = Resources.GameList;
            sql += " ORDER BY NumAchievements=0, Title ASC ";

            var parameters = MountFilters(obj);
            parameters.Add(new SqlParameter("@allTables", allTables));

            return Load<List<Game>>(await Banco.ExecutarSelect(sql, parameters));
        }

        public static ListBind<Game> OrderList(List<Game> list)
        {
            //Get NotOffical
            var LNotOffical = list.Where(x => RA.GameType.NotOfficial.Any(x.Title.ContainsExtend));
            //Remove NotOffical from Main List
            list = list.Except(LNotOffical).ToList();
            //Get Game with no cheevos from NotOffical
            var LNotOfficalNoCheevos = LNotOffical.Where(x => x.NumAchievements == 0);
            //Get Games Has Cheevos
            LNotOffical = LNotOffical.Where(x => x.NumAchievements > 0);
            //Get Game with no cheevos from Main List
            var LNoCheevos = list.Where(x => x.NumAchievements == 0);
            //Remove Games no Cheevos from Main List
            list = list.Except(LNoCheevos).ToList();

            //Join Ordered Lists
            list = list.OrderBy(x => x.Title).ToList();
            list.AddRange(LNotOffical.OrderBy(x => x.Title));
            list.AddRange(LNoCheevos.OrderBy(x => x.Title));
            list.AddRange(LNotOfficalNoCheevos.OrderBy(x => x.Title));

            return new ListBind<Game>(list);
        }
        #endregion

        #region " _Insert "
        public async static Task<bool> Insert(Game obj)
        {
            string sql = Resources.GameInsert;
            var parameters = MountParameters(obj);

            return (await Banco.Executar(sql, DatabaseAction.Insert, parameters)).AffectedRows > 0;
        }

        public async static Task<bool> InsertList(IList<Game> list)
        {
            string sql = "INSERT INTO GameData " +
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
                        ", " + i.DateModified.ToDBquoted() + "" +
                        ", " + i.ForumTopicID.ToDB() +
                        ", '" + i.ImageIcon + "')");

                index++;
                if (index < list.Count) { s.Append("," + Environment.NewLine); }
            }
            sql += s.ToString();

            var parameters = new List<SqlParameter>();

            return (await Banco.Executar(sql, DatabaseAction.Insert, parameters)).AffectedRows > 0;
        }
        #endregion

        #region " _Delete "
        public async static Task<bool> Delete(Game obj)
        {
            string sql = Resources.GameDelete;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@ConsoleID", obj.ConsoleID)
            };

            return (await Banco.Executar(sql, DatabaseAction.Delete, parameters)).AffectedRows > 0;
        }
        #endregion

        #region _ToHide
        public async static Task<List<Game>> ListToHide()
        {
            string sql = Resources.GameListToHide;

            return Load<List<Game>>(await Banco.ExecutarSelect(sql));
        }

        public async static Task<bool> InsertToHide(Game obj)
        {
            string sql = Resources.GameInsertToHide;
            var parameters = MountParameters(obj);

            return await Task.Run(async () => (await Banco.Executar(sql, DatabaseAction.Insert, parameters)).AffectedRows > 0);
        }

        public async static Task<bool> DeleteFromHide(Game obj)
        {
            string sql = Resources.GameDeleteFromHide;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID)
            };

            return (await Banco.Executar(sql, DatabaseAction.Delete, parameters)).AffectedRows > 0;
        }
        #endregion

        #region _ToPlay
        public async static Task<List<Game>> ListToPlay()
        {
            string sql = Resources.GameListToPlay;
            return Load<List<Game>>(await Banco.ExecutarSelect(sql));
        }

        public async static Task<bool> InsertToPlay(Game obj)
        {
            string sql = Resources.GameInsertToPlay;
            var parameters = MountParameters(obj);

            return await Task.Run(async () => (await Banco.Executar(sql, DatabaseAction.Insert, parameters)).AffectedRows > 0);
        }

        public async static Task<bool> DeleteFromPlay(Game obj)
        {
            string sql = Resources.GameDeleteFromPlay;

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID)
            };

            return (await Banco.Executar(sql, DatabaseAction.Delete, parameters)).AffectedRows > 0;
        }
        #endregion

        public async static Task<bool> InsertReleasedDate(Game obj)
        {
            string sql = Resources.GameInsertReleasedDate;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@ConsoleID", obj.ConsoleID),
                new SqlParameter("@ReleasedDate", obj.ReleasedDate)
            };

            return (await Banco.Executar(sql, DatabaseAction.Insert, parameters)).AffectedRows > 0;
        }

        public async static Task<List<Game>> ListNotInReleasedDate(int consoleID)
        {
            string sql = Resources.GameNotInReleasedDate;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ConsoleID", consoleID)
            };
            return LoadReleasedDate<List<Game>>(await Banco.ExecutarSelect(sql, parameters));
        }

        static T LoadReleasedDate<T>(DataTable table) where T : IList, new()
        {
            var list = new T();
            foreach (DataRow row in table.Rows)
            {
                var obj = new Game
                {
                    ID = row.Value<int>("ID"),
                    ConsoleID = row.Value<int>("ConsoleID")
                };

                list.Add(obj);
            }
            return list;
        }
    }
}