using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Data.Sqlite;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Ado
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase
    {
        public readonly TccContextADO _context;

        public EntityBaseRepository(TccContextADO context)
        {
            _context = context;
        }

        public ICollection<T> GetAll()
        {
            using (var conn = _context.Conn)
            {
                IDbCommand cmd = null;
                switch (_context.Banco)
                {
                    case CrossCutting.Enums.BANCOS.SQLite:
                        cmd = new SqliteCommand();
                        break;
                    case CrossCutting.Enums.BANCOS.SQLServer:
                        cmd = new SqlCommand();
                        break;
                    default:
                        break;
                }
                using (cmd)
                {
                    cmd.CommandText = "SELECT * FROM " + typeof(T).Name;
                    cmd.Connection = conn;

                    conn.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        //return ModelHelper.PopularListaModel(typeof(T), dr);
                    }


                }
                var sql = "SELECT * FROM " + typeof(T).Name;
                conn.Open();
                //return conn.Query<T>(sql).AsList();
            }

            return null;
        }
    }
}
