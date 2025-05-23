﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using RADB.Properties;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public static class GameExtendDao
    {
        #region " _Load "
        static T Load<T>(DataTable table) where T : IList, new()
        {
            var list = new T();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new GameExtend
                {
                    ID = row.Value<int>("ID"),
                    ConsoleID = row.Value<int>("ConsoleID"),

                    Developer = row.Value<string>("Developer"),
                    Publisher = row.Value<string>("Publisher"),
                    Genre = row.Value<string>("Genre"),
                    Released = row.Value<string>("Released"),
                    ImageTitle = row.Value<string>("ImageTitle"),
                    ImageIngame = row.Value<string>("ImageIngame"),
                    ImageBoxArt = row.Value<string>("ImageBoxArt")
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
        static List<SqlParameter> MountFilters(GameExtend obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@ConsoleID", obj.ConsoleID)
            };
        }
        #endregion

        #region " _MountParameters "
        static List<SqlParameter> MountParameters(GameExtend obj)
        {
            return new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@ConsoleID", obj.ConsoleID),
                new SqlParameter("@Developer", obj.Developer),
                new SqlParameter("@Publisher", obj.Publisher),
                new SqlParameter("@Genre", obj.Genre),
                new SqlParameter("@Released", obj.Released),
                new SqlParameter("@ImageTitle", obj.ImageTitle),
                new SqlParameter("@ImageIngame", obj.ImageIngame),
                new SqlParameter("@ImageBoxArt", obj.ImageBoxArt)
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

        public async static Task<List<GameExtend>> Search(GameExtend obj)
        {
            string sql = Resources.GameExtendList;
            return Load<List<GameExtend>>(await Banco.ExecutarSelect(sql, MountFilters(obj)));
        }
        #endregion

        #region " _Insert "
        public async static Task<bool> Insert(GameExtend obj)
        {
            string sql = Resources.GameExtendInsert;
            var parameters = MountParameters(obj);

            return (await Banco.Executar(sql, DatabaseAction.Insert, parameters)).AffectedRows > 0;
        }
        #endregion

        #region " _Delete "
        public async static Task<bool> Delete(GameExtend obj)
        {
            string sql = Resources.GameExtendDelete;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ID", obj.ID),
                new SqlParameter("@ConsoleID", obj.ConsoleID)
            };

            return (await Banco.Executar(sql, DatabaseAction.Delete, parameters)).AffectedRows > 0;
        }
        #endregion
    }
}