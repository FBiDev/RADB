using System.Collections.Generic;
using System.Threading.Tasks;

namespace RADB
{
    public class Console
    {
        private static readonly ConsoleDao DAO = new ConsoleDao();

        public Console()
        {
            Company = Name = string.Empty;
        }

        public int ID { get; set; }

        public string Company { get; set; }

        public string Name { get; set; }

        public int NumGames { get; set; }

        public int TotalGames { get; set; }

        public static async Task<List<Console>> List()
        {
            return await DAO.List();
        }

        public static async Task<bool> SaveList(IList<Console> list)
        {
            return await DAO.InsertList(list);
        }

        public static async Task<bool> DeleteAll()
        {
            return await DAO.Delete(new Console());
        }

        public async Task<bool> Delete()
        {
            return await DAO.Delete(this);
        }

        public override string ToString()
        {
            return ID + " - " + Name;
        }
    }
}