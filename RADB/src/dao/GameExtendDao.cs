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
    public class GameExtendDao
    {
        #region " _Carregar "
        private static T Carregar<T>(DataTable table) where T : IList, new()
        {
            T list = new T();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new GameExtend()
                {
                    ID = row.Value<int>("ID"),
                    ConsoleID = row.Value<int>("ConsoleID"),

                    Developer = row.Value<string>("Developer"),
                    Publisher = row.Value<string>("Publisher"),
                    Genre = row.Value<string>("Genre"),
                    Released = row.Value<string>("Released"),
                    ImageTitle = row.Value<string>("ImageTitle"),
                    ImageIngame = row.Value<string>("ImageIngame"),
                    ImageBoxArt = row.Value<string>("ImageBoxArt"),
                    //Flags = row.Value<string>("Flags"),
                    //IsFinal = row.Value<bool>("IsFinal"),
                    //RichPresencePatch = row.Value<string>("RichPresencePatch"),
                    //NumDistinctPlayersCasual = row.Value<int>("NumDistinctPlayersCasual"),
                    //NumDistinctPlayersHardcore = row.Value<int>("NumDistinctPlayersHardcore"),
                });
            }
            return list;
        }
        #endregion

        #region " _MontarFiltros "
        private static List<cSqlParameter> MontarFiltros(GameExtend obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@ConsoleID", obj.ConsoleID),
            };
        }
        #endregion

        #region " _MontarParametros "
        private static List<cSqlParameter> MontarParametros(GameExtend obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@ConsoleID", obj.ConsoleID),
                new cSqlParameter("@Developer", obj.Developer),
                new cSqlParameter("@Publisher", obj.Publisher),
                new cSqlParameter("@Genre", obj.Genre),
                new cSqlParameter("@Released", obj.Released),
                new cSqlParameter("@ImageTitle", obj.ImageTitle),
                new cSqlParameter("@ImageIngame", obj.ImageIngame),
                new cSqlParameter("@ImageBoxArt", obj.ImageBoxArt),
            };
        }
        #endregion

        #region " _Listar "
        public static Task<GameExtend> List(GameExtend obj = null)
        {
            return Task<GameExtend>.Run(() =>
            {
                obj = obj ?? new GameExtend();

                //Monta SQL
                string sql = Resources.GameExtendList;

                List<GameExtend> list = Carregar<List<GameExtend>>(Banco.ExecutarSelect(sql, MontarFiltros(obj)));
                if (list.Empty())
                {
                    return new GameExtend();
                }
                return list[0];
            });
        }
        #endregion

        #region " _Incluir "
        public static bool Insert(GameExtend obj)
        {
            //Monta SQL
            string sql = Resources.GameExtendInsert;

            var parametros = MontarParametros(obj);

            return Banco.Executar(sql, MovimentoLog.Inclusão, parametros).AffectedRows > 0;
        }
        #endregion

        #region " _Excluir "
        public static Task<bool> Delete(GameExtend obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameExtendDelete;

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
