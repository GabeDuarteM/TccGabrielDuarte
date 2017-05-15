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
                var sql = MontarSql(curso.GetType(), nameof(Curso.Nome), nameof(Curso.Sigla));

                Conn.Execute(sql, new { curso.Nome, curso.Sigla });
            }

            cursos = Conn.Query<Curso>("SELECT * FROM Curso").ToList();
            cursos.ForEach(x => { x.AlunoCursos = new List<AlunoCurso>(); x.CursoDisciplinas = new List<CursoDisciplina>(); });

            var turmas = DataGenerator.Turmas(cursos);

            foreach (var turma in turmas)
            {
                var sql = MontarSql(turma.GetType(), nameof(Turma.Professor), nameof(Turma.CursoId));

                Conn.Execute(sql, new { turma.Professor, turma.CursoId });
            }

            turmas = Conn.Query<Turma>("SELECT * FROM Turma").ToList();
            turmas.ForEach(x => x.Curso = cursos.Single(y => y.Id == x.CursoId));

            var disciplinas = DataGenerator.Disciplinas(cursos, turmas);

            foreach (var disciplina in disciplinas)
            {
                var sql = MontarSql(disciplina.GetType(), nameof(Disciplina.Nome), nameof(Disciplina.CodDisciplina), nameof(Disciplina.Creditos), nameof(Disciplina.TurmaId));

                Conn.Execute(sql, new { disciplina.Nome, disciplina.CodDisciplina, disciplina.Creditos, disciplina.TurmaId });
            }

            var lstCursoDisciplina = new List<CursoDisciplina>();

            foreach (var disciplina in disciplinas)
            {
                lstCursoDisciplina.AddRange(disciplina.CursoDisciplinas);
            }

            disciplinas = Conn.Query<Disciplina>("SELECT * FROM Disciplina").ToList();

            for (int i = 0; i < disciplinas.Count; i++)
            {
                var disciplina = disciplinas[i];
                var cursoDisc = lstCursoDisciplina[i];
                cursoDisc.DisciplinaId = disciplina.Id;
            }

            foreach (var cursoDisc in lstCursoDisciplina)
            {
                var sql = MontarSql(cursoDisc.GetType(), nameof(CursoDisciplina.CursoId), nameof(CursoDisciplina.DisciplinaId));

                Conn.Execute(sql, new { cursoDisc.CursoId, cursoDisc.DisciplinaId });
            }

            var alunos = DataGenerator.Alunos(qtAlunos, cursos);

            foreach (var aluno in alunos)
            {
                var sql = MontarSql(aluno.GetType(), nameof(Aluno.Nome), nameof(Aluno.Semestre));

                Conn.Execute(sql, new { aluno.Nome, aluno.Semestre });
            }

            var lstAlunoCurso = new List<AlunoCurso>();

            foreach (var aluno in alunos)
            {
                lstAlunoCurso.AddRange(aluno.AlunoCursos);
            }

            alunos = Conn.Query<Aluno>("SELECT * FROM Aluno").ToList();

            for (int i = 0; i < alunos.Count; i++)
            {
                var aluno = alunos[i];
                var alunoCurso = lstAlunoCurso[i];
                alunoCurso.AlunoId = aluno.Id;
            }

            foreach (var alunoCurso in lstAlunoCurso)
            {
                var sql = MontarSql(alunoCurso.GetType(), nameof(AlunoCurso.CursoId), nameof(AlunoCurso.AlunoId));

                Conn.Execute(sql, new { alunoCurso.CursoId, alunoCurso.AlunoId });
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
