﻿using System;
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
    public class GameDao
    {
        #region " _Load "
        private static T Load<T>(DataTable table) where T : IList, new()
        {
            T list = new T();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Game()
                {
                    ID = row.Value<int>("ID"),
                    Title = row.Value<string>("Title"),
                    ConsoleID = row.Value<int>("ConsoleID"),
                    ConsoleName = row.Value<string>("ConsoleName"),
                    ImageIcon = row.Value<string>("ImageIcon"),
                    NumAchievements = row.Value<int>("NumAchievements"),
                    Points = row.Value<int>("Points"),
                    NumLeaderboards = row.Value<int>("NumLeaderboards"),
                    DateModified = row.ValueNullable<DateTime>("DateModified"),
                    ForumTopicID = row.ValueNullable<int>("ForumTopicID"),
                });
            }
            return list;
        }
        #endregion

        #region " _MountFilters "
        private static List<cSqlParameter> MountFilters(Game obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Title", obj.Title),
                new cSqlParameter("@ConsoleID", obj.ConsoleID),
                new cSqlParameter("@ImageIcon", obj.ImageIcon),
            };
        }
        #endregion

        #region " _MountParameters "
        private static List<cSqlParameter> MountParameters(Game obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Title", obj.Title),
                new cSqlParameter("@ConsoleID", obj.ConsoleID),
                new cSqlParameter("@ImageIcon", obj.ImageIcon),
                new cSqlParameter("@NumAchievements", obj.NumAchievements),
                new cSqlParameter("@Points", obj.Points),
                new cSqlParameter("@NumLeaderboards", obj.NumLeaderboards),
                new cSqlParameter("@DateModified", obj.DateModified),
                new cSqlParameter("@ForumTopicID", obj.ForumTopicID),
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

        public static Task<List<Game>> Search(Game obj, bool allTables)
        {
            return Task<List<Game>>.Run(() =>
            {
                string sql = Resources.GameList;
                sql += " ORDER BY NumAchievements=0, Title ASC ";

                var parameters = MountFilters(obj);
                parameters.Add(new cSqlParameter("@allTables", allTables));

                return Load<List<Game>>(Banco.ExecutarSelect(sql, parameters));
            });
        }

        public static ListBind<Game> OrderList(List<Game> list)
        {
            List<string> prefixNotOffical = new List<string> { 
                        "~Demo~", "~Hack~", "~Homebrew~", "~Prototype~", "~Test Kit~", "~Unlicensed~", "~Z~" };

            //Get NotOffical
            List<Game> LNotOffical = list.Where(x => prefixNotOffical.Any(y => x.Title.IndexOf(y) >= 0)).ToList();
            //Remove NotOffical from Main List
            list = list.Except(LNotOffical).ToList();
            //Get Game with no cheevos from NotOffical
            List<Game> LNotOfficalNoCheevos = LNotOffical.Where(x => x.NumAchievements == 0).ToList();
            //Get Games Has Cheevos
            LNotOffical = LNotOffical.Where(x => x.NumAchievements > 0).ToList();
            //Get Game with no cheevos from Main List
            List<Game> LNoCheevos = list.Where(x => x.NumAchievements == 0).ToList();
            //Remove Games no Cheevos from Main List
            list = list.Except(LNoCheevos).ToList();

            //TimeSpan fim = new TimeSpan(DateTime.Now.Ticks) - ini;
            //Join Ordered Lists
            list = list.OrderBy(x => x.Title).ToList();
            list.AddRange(LNotOffical.OrderBy(x => x.Title).ToList());
            list.AddRange(LNoCheevos.OrderBy(x => x.Title).ToList());
            list.AddRange(LNotOfficalNoCheevos.OrderBy(x => x.Title).ToList());

            return new ListBind<Game>(list);
        }
        #endregion

        #region " _Insert "
        public static bool Insert(Game obj)
        {
            string sql = Resources.GameInsert;

            var parameters = MountParameters(obj);

            return Banco.Executar(sql, DbAction.Insert, parameters).AffectedRows > 0;
        }

        public static Task<bool> InsertList(IList<Game> list)
        {
            return Task<bool>.Run(() =>
            {
                string sql = "INSERT INTO GameData " +
                                "(ID, Title, ConsoleID, NumAchievements, Points, " +
                                "NumLeaderboards, DateModified, ForumTopicID, ImageIcon)" +
                            " VALUES " + Environment.NewLine;

                StringBuilder s = new StringBuilder();
                int index = 0;
                foreach (var i in list)
                {
                    s.Append("(" + i.ID +
                            ",'" + i.Title.Replace("'", "''") + "'" +
                            ", " + i.ConsoleID +
                            ", " + i.NumAchievements +
                            ", " + i.Points +
                            ", " + i.NumLeaderboards +
                            ", " + i.DateModified.ToDB() +
                            ", " + i.ForumTopicID.ToDB() +
                            ", '" + i.ImageIcon + "')");

                    index++;
                    if (index < list.Count) { s.Append("," + Environment.NewLine); }
                }
                sql += s.ToString();

                var parameters = new List<cSqlParameter>();

                return Banco.Executar(sql, DbAction.Insert, parameters).AffectedRows > 0;
            });
        }
        #endregion

        #region " _Delete "
        public static Task<bool> Delete(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameDelete;

                var parameters = new List<cSqlParameter> 
                {
                    new cSqlParameter("@ID", obj.ID),
                    new cSqlParameter("@ConsoleID", obj.ConsoleID),
                };

                return Banco.Executar(sql, DbAction.Delete, parameters).AffectedRows > 0;
            });
        }
        #endregion

        #region _ToHide
        public static Task<List<Game>> ListToHide()
        {
            return Task<List<Game>>.Run(() =>
            {
                string sql = Resources.GameListToHide;

                return Load<List<Game>>(Banco.ExecutarSelect(sql));
            });
        }

        public static Task<bool> InsertToHide(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameInsertToHide;

                var parameters = MountParameters(obj);

                return Banco.Executar(sql, DbAction.Insert, parameters).AffectedRows > 0;
            });
        }

        public static Task<bool> DeleteFromHide(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameDeleteFromHide;

                var parameters = new List<cSqlParameter> 
                {
                    new cSqlParameter("@ID", obj.ID),
                };

                return Banco.Executar(sql, DbAction.Delete, parameters).AffectedRows > 0;
            });
        }
        #endregion

        #region _ToPlay
        public static Task<List<Game>> ListToPlay()
        {
            return Task<List<Game>>.Run(() =>
            {
                string sql = Resources.GameListToPlay;

                return Load<List<Game>>(Banco.ExecutarSelect(sql));
            });
        }

        public static Task<bool> InsertToPlay(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameInsertToPlay;

                var parameters = MountParameters(obj);

                return Banco.Executar(sql, DbAction.Insert, parameters).AffectedRows > 0;
            });
        }

        public static Task<bool> DeleteFromPlay(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameDeleteFromPlay;

                var parameters = new List<cSqlParameter> 
                {
                    new cSqlParameter("@ID", obj.ID),
                };

                return Banco.Executar(sql, DbAction.Delete, parameters).AffectedRows > 0;
            });
        }
        #endregion
    }
}