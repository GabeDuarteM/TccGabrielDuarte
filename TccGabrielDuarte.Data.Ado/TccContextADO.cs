using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                        {

                            break;
                        }
                    case Enums.BANCOS.SQLServer:
                        {
                            var cursos = DataGenerator.Cursos();
                            var cursoInsert = "INSERT INTO Curso (Nome, Sigla) OUTPUT INSERTED.Id VALUES (@Nome, @Sigla)";

                            foreach (var curso in cursos)
                            {
                                using (var cmd = new SqlCommand(cursoInsert, (SqlConnection)conn) { CommandType = CommandType.Text })
                                {
                                    cmd.Parameters.AddWithValue("@Nome", curso.Nome);
                                    cmd.Parameters.AddWithValue("@Sigla", curso.Sigla);

                                    curso.Id = (int)cmd.ExecuteScalar();
                                }
                            }

                            var disciplinas = DataGenerator.Disciplinas(cursos);
                            var discInsert = $@"INSERT INTO {nameof(Disciplina)} ({nameof(Disciplina.Nome)}, {nameof(Disciplina.CodDisciplina)}, {nameof(Disciplina.Creditos)}) OUTPUT INSERTED.Id 
                                                    VALUES (@{nameof(Disciplina.Nome)}, @{nameof(Disciplina.CodDisciplina)}, @{nameof(Disciplina.Creditos)})";

                            //var cursoDiscInsert = $@"INSERT INTO {nameof(CursoDisciplina)} ({nameof(CursoDisciplina.CursoId)}, {nameof(CursoDisciplina.DisciplinaId)})
                            //                        VALUES (@{nameof(CursoDisciplina.CursoId)}, @{nameof(CursoDisciplina.DisciplinaId)})";

                            foreach (var disciplina in disciplinas)
                            {
                                using (var cmd = new SqlCommand(discInsert, (SqlConnection)conn) { CommandType = CommandType.Text })
                                {
                                    cmd.Parameters.AddWithValue($"@{nameof(disciplina.Nome)}", disciplina.Nome);
                                    cmd.Parameters.AddWithValue($"@{nameof(disciplina.CodDisciplina)}", disciplina.CodDisciplina);
                                    cmd.Parameters.AddWithValue($"@{nameof(disciplina.Creditos)}", disciplina.Creditos);

                                    disciplina.Id = (int)cmd.ExecuteScalar();

                                    //cmd.CommandText = cursoDiscInsert;

                                    //foreach (var cursoDisc in disciplina.CursoDisciplinas)
                                    //{
                                    //    cmd.Parameters.AddWithValue($"@{nameof(cursoDisc.DisciplinaId)}", cursoDisc.DisciplinaId);
                                    //    cmd.Parameters.AddWithValue($"@{nameof(cursoDisc.CursoId)}", cursoDisc.CursoId);

                                    //    cmd.ExecuteNonQuery();
                                    //}
                                }
                            }

                            var turmas = DataGenerator.Turmas(cursos, disciplinas);
                            var turmaInsert = $@"INSERT INTO {nameof(Turma)} ({nameof(Turma.Professor)}, {nameof(Turma.Semestre)})
                                                    VALUES (@{nameof(Turma.Professor)}, @{nameof(Turma.Semestre)})";

                            foreach (var turma in turmas)
                            {
                                using (var cmd = new SqlCommand(turmaInsert, (SqlConnection)conn) { CommandType = CommandType.Text })
                                {
                                    cmd.Parameters.AddWithValue($"@{nameof(Turma.Ano)}", turma.Ano);
                                    cmd.Parameters.AddWithValue($"@{nameof(Turma.Professor)}", turma.Professor);
                                    cmd.Parameters.AddWithValue($"@{nameof(Turma.Semestre)}", turma.Semestre);

                                    turma.Id = (int)cmd.ExecuteScalar();

                                    cmd.CommandText = 
                                }
                            }

                            var alunos = DataGenerator.Alunos(qtAlunos, cursos);
                            var historicos = DataGenerator.HistoricosEscolares(cursos, alunos, turmas);



                            

                            Conn.Insert(turmas);

                            Conn.Insert(alunos);

                            Conn.Insert(historicos);

                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
