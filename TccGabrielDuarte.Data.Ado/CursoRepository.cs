using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Ado
{
    public class CursoRepository : EntityBaseRepository<Curso>, ICursoRepository
    {
        public CursoRepository(TccContextADO context) : base(context) { }
    }
}
