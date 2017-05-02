using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.EF
{
    public class AlunoRepository : EntityBaseRepository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(TccContext context) : base(context) { }
    }
}
