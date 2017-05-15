using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Data.Sqlite;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Ado
{
    public class TccContextADO
    {
        private readonly string connString;

        public IDbConnection Conn { get => CreateConn(); }
        public Enums.BANCOS Banco { get; set; }

        public TccContextADO(Enums.BANCOS banco)
        {
            Banco = banco;
            switch (banco)
            {
                case Enums.BANCOS.SQLite:
                    connString = Constantes.CONN_SQLITE;
                    break;
                case Enums.BANCOS.SQLServer:
                    connString = Constantes.CONN_SQLSERVER;
                    break;
            }
        }

        public IDbConnection CreateConn()
        {
            switch (Banco)
            {
                case Enums.BANCOS.SQLite:
                    return new SqliteConnection(connString);
                case Enums.BANCOS.SQLServer:
                    return new SqlConnection(connString);
                default:
                    throw new Exception("Banco não suportado");
            }
        }

        public void Seed(int qtAlunos)
        {
            using (var conn = Conn)
            {
                conn.Open();
                switch (Banco)
                {
                    case Enums.BANCOS.SQLite:
                        break;
                    case Enums.BANCOS.SQLServer:
                        {
                            var cursos = DataGenerator.Cursos();

                            foreach (var curso in cursos)
                            {
                                var sql = MontarSql(curso.GetType(), nameof(Curso.Nome), nameof(Curso.Sigla));

                                using (var cmd = new SqlCommand(sql, (SqlConnection)conn) { CommandType = CommandType.Text })
                                {
                                    cmd.Parameters.AddWithValue("@Nome", curso.Nome);
                                    cmd.Parameters.AddWithValue("@Sigla", curso.Sigla);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            using (var cmd = new SqlCommand("SELECT * FROM Curso", (SqlConnection)conn) { CommandType = CommandType.Text })
                            using (var dr = cmd.ExecuteReader())
                            {
                                cursos = ModelHelper.PopularListaCursos(dr);
                            }

                            var turmas = DataGenerator.Turmas(cursos);

                            foreach (var turma in turmas)
                            {
                                var sql = MontarSql(turma.GetType(), nameof(Turma.Professor), nameof(Turma.CursoId));

                                using (var cmd = new SqlCommand(sql, (SqlConnection)conn) { CommandType = CommandType.Text })
                                {
                                    cmd.Parameters.AddWithValue("@Professor", turma.Professor);
                                    cmd.Parameters.AddWithValue("@CursoId", turma.CursoId);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            using (var cmd = new SqlCommand("SELECT * FROM Turma", (SqlConnection)conn) { CommandType = CommandType.Text })
                            using (var dr = cmd.ExecuteReader())
                            {
                                turmas = ModelHelper.PopularListaTurmas(dr, cursos);
                            }

                            var disciplinas = DataGenerator.Disciplinas(cursos, turmas);

                            foreach (var disciplina in disciplinas)
                            {
                                var sql = MontarSql(disciplina.GetType(), nameof(Disciplina.Nome), nameof(Disciplina.CodDisciplina), nameof(Disciplina.Creditos), nameof(Disciplina.TurmaId));

                                using (var cmd = new SqlCommand(sql, (SqlConnection)conn) { CommandType = CommandType.Text })
                                {
                                    cmd.Parameters.AddWithValue("@Nome", disciplina.Nome);
                                    cmd.Parameters.AddWithValue("@CodDisciplina", disciplina.CodDisciplina);
                                    cmd.Parameters.AddWithValue("@Creditos", disciplina.Creditos);
                                    cmd.Parameters.AddWithValue("@TurmaId", disciplina.TurmaId);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            using (var cmd = new SqlCommand("SELECT * FROM Disciplina", (SqlConnection)conn))
                            using (var dr = cmd.ExecuteReader())
                            {
                                disciplinas = ModelHelper.PopularListaDisciplinas(dr, disciplinas, turmas);
                            }

                            var lstCursoDisciplina = disciplinas.SelectMany(x => x.CursoDisciplinas);

                            foreach (var cursoDisc in lstCursoDisciplina)
                            {
                                var sql = MontarSql(cursoDisc.GetType(), nameof(CursoDisciplina.CursoId), nameof(CursoDisciplina.DisciplinaId));

                                using (var cmd = new SqlCommand(sql, (SqlConnection)conn) { CommandType = CommandType.Text })
                                {
                                    cmd.Parameters.AddWithValue("@CursoId", cursoDisc.CursoId);
                                    cmd.Parameters.AddWithValue("@DisciplinaId", cursoDisc.DisciplinaId);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            var alunos = DataGenerator.Alunos(qtAlunos, cursos);

                            foreach (var aluno in alunos)
                            {
                                var sql = MontarSql(aluno.GetType(), nameof(Aluno.Nome), nameof(Aluno.Semestre));

                                using (var cmd = new SqlCommand(sql, (SqlConnection)conn) { CommandType = CommandType.Text })
                                {
                                    cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                                    cmd.Parameters.AddWithValue("@Semestre", aluno.Semestre);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            using (var cmd = new SqlCommand("SELECT * FROM Aluno", (SqlConnection)conn))
                            using (var dr = cmd.ExecuteReader())
                            {
                                alunos = ModelHelper.PopularListaAlunos(dr, alunos);
                            }
                        }
                        break;
                }
            }
        }

        private string MontarSql(Type type, params string[] sqlCampos)
        {
            var sqlInsert = $"INSERT INTO {type.Name} ({string.Join(", ", sqlCampos)})";
            var sqlValues = $"VALUES ({string.Join(", ", sqlCampos.Select(x => "@" + x))})";

            return $"{sqlInsert} {sqlValues}";
        }
    }
}
