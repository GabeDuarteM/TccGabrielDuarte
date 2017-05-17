using System;
using System.Collections.Generic;
using System.Data;
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
                    Id = int.Parse(dr[nameof(Curso.Id)].ToString()),
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
                    Id = int.Parse(dr[nameof(Turma.Id)].ToString()),
                    CursoId = int.Parse(dr[nameof(Turma.CursoId)].ToString()),
                    Professor = (string)dr[nameof(Turma.Professor)],
                    Curso = lstCurso.Find(x => x.Id == int.Parse(dr[nameof(Turma.CursoId)].ToString()))
                };

                lstTurma.Add(turma);
            }

            return lstTurma;
        }

        public static ICollection<IEntityBase> PopularListaModel(Type type, IDataReader dr)
        {
            if (type == typeof(Aluno))
            {
                
            }
            return null;
        }

        public static List<Disciplina> PopularListaDisciplinas(System.Data.IDataReader dr, List<Disciplina> lstDisciplina, List<Turma> lstTurma)
        {
            var lstDisciplinaRetorno = new List<Disciplina>();
            var i = 0;
            while (dr.Read())
            {
                foreach (var cursoDisc in lstDisciplina[i].CursoDisciplinas)
                {
                    cursoDisc.DisciplinaId = int.Parse(dr[nameof(Disciplina.Id)].ToString());
                }

                var disciplina = new Disciplina
                {
                    Id = int.Parse(dr[nameof(Disciplina.Id)].ToString()),
                    CodDisciplina = (string)dr[nameof(Disciplina.CodDisciplina)],
                    Creditos = int.Parse(dr[nameof(Disciplina.Creditos)].ToString()),
                    Nome = (string)dr[nameof(Disciplina.Nome)],
                    TurmaId = int.Parse(dr[nameof(Disciplina.TurmaId)].ToString()),
                    Turma = lstTurma.Find(x => x.Id == int.Parse(dr[nameof(Disciplina.TurmaId)].ToString())),
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
                    alunoCurso.AlunoId = int.Parse(dr[nameof(Aluno.Id)].ToString());
                }

                var aluno = new Aluno
                {
                    Id = int.Parse(dr[nameof(Aluno.Id)].ToString()),
                    Nome = (string)dr[nameof(Aluno.Nome)],
                    Semestre = int.Parse(dr[nameof(Aluno.Semestre)].ToString()),
                    AlunoCursos = lstAlunos[i].AlunoCursos
                };

                lstAlunoRetorno.Add(aluno);

                i++;
            }

            return lstAlunoRetorno;
        }
    }
}
