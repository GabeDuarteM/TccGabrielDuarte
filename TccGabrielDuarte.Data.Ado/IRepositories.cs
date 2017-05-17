using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Ado
{
    public interface IAlunoRepository : IEntityBaseRepository<Aluno> { }
    public interface ITurmaRepository : IEntityBaseRepository<Turma> { }
    public interface IDisciplinaRepository : IEntityBaseRepository<Disciplina> { }
    public interface ICursoRepository : IEntityBaseRepository<Curso> { }
}
