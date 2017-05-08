using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Dapper
{
    public class DisciplinaRepository : EntityBaseRepository<Disciplina>, IDisciplinaRepository
    {
        public DisciplinaRepository(TccContextDapper context) : base(context) { }
    }
}
