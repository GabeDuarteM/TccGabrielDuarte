using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using TccGabrielDuarte.CrossCutting;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.Data.Dapper
{
    public class TccContextDapper
    {
        private readonly string connString;

        public IDbConnection Conn { get => CreateConn(); }
        public Enums.BANCOS Banco { get; set; }

        public TccContextDapper(Enums.BANCOS banco)
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
            var cursos = DataGenerator.Cursos();

            foreach (var curso in cursos)
            {
                var sql = "";

                switch (Banco)
                {
                    case Enums.BANCOS.SQLite:
                        {
                            sql = $"INSERT INTO {nameof(Curso)} ({nameof(Curso.Nome)}, {nameof(Curso.Sigla)}) VALUES (@{nameof(Curso.Nome)}, @{nameof(Curso.Sigla)}); SELECT last_insert_rowid();";
                        }
                        break;
                    case Enums.BANCOS.SQLServer:
                        {
                            sql = $"INSERT INTO {nameof(Curso)} ({nameof(Curso.Nome)}, {nameof(Curso.Sigla)}) OUTPUT INSERTED.Id VALUES (@{nameof(Curso.Nome)}, @{nameof(Curso.Sigla)})";
                        }
                        break;
                }

                curso.Id = Conn.Query<int>(sql, new { curso.Nome, curso.Sigla }).Single();
            }

            var turmas = DataGenerator.Turmas(cursos);

            foreach (var turma in turmas)
            {
                var sql = "";
                switch (Banco)
                {
                    case Enums.BANCOS.SQLite:
                        {
                            sql = $"INSERT INTO {nameof(Turma)} ({nameof(Turma.Professor)}) VALUES (@{nameof(Turma.Professor)}); SELECT last_insert_rowid();";
                        }
                        break;
                    case Enums.BANCOS.SQLServer:
                        {
                            sql = $"INSERT INTO {nameof(Turma)} OUTPUT INSERTED.Id VALUES (@{nameof(Turma.Professor)});";
                        }
                        break;
                }


                turma.Id = Conn.Query<int>(sql, new { turma.Professor }).Single();
            }

            var disciplinas = DataGenerator.Disciplinas(cursos, turmas);

            foreach (var disciplina in disciplinas)
            {
                var sql = "";
                switch (Banco)
                {
                    case Enums.BANCOS.SQLite:
                        {
                            sql = $"INSERT INTO {nameof(Disciplina)} ({nameof(Disciplina.Nome)}, {nameof(Disciplina.CodDisciplina)}, {nameof(Disciplina.Creditos)}, {nameof(Disciplina.)}) VALUES (@{nameof(Disciplina.Professor)}); SELECT last_insert_rowid();";
                        }
                        break;
                    case Enums.BANCOS.SQLServer:
                        break;
                }
            }

            Conn.Insert(disciplinas);

            var alunos = DataGenerator.Alunos(qtAlunos, cursos);
            Conn.Insert(alunos);
        }
    }
}
