using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using GNX;

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

        public async static Task<bool> IncluirLista(IList<Console> list)
        {
            return await ConsoleDao.IncluirLista(list);
        }

        public async static Task<bool> Excluir()
        {
            return await ConsoleDao.Excluir(new Console() { });
        }

        public async static Task<List<Console>> Listar(int consoleID)
        {
            return await ConsoleDao.Listar(new Console() { ID = consoleID });
        }

        public async static Task<List<Console>> Listar(Console obj = null)
        {
            return await ConsoleDao.Listar(obj);
        }

        public async static Task<ListBind<Console>> ListarBind()
        {
            return new ListBind<Console>(await ConsoleDao.Listar());
        }
    }
}
