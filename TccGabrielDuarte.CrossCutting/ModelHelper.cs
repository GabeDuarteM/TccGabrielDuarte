using System;
using System.Collections.Generic;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.CrossCutting
{
    public class ModelHelper
    {
        public static List<Curso> PopularListaCursos(System.Data.IDataReader dr)
        {
            var lstCurso = new List<Curso>();

            while (dr.Read())
            {
                var curso = new Curso
                {
                    Id = (int)dr[nameof(Curso.Id)],
                    Nome = (string)dr[nameof(Curso.Nome)],
                    Sigla = (string)dr[nameof(Curso.Sigla)],
                    AlunoCursos = new List<AlunoCurso>(),
                    CursoDisciplinas = new List<CursoDisciplina>()
                };

                lstCurso.Add(curso);
            }

            return lstCurso;
        }

        public static List<Turma> PopularListaTurmas(System.Data.IDataReader dr, List<Curso> lstCurso)
        {
            var lstTurma = new List<Turma>();

            while (dr.Read())
            {
                var turma = new Turma
                {
                    Id = (int)dr[nameof(Turma.Id)],
                    CursoId = (int)dr[nameof(Turma.CursoId)],
                    Professor = (string)dr[nameof(Turma.Professor)],
                    Curso = lstCurso.Find(x => x.Id == (int)dr[nameof(Turma.CursoId)])
                };

                lstTurma.Add(turma);
            }

            return lstTurma;
        }

        public static List<Disciplina> PopularListaDisciplinas(System.Data.IDataReader dr, List<Disciplina> lstDisciplina, List<Turma> lstTurma)
        {
            var lstDisciplinaRetorno = new List<Disciplina>();
            var i = 0;
            while (dr.Read())
            {
                foreach (var cursoDisc in lstDisciplina[i].CursoDisciplinas)
                {
                    cursoDisc.DisciplinaId = (int)dr[nameof(Disciplina.Id)];
                }

                var disciplina = new Disciplina
                {
                    Id = (int)dr[nameof(Disciplina.Id)],
                    CodDisciplina = (string)dr[nameof(Disciplina.CodDisciplina)],
                    Creditos = (int)dr[nameof(Disciplina.Creditos)],
                    Nome = (string)dr[nameof(Disciplina.Nome)],
                    TurmaId = (int)dr[nameof(Disciplina.TurmaId)],
                    Turma = lstTurma.Find(x => x.Id == (int)dr[nameof(Disciplina.TurmaId)]),
                    CursoDisciplinas = lstDisciplina[i].CursoDisciplinas
                };

                lstDisciplinaRetorno.Add(disciplina);

                i++;
            }

            return lstDisciplinaRetorno;
        }

        public static List<Aluno> PopularListaAlunos(System.Data.IDataReader dr, List<Aluno> lstAlunos)
        {
            var lstAlunoRetorno = new List<Aluno>();
            var i = 0;
            while (dr.Read())
            {
                foreach (var alunoCurso in lstAlunos[i].AlunoCursos)
                {
                    alunoCurso.AlunoId = (int)dr[nameof(Aluno.Id)];
                }

                var aluno = new Aluno
                {
                    Id = (int)dr[nameof(Aluno.Id)],
                    Nome = (string)dr[nameof(Aluno.Nome)],
                    Semestre = (int)dr[nameof(Aluno.Semestre)],
                    AlunoCursos = lstAlunos[i].AlunoCursos
                };

                lstAlunoRetorno.Add(aluno);

                i++;
            }

            return lstAlunoRetorno;
        }
    }
}
