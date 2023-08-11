using System.Collections.Generic;
using System.Threading.Tasks;

namespace RADB
{
    public class Console
    {
        public int ID { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public int NumGames { get; set; }
        public int TotalGames { get; set; }

        public Console()
        {
            Company = Name = string.Empty;
        }

        public async static Task<bool> SaveList(IList<Console> list)
        {
            return await ConsoleDao.InsertList(list);
        }

        public async static Task<bool> DeleteAll()
        {
            return await ConsoleDao.Delete(new Console());
        }

        public async Task<bool> Delete()
        {
            return await ConsoleDao.Delete(this);
        }

        public async static Task<List<Console>> List()
        {
            return await ConsoleDao.List();
        }
    }
}