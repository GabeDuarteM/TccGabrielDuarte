using System.Collections.Generic;
using TccGabrielDuarte.Model;

namespace TccGabrielDuarte.CrossCutting
{
    public interface ITipoConexao
    {
        Enums.BANCOS Banco { get; set; }

        int GetListaAlunos();

        void Seed(int qtAlunos);
        void LimparBase();
        int GetListaCursos();
        int GetListaDisciplinas();
        int GetListaHistoricos();
        int GetListaTurmas();
    }
}
