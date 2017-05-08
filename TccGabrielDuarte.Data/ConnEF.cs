using Microsoft.EntityFrameworkCore;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Data.EF;

namespace TccGabrielDuarte.Data
{
    public class ConnEF : ITipoConexao
    {
        public Enums.BANCOS Banco { get; set; }

        public TccContext Conn { get => new TccContext(Banco);  }

        public ConnEF(Enums.BANCOS banco)
        {
            Banco = banco;
        }

        public int GetListaAlunos()
        {
            using (var context = Conn)
            {
                AlunoRepository alunoRepo = new AlunoRepository(context);

                return alunoRepo.GetAll().Count;
            }
        }

        public void Seed(int qtAlunos)
        {
            using (var context = Conn)
            {
                context.Seed(qtAlunos);
            }
        }

        public void LimparBase()
        {
            using (var context = Conn)
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Aluno WHERE id > 500000");
                context.Database.ExecuteSqlCommand("DELETE FROM Aluno WHERE id <= 500000");
                context.Database.ExecuteSqlCommand("DELETE FROM HistoricoEscolar WHERE id > 500000");
                context.Database.ExecuteSqlCommand("DELETE FROM HistoricoEscolar WHERE id <= 500000");
                context.Database.ExecuteSqlCommand("DELETE FROM CursoDisciplina");
                context.Database.ExecuteSqlCommand("DELETE FROM Disciplina");
                context.Database.ExecuteSqlCommand("DELETE FROM Turma");
                context.Database.ExecuteSqlCommand("DELETE FROM Curso");

                if (Banco == Enums.BANCOS.SQLite)
                {
                    context.Database.ExecuteSqlCommand("VACUUM");
                }
                else if (Banco == Enums.BANCOS.SQLServer)
                {
                    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Aluno',RESEED, 0)");
                    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('HistoricoEscolar',RESEED, 0)");
                    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('CursoDisciplina',RESEED, 0)");
                    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Disciplina',RESEED, 0)");
                    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Turma',RESEED, 0)");
                    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Curso',RESEED, 0)");

                }
            }
        }

        public int GetListaCursos()
        {
            using (var context = Conn)
            {
                CursoRepository cursoRepo = new CursoRepository(context);
                return cursoRepo.GetAll().Count;
            }
        }

        public int GetListaDisciplinas()
        {
            using (var context = Conn)
            {
                var repo = new DisciplinaRepository(context);
                return repo.GetAll().Count;
            }
        }

        public int GetListaHistoricos()
        {
            using (var context = Conn)
            {
                var repo = new HistoricoEscolarRepository(context);
                return repo.GetAll().Count;
            }
        }

        public int GetListaTurmas()
        {
            using (var context = Conn)
            {
                var repo = new TurmaRepository(context);
                return repo.GetAll().Count;
            }
        }
    }
}
