using System.Collections.Generic;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.CrossCutting
{
    public interface ITipoConexao
    {
        Enums.BANCOS Banco { get; set; }

        void Seed(int qtAlunos);

        void LimparBase();

        ICollection<Aluno> GetListaAlunos();

        int GetListaCursos();

        int GetListaDisciplinas();

        int GetListaTurmas();
        int GetAlunoById(int id);
    }
}
