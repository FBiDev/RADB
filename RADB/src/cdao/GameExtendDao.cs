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
    public class GameExtendDao
    {
        #region " _Load "
        private static T Load<T>(DataTable table) where T : IList, new()
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

        #region " _MountFilters "
        private static List<cSqlParameter> MountFilters(GameExtend obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@ConsoleID", obj.ConsoleID),
            };
        }
        #endregion

        #region " _MountParameters "
        private static List<cSqlParameter> MountParameters(GameExtend obj)
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

        #region " _List "
        public async static Task<List<GameExtend>> List()
        {
            var obj = new GameExtend();
            return (await Search(obj));
        }

        public static async Task<GameExtend> Find(int gameID)
        {
            var obj = new GameExtend { ID = gameID };

            return (await Search(obj)).FirstOrNew();
        }

        public static Task<List<GameExtend>> Search(GameExtend obj)
        {
            return Task<List<GameExtend>>.Run(() =>
            {
                string sql = Resources.GameExtendList;

                return Load<List<GameExtend>>(Banco.ExecutarSelect(sql, MountFilters(obj)));
            });
        }
        #endregion

        #region " _Insert "
        public static Task<bool> Insert(GameExtend obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameExtendInsert;

                var parameters = MountParameters(obj);

                return Banco.Executar(sql, DbAction.Insert, parameters).AffectedRows > 0;
            });
        }
        #endregion

        #region " _Delete "
        public static Task<bool> Delete(GameExtend obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.GameExtendDelete;

                var parameters = new List<cSqlParameter> 
                {
                    new cSqlParameter("@ID", obj.ID),
                    new cSqlParameter("@ConsoleID", obj.ConsoleID),
                };

                return Banco.Executar(sql, DbAction.Delete, parameters).AffectedRows > 0;
            });
        }
        #endregion
    }
}
