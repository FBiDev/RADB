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
        public string Name { get; set; }

        public async static Task<bool> IncluirLista(IList<Console> list)
        {
            return await ConsoleDao.IncluirLista(list);
        }

        public async static Task<bool> Excluir()
        {
            return await ConsoleDao.Excluir(new Console() { });
        }

        public async static Task<List<Console>> Listar()
        {
            return await ConsoleDao.Listar();
        }

        public async static Task<ListBind<Console>> ListarBind()
        {
            return new ListBind<Console>(await ConsoleDao.Listar());
        }
    }
}
