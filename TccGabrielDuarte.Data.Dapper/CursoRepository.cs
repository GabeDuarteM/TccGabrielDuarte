using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Dapper
{
    public class CursoRepository : EntityBaseRepository<Curso>, ICursoRepository
    {
        public CursoRepository(TccContextDapper context) : base(context) { }
    }
}
