using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
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
                new cSqlParameter("@ImageIcon", obj.ImageIcon),
                new cSqlParameter("@NumAchievements", obj.NumAchievements),
                new cSqlParameter("@Points", obj.Points),
                new cSqlParameter("@NumLeaderboards", obj.NumLeaderboards),
                new cSqlParameter("@DateModified", obj.DateModified),
                new cSqlParameter("@ForumTopicID", obj.ForumTopicID),
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
                string sql = "INSERT INTO GameData " +
                                "(ID, Title, ConsoleID, NumAchievements, Points, NumLeaderboards, DateModified, ForumTopicID, ImageIcon)" +
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

                var parametros = new List<cSqlParameter>();

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

        #region ToHide
        public static Task<List<Game>> ToHideListar()
        {
            return Task<List<Game>>.Run(() =>
            {
                //Monta SQL
                string sql = Resources.GameToHideListar;

                return Carregar<List<Game>>(Banco.ExecutarSelect(sql, new List<cSqlParameter> { }));
            });
        }

        public static Task<bool> ToHideIncluir(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                //Monta SQL
                string sql = Resources.GameToHideIncluir;

                var parametros = MontarParametros(obj);

                return Banco.Executar(sql, MovimentoLog.Inclusão, parametros).AffectedRows > 0;
            });
        }

        public static Task<bool> ToHideExcluir(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameToHideExcluir;

                var parametros = new List<cSqlParameter> 
                {
                    new cSqlParameter("@ID", obj.ID),
                };

                return Banco.Executar(sql, MovimentoLog.Exclusão, parametros).AffectedRows > 0;
            });
        }
        #endregion

        #region ToPlay
        public static Task<List<Game>> ToPlayListar()
        {
            return Task<List<Game>>.Run(() =>
            {
                //Monta SQL
                string sql = Resources.GameToPlayListar;

                return Carregar<List<Game>>(Banco.ExecutarSelect(sql, new List<cSqlParameter> { }));
            });
        }

        public static Task<bool> ToPlayIncluir(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                //Monta SQL
                string sql = Resources.GameToPlayIncluir;

                var parametros = MontarParametros(obj);

                return Banco.Executar(sql, MovimentoLog.Inclusão, parametros).AffectedRows > 0;
            });
        }

        public static Task<bool> ToPlayExcluir(Game obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameToPlayExcluir;

                var parametros = new List<cSqlParameter> 
                {
                    new cSqlParameter("@ID", obj.ID),
                };

                return Banco.Executar(sql, MovimentoLog.Exclusão, parametros).AffectedRows > 0;
            });
        }
        #endregion
    }
}
