using System;
using System.Collections.Generic;
using System.Text;
using TccGabrielDuarte.CrossCutting;

namespace TccGabrielDuarte.Data
{
    public class ConnDapper : ITipoConexao
    {
        public Enums.BANCOS Banco { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int GetListaAlunos()
        {
            throw new NotImplementedException();
        }

        public int GetListaCursos()
        {
            throw new NotImplementedException();
        }

        public int GetListaDisciplinas()
        {
            throw new NotImplementedException();
        }

        public int GetListaHistoricos()
        {
            throw new NotImplementedException();
        }

        public int GetListaTurmas()
        {
            throw new NotImplementedException();
        }

        public void LimparBase()
        {
            throw new NotImplementedException();
        }

        public void Seed(int qtAlunos)
        {
            throw new NotImplementedException();
        }
    }
}
