using Microsoft.EntityFrameworkCore;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Data.EF;

namespace TccGabrielDuarte.Data
{
    public class ConnEF : ITipoConexao
    {
        public Enums.BANCOS Banco { get; set; }

        public ConnEF(Enums.BANCOS banco)
        {
            Banco = banco;
        }

        public TccContext CreateContext()
        {
            return new TccContext(Banco);
        }

        public int GetListaAlunos()
        {
            using (var context = CreateContext())
            {
                AlunoRepository alunoRepo = new AlunoRepository(context);

                return alunoRepo.GetAll().Count;
            }
        }

        public void Seed(int qtAlunos)
        {
            using (var context = CreateContext())
            {
                context.Seed(qtAlunos);
            }
        }

        public void LimparBase()
        {
            using (var context = CreateContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Aluno");
                context.Database.ExecuteSqlCommand("DELETE FROM HistoricoEscolar");
                context.Database.ExecuteSqlCommand("DELETE FROM CursoDisciplina");
                context.Database.ExecuteSqlCommand("DELETE FROM Disciplina");
                context.Database.ExecuteSqlCommand("DELETE FROM Turma");
            }
        }

        public int GetListaCursos()
        {
            using (var context = CreateContext())
            {
                CursoRepository cursoRepo = new CursoRepository(context);
                return cursoRepo.GetAll().Count;
            }
        }

        public int GetListaDisciplinas()
        {
            using (var context = CreateContext())
            {
                var repo = new DisciplinaRepository(context);
                return repo.GetAll().Count;
            }
        }

        public int GetListaHistoricos()
        {
            using (var context = CreateContext())
            {
                var repo = new HistoricoEscolarRepository(context);
                return repo.GetAll().Count;
            }
        }

        public int GetListaTurmas()
        {
            using (var context = CreateContext())
            {
                var repo = new TurmaRepository(context);
                return repo.GetAll().Count;
            }
        }
    }
}
