using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Ado
{
    public class DisciplinaRepository : EntityBaseRepository<Disciplina>, IDisciplinaRepository
    {
        public DisciplinaRepository(TccContextADO context) : base(context) { }
    }
}
