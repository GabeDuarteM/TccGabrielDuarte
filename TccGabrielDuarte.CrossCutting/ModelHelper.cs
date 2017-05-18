using System.Collections.Generic;
using System.Data;
using System.Linq;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.CrossCutting
{
    public class ModelHelper
    {
        public static List<Curso> PopularListaCursos(IDataReader dr)
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

        public static List<Turma> PopularListaTurmasCompleta(IDataReader dr, List<Curso> lstCurso)
        {
            var lstTurma = PopularListaTurmas(dr);

            foreach (var turma in lstTurma)
            {
                turma.Curso = lstCurso.Find(x => x.Id == turma.CursoId);
            }

            return lstTurma;
        }

        public static List<Turma> PopularListaTurmas(IDataReader dr)
        {
            var lstTurma = new List<Turma>();

            while (dr.Read())
            {
                var turma = new Turma
                {
                    Id = int.Parse(dr[nameof(Turma.Id)].ToString()),
                    CursoId = int.Parse(dr[nameof(Turma.CursoId)].ToString()),
                    Professor = (string)dr[nameof(Turma.Professor)]
                };

                lstTurma.Add(turma);
            }

            return lstTurma;
        }

        public static List<Disciplina> PopularListaDisciplinasCompleta(IDataReader dr, List<Disciplina> lstDisciplina)
        {
            var lstDisciplinaRetorno = PopularListaDisciplinas(dr);

            for (int i = 0; i < lstDisciplinaRetorno.Count; i++)
            {
                lstDisciplinaRetorno[i].Turma = lstDisciplina[i].Turma;
                lstDisciplinaRetorno[i].CursoDisciplinas = lstDisciplina[i].CursoDisciplinas;
                lstDisciplinaRetorno[i].CursoDisciplinas.First().DisciplinaId = lstDisciplinaRetorno[i].Id;
            }

            return lstDisciplinaRetorno;
        }

        public static List<Disciplina> PopularListaDisciplinas(IDataReader dr)
        {
            var lstDisciplinaRetorno = new List<Disciplina>();

            while (dr.Read())
            {
                var disciplina = new Disciplina
                {
                    Id = int.Parse(dr[nameof(Disciplina.Id)].ToString()),
                    CodDisciplina = (string)dr[nameof(Disciplina.CodDisciplina)],
                    Creditos = int.Parse(dr[nameof(Disciplina.Creditos)].ToString()),
                    Nome = (string)dr[nameof(Disciplina.Nome)],
                    TurmaId = int.Parse(dr[nameof(Disciplina.TurmaId)].ToString()),
                    
                };

                lstDisciplinaRetorno.Add(disciplina);
            }

            return lstDisciplinaRetorno;
        }

        public static List<Aluno> PopularListaAlunos(IDataReader dr)
        {
            var lstAlunoRetorno = new List<Aluno>();

            while (dr.Read())
            {
                var aluno = new Aluno
                {
                    Id = int.Parse(dr[nameof(Aluno.Id)].ToString()),
                    Nome = (string)dr[nameof(Aluno.Nome)],
                    Semestre = int.Parse(dr[nameof(Aluno.Semestre)].ToString())
                };                

                lstAlunoRetorno.Add(aluno);
            }

            return lstAlunoRetorno;
        }
    }
}
