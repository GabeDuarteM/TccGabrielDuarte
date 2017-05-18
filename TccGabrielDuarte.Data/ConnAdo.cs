using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Data.Ado;

namespace TccGabrielDuarte.Data
{
    public class ConnAdo : ITipoConexao
    {
        public Enums.BANCOS Banco { get; set; }

        public TccContextADO Conn { get => new TccContextADO(Banco); }

        public ConnAdo(Enums.BANCOS banco)
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

                switch (Banco)
                {
                    case Enums.BANCOS.SQLite:
                        using (var cmd = new SqliteCommand())
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = (SqliteConnection)conn;

                            cmd.CommandText = "DELETE FROM AlunoCurso";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Aluno";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM CursoDisciplina";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Disciplina";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Turma";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Curso";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "VACUUM";
                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case Enums.BANCOS.SQLServer:
                        using (var cmd = new SqlCommand())
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = (SqlConnection)conn;

                            cmd.CommandText = "DELETE FROM AlunoCurso";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Aluno";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM CursoDisciplina";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Disciplina";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Turma";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DELETE FROM Curso";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DBCC CHECKIDENT ('Aluno',RESEED, 0)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DBCC CHECKIDENT ('Disciplina',RESEED, 0)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DBCC CHECKIDENT ('Turma',RESEED, 0)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "DBCC CHECKIDENT ('Curso',RESEED, 0)";
                            cmd.ExecuteNonQuery();
                        }
                        break;
                }
            }
        }

        public void Seed(int qtAlunos)
        {
            Conn.Seed(qtAlunos);
        }
    }
}