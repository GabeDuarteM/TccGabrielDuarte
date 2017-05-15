using System;
using Dapper;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Data.Dapper;

namespace TccGabrielDuarte.Data
{
    public class ConnDapper : ITipoConexao
    {
        public Enums.BANCOS Banco { get; set; }
        public TccContextDapper Conn { get => new TccContextDapper(Banco); }

        public ConnDapper(Enums.BANCOS banco)
        {
            Banco = banco;
        }

        public int GetListaAlunos()
        {
            var repo = new AlunoRepository(Conn);
            return repo.GetAll().Count;
        }

        public int GetListaCursos()
        {
            var repo = new CursoRepository(Conn);
            return repo.GetAll().Count;
        }

        public int GetListaDisciplinas()
        {
            var repo = new DisciplinaRepository(Conn);
            return repo.GetAll().Count;
        }

        public int GetListaTurmas()
        {
            var repo = new TurmaRepository(Conn);
            return repo.GetAll().Count;
        }

        public void LimparBase()
        {
            using (var conn = Conn.Conn)
            {
                conn.Open();

                conn.Execute("DELETE FROM AlunoCurso");
                conn.Execute("DELETE FROM Aluno");
                conn.Execute("DELETE FROM CursoDisciplina");
                conn.Execute("DELETE FROM Disciplina");
                conn.Execute("DELETE FROM Turma");
                conn.Execute("DELETE FROM Curso");

                if (Banco == Enums.BANCOS.SQLite)
                {
                    conn.Execute("VACUUM");
                }
                else if (Banco == Enums.BANCOS.SQLServer)
                {
                    conn.Execute("DBCC CHECKIDENT ('Aluno',RESEED, 0)");
                    conn.Execute("DBCC CHECKIDENT ('Disciplina',RESEED, 0)");
                    conn.Execute("DBCC CHECKIDENT ('Turma',RESEED, 0)");
                    conn.Execute("DBCC CHECKIDENT ('Curso',RESEED, 0)");

                }
            }
        }

        public void Seed(int qtAlunos)
        {
            Conn.Seed(qtAlunos);
        }
    }
}
