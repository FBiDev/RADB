using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
//
using RADB.Properties;
using GNX;

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
                sql += " ORDER BY Title ASC ";

                List<Game> GameList = Carregar<List<Game>>(Banco.ExecutarSelect(sql, MontarFiltros(obj)));
                return OrdenarLista(GameList);
            });
        }

        public static List<Game> OrdenarLista(List<Game> GameList)
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

            return GameList;
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

        public static bool IncluirLista(ListBind<Game> list)
        {
            //Monta SQL
            //string sql = Resources.GameIncluir;
            string sql = "INSERT INTO game (Title) VALUES ";

            list.ToList().ForEach(g => sql += "('" + g.Title.Replace("'", "''") + "'),");

            sql = sql.Substring(0, sql.Length - 1);

            //var parametros = MontarParametros(obj);

            return Banco.Executar(sql, MovimentoLog.Inclusão, new List<cSqlParameter>()).AffectedRows > 0;
        }
        #endregion

        #region " _Excluir "
        public static bool Excluir(Game obj)
        {
            string sql = Resources.GameExcluir;

            var parametros = new List<cSqlParameter> 
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@ConsoleID", obj.ConsoleID),
            };

            return Banco.Executar(sql, MovimentoLog.Exclusão, parametros).AffectedRows > 0;
        }
        #endregion
    }
}
