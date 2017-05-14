using System;
using System.Collections.Generic;
using System.Text;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.EF
{
    public interface IAlunoRepository : IEntityBaseRepository<Aluno> { }
    public interface ITurmaRepository : IEntityBaseRepository<Turma> { }
    public interface IDisciplinaRepository : IEntityBaseRepository<Disciplina> { }
    public interface ICursoRepository : IEntityBaseRepository<Curso> { }
}
