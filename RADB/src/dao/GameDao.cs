using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
//
using RADB.Properties;
using GNX;
using System.Threading;

namespace RADB
{
    public class GameDao
    {
        #region " _Carregar "
        private static T Carregar<T>(DataTable table) where T : IList, new()
        {
            T list = new T();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Game()
                {
                    ID = row.Value<int>("ID"),
                    Title = row.Value<string>("Title"),
                    ConsoleID = row.Value<int>("ConsoleID"),
                    NumAchievements = row.Value<int>("NumAchievements"),
                    NumLeaderboards = row.Value<int>("NumLeaderboards"),
                    Points = row.Value<int>("Points"),
                    ImageIcon = row.Value<string>("ImageIcon"),
                });
            }
            return list;
        }
        #endregion

        #region " _MontarFiltros "
        private static List<cSqlParameter> MontarFiltros(Game obj)
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

        #region " _MontarParametros "
        private static List<cSqlParameter> MontarParametros(Game obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Title", obj.Title),
                new cSqlParameter("@ConsoleID", obj.ConsoleID),
                new cSqlParameter("@NumAchievements", obj.NumAchievements),
                new cSqlParameter("@NumLeaderboards", obj.NumLeaderboards),
                new cSqlParameter("@Points", obj.Points),
                new cSqlParameter("@ImageIcon", obj.ImageIcon),
            };
        }
        #endregion

        #region " _Listar "
        public static Task<List<Game>> Listar(Game obj = null)
        {
            return Task<List<Game>>.Run(() =>
            {
                obj = obj ?? new Game();

                //Monta SQL
                string sql = Resources.GameListar;
                sql += " ORDER BY NumAchievements=0, Title ASC ";

                return Carregar<List<Game>>(Banco.ExecutarSelect(sql, MontarFiltros(obj)));
            });
        }

        public static ListBind<Game> OrdenarLista(List<Game> GameList)
        {
            List<string> prefixNotOffical = new List<string> { 
                        "~Demo~", "~Hack~", "~Homebrew~", "~Prototype~", "~Test Kit~", "~Unlicensed~", "~Z~" };

            //Get NotOffical
            List<Game> LNotOffical = GameList.Where(x => prefixNotOffical.Any(y => x.Title.IndexOf(y) >= 0)).ToList();
            //Remove NotOffical from Main List
            GameList = GameList.Except(LNotOffical).ToList();
            //Get Game with no cheevos from NotOffical
            List<Game> LNotOfficalNoCheevos = LNotOffical.Where(x => x.NumAchievements == 0).ToList();
            //Get Games Has Cheevos
            LNotOffical = LNotOffical.Where(x => x.NumAchievements > 0).ToList();
            //Get Game with no cheevos from Main List
            List<Game> LNoCheevos = GameList.Where(x => x.NumAchievements == 0).ToList();
            //Remove Games no Cheevos from Main List
            GameList = GameList.Except(LNoCheevos).ToList();

            //TimeSpan fim = new TimeSpan(DateTime.Now.Ticks) - ini;
            //Join Ordered Lists
            GameList = GameList.OrderBy(x => x.Title).ToList();
            GameList.AddRange(LNotOffical.OrderBy(x => x.Title).ToList());
            GameList.AddRange(LNoCheevos.OrderBy(x => x.Title).ToList());
            GameList.AddRange(LNotOfficalNoCheevos.OrderBy(x => x.Title).ToList());

            return new ListBind<Game>(GameList);
        }
        #endregion

        #region " _Incluir "
        public static bool Incluir(Game obj)
        {
            //Monta SQL
            string sql = Resources.GameIncluir;

            var parametros = MontarParametros(obj);

            return Banco.Executar(sql, MovimentoLog.Inclusão, parametros).AffectedRows > 0;
        }

        public static Task<bool> IncluirLista(IList<Game> list)
        {
            return Task<bool>.Run(() =>
            {
                //Monta SQL
                string sql = "INSERT INTO Game (ID, Title, ConsoleID, NumAchievements, NumLeaderboards, Points, ImageIcon)" +
                             " VALUES " + Environment.NewLine;

                var parametros = new List<cSqlParameter>();

                int index = 0;
                foreach (var i in list)
                {
                    sql += "(" + i.ID +
                            ",'" + i.Title.Replace("'", "''") + "'" +
                            ", " + i.ConsoleID +
                            ", " + i.NumAchievements +
                            ", " + i.NumLeaderboards +
                            ", " + i.Points +
                            ", '" + i.ImageIcon + "')";

                    //parametros.AddRange(new List<cSqlParameter>
                    //{
                    //    new cSqlParameter("@ID" + index, i.ID),
                    //    new cSqlParameter("@Title" + index, i.Title),
                    //    new cSqlParameter("@ConsoleID" + index, i.ConsoleID),
                    //    new cSqlParameter("@NumAchievements" + index, i.NumAchievements),
                    //    new cSqlParameter("@NumLeaderboards" + index, i.NumLeaderboards),
                    //    new cSqlParameter("@Points" + index, i.Points),
                    //    new cSqlParameter("@ImageIcon" + index, i.ImageIcon)
                    //});

                    //sql += "(" + "@ID" + index + ", @Title" + index + ", @ConsoleID" + index +
                    //           ", @NumAchievements" + index + ", @NumLeaderboards" + index +
                    //           ", @Points" + index + ", @ImageIcon" + index +
                    //       ")";

                    index++;
                    if (index < list.Count) { sql += "," + Environment.NewLine; }
                }

                return Banco.Executar(sql, MovimentoLog.Inclusão, parametros).AffectedRows > 0;
            });
        }
        #endregion

        #region " _Excluir "
        public static Task<bool> Excluir(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameExcluir;

                var parametros = new List<cSqlParameter> 
                {
                    new cSqlParameter("@ID", obj.ID),
                    new cSqlParameter("@ConsoleID", obj.ConsoleID),
                };

                return Banco.Executar(sql, MovimentoLog.Exclusão, parametros).AffectedRows > 0;
            });
        }
        #endregion
    }
}
