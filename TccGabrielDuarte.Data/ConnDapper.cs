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

        public int GetListaHistoricos()
        {
            var repo = new HistoricoEscolarRepository(Conn);
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

                conn.Execute("DELETE FROM Aluno");
                conn.Execute("DELETE FROM HistoricoEscolar");
                conn.Execute("DELETE FROM CursoDisciplina");
                conn.Execute("DELETE FROM Disciplina");
                conn.Execute("DELETE FROM Turma");
            }
        }

        public void Seed(int qtAlunos)
        {
            Conn.Seed(qtAlunos);
        }
    }
}
