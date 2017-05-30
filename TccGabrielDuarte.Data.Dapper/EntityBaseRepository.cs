using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Dapper
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase
    {
        public readonly TccContextDapper _context;

        public EntityBaseRepository(TccContextDapper context)
        {
            _context = context;
        }
        public ICollection<T> GetAll()
        {
            using (var context = _context.Conn)
            {
                var sql = "SELECT * FROM " + typeof(T).Name;
                context.Open();
                return context.Query<T>(sql).AsList();
            }
        }

        public T GetById(int id)
        {
            using (var context = _context.Conn)
            {
                var sql = $"SELECT * FROM {typeof(T).Name} WHERE {nameof(Aluno.Id)} = @{nameof(Aluno.Id)}";

                context.Open();
                return context.Query<T>(sql, new { Id = id }).AsList().First();
            }
        }
    }
}
