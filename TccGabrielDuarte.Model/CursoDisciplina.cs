namespace TccGabrielDuarte.Model
{
    public class CursoDisciplina
    {
        public int CursoId { get; set; }
        public int DisciplinaId { get; set; }
        public Curso Curso { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
