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
    public class ConsoleDao
    {
        #region " _Carregar "
        private static T Carregar<T>(DataTable table) where T : IList, new()
        {
            T list = new T();
            foreach (DataRow row in table.Rows)
            {
                list.Add(new Console()
                {
                    ID = row.Value<int>("ID"),
                    Name = row.Value<string>("Name"),
                });
            }
            return list;
        }
        #endregion

        #region " _MontarFiltros "
        private static List<cSqlParameter> MontarFiltros(Console obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Name", obj.Name),
            };
        }
        #endregion

        #region " _MontarParametros "
        private static List<cSqlParameter> MontarParametros(Console obj)
        {
            return new List<cSqlParameter>
            {
                new cSqlParameter("@ID", obj.ID),
                new cSqlParameter("@Name", obj.Name),
            };
        }
        #endregion

        #region " _Listar "
        public static Task<List<Console>> Listar(Console obj = null)
        {
            return Task<List<Console>>.Run(() =>
            {
                obj = obj ?? new Console();

                //Monta SQL
                string sql = Resources.ConsoleListar;
                sql += " ORDER BY Name ASC ";

                return Carregar<List<Console>>(Banco.ExecutarSelect(sql, MontarFiltros(obj)));
            });
        }
        #endregion

        #region " _Incluir "
        public static bool Incluir(Console obj)
        {
            //Monta SQL
            string sql = Resources.ConsoleIncluir;

            var parametros = MontarParametros(obj);

            return Banco.Executar(sql, MovimentoLog.Inclusão, parametros).AffectedRows > 0;
        }

        public static Task<bool> IncluirLista(IList<Console> list)
        {
            return Task<bool>.Run(() =>
            {
                //Monta SQL
                string sql = "INSERT INTO Console (ID, Name) VALUES " + Environment.NewLine; ;

                var parametros = new List<cSqlParameter>();

                int index = 0;
                foreach (var i in list)
                {
                    parametros.AddRange(new List<cSqlParameter>
                    {
                        new cSqlParameter("@ID" + index, i.ID),
                        new cSqlParameter("@Name" + index, i.Name),
                    });

                    sql += "(" + "@ID" + index + ", @Name" + index + ")";

                    index++;
                    if (index < list.Count) { sql += "," + Environment.NewLine; }
                }

                return Banco.Executar(sql, MovimentoLog.Inclusão, parametros).AffectedRows > 0;
            });
        }
        #endregion

        #region " _Excluir "
        public static Task<bool> Excluir(Console obj)
        {
            return Task<bool>.Run(() =>
            {
                string sql = Resources.ConsoleExcluir;

                var parametros = new List<cSqlParameter> 
                {
                    new cSqlParameter("@ID", obj.ID),
                    new cSqlParameter("@Name", obj.Name),
                };

                return Banco.Executar(sql, MovimentoLog.Exclusão, parametros).AffectedRows > 0;
            });
        }
        #endregion
    }
}
