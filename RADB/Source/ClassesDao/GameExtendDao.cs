using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using App.Core;
using RADB.Properties;

namespace RADB
{
    public class GameExtendDao : DaoBase
    {
        #region " _Select "
        public async Task<List<GameExtend>> List()
        {
            return await Select();
        }

        public async Task<List<GameExtend>> Search(GameExtend obj)
        {
            return await Select(obj);
        }

        public async Task<GameExtend> Find(int gameID)
        {
            var obj = new GameExtend { ID = gameID };
            return (await Select(obj)).First();
        }
        #endregion

        #region " _Actions "
        public async Task<bool> Insert(GameExtend obj)
        {
            var sql = new SqlQuery(
                Resources.GameExtendInsert,
                DatabaseAction.Insert,
                P("@ID", obj.ID),
                P("@ConsoleID", obj.ConsoleID),
                P("@Developer", obj.Developer),
                P("@Publisher", obj.Publisher),
                P("@Genre", obj.Genre),
                P("@Released", obj.Released),
                P("@ImageTitle", obj.ImageTitle),
                P("@ImageIngame", obj.ImageIngame),
                P("@ImageBoxArt", obj.ImageBoxArt));

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }

        public async Task<bool> Delete(GameExtend obj)
        {
            var sql = new SqlQuery(
                Resources.GameExtendDelete,
                DatabaseAction.Delete,
                P("@ID", obj.ID),
                P("@ConsoleID", obj.ConsoleID));

            return (await Banco.Executar(sql)).AffectedRows > 0;
        }

        private async Task<List<GameExtend>> Select(GameExtend obj = null)
        {
            obj = obj ?? new GameExtend();

            var sql = new SqlQuery(
                Resources.GameExtendList,
                DatabaseAction.Select,
                P("@ID", obj.ID),
                P("@ConsoleID", obj.ConsoleID));

            return Load(await Banco.ExecutarSelect(sql));
        }
        #endregion

        #region " _Load "
        private List<GameExtend> Load(DataTable table)
        {
            return table.ProcessRows<GameExtend>((row, lst) =>
            {
                var entity = new GameExtend
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

                    // Flags = row.Value<string>("Flags"),
                    // IsFinal = row.Value<bool>("IsFinal"),
                    // RichPresencePatch = row.Value<string>("RichPresencePatch"),
                    // NumDistinctPlayersCasual = row.Value<int>("NumDistinctPlayersCasual"),
                    // NumDistinctPlayersHardcore = row.Value<int>("NumDistinctPlayersHardcore"),
                };

                lst.Add(entity);
            });
        }
        #endregion
    }
}