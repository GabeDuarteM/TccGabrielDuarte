using System;
using System.Collections.Generic;
using System.Linq;

namespace TccGabrielDuarte.Model
{
    public class DataGenerator
    {
        private static readonly Random rnd = new Random();
        private static readonly int disciplinasPorTurma = 5;

        public static List<Curso> Cursos()
        {
            return new List<Curso>
            {
                new Curso
                {
                    Nome = "Sistemas de Informação",
                    Sigla = "SI",
                    CursoDisciplinas = new List<CursoDisciplina>(),
                    AlunoCursos = new List<AlunoCurso>()
                },
                new Curso
                {
                    Nome = "Administração",
                    Sigla = "ADM",
                    CursoDisciplinas = new List<CursoDisciplina>(),
                    AlunoCursos = new List<AlunoCurso>()
                },
                new Curso
                {
                    Nome = "Direito",
                    Sigla = "DIR",
                    CursoDisciplinas = new List<CursoDisciplina>(),
                    AlunoCursos = new List<AlunoCurso>()
                },
                new Curso
                {
                    Nome = "Gastronomia",
                    Sigla = "GST",
                    CursoDisciplinas = new List<CursoDisciplina>(),
                    AlunoCursos = new List<AlunoCurso>()
                },
                new Curso
                {
                    Nome = "Psicologia",
                    Sigla = "PSI",
                    CursoDisciplinas = new List<CursoDisciplina>(),
                    AlunoCursos = new List<AlunoCurso>()
                },
            };
        }

        public static List<Disciplina> Disciplinas(List<Curso> cursos, List<Turma> turmas)
        {
            var disciplinas = new List<Disciplina>();
            var disciplinasPorTurma = 5;

            foreach (var turma in turmas)
            {
                for (int i = 0; i < disciplinasPorTurma; i++)
                {
                    var disciplina = new Disciplina
                    {
                        Nome = $"{turma.Curso.Nome}: {i + 1}",
                        CodDisciplina = $"{turma.Curso.Sigla}{ (i + 1).ToString().PadLeft(3, '0')}",
                        Creditos = rnd.Next(1, 8),
                        CursoDisciplinas = new List<CursoDisciplina>(),
                        Turma = turma,
                        TurmaId = turma.Id
                    };

                    var cursoDisc = new CursoDisciplina { Curso = turma.Curso, CursoId = turma.CursoId, Disciplina = disciplina, DisciplinaId = disciplina.Id };
                    disciplina.CursoDisciplinas.Add(cursoDisc);

                    var curso = cursos.Where(x => x == cursoDisc.Curso).Single();
                    curso.CursoDisciplinas.Add(cursoDisc);

                    disciplinas.Add(disciplina);
                }
            }

            return disciplinas;
        }

        public static List<Aluno> Alunos(int qtAlunos, List<Curso> cursos)
        {
            var alunos = new List<Aluno>();
            var alunosPorCurso = qtAlunos / cursos.Count;

            foreach (var curso in cursos)
            {
                for (int i = 0; i < alunosPorCurso; i++)
                {
                    var aluno = new Aluno
                    {
                        Nome = $"Aluno {i + 1} {curso.Sigla}",
                        Semestre = rnd.Next(1, 8),
                        AlunoCursos = new List<AlunoCurso>()
                    };

                    var alunoCurso = new AlunoCurso { Aluno = aluno, Curso = curso, CursoId = curso.Id };
                    aluno.AlunoCursos.Add(alunoCurso);
                    curso.AlunoCursos.Add(alunoCurso);

                    alunos.Add(aluno);
                }
            }

            return alunos;
        }

        public static List<Turma> Turmas(List<Curso> cursos)
        {
            var turmas = new List<Turma>();
            var turmasPorCurso = 10;
            foreach (var curso in cursos)
            {
                for (int i = 0; i < turmasPorCurso; i++)
                {
                    turmas.Add(new Turma { Professor = $"Professor {curso.Nome}", Curso = curso, CursoId = curso.Id });
                }
            }

            return turmas;
        }
    }
}
