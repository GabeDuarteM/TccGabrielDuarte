//using System;
//using TccGabrielDuarte.CrossCutting;
//using TccGabrielDuarte.Data.Ado;

//namespace TccGabrielDuarte.Data
//{
//    public class ConnAdo : ITipoConexao
//    {
//        public Enums.BANCOS Banco { get; set; }

//        public TccContextADO Conn { get => new TccContextADO(Banco); }

//        public int GetListaAlunos()
//        {
            
//        }

//        public int GetListaCursos()
//        {
//            throw new NotImplementedException();
//        }

//        public int GetListaDisciplinas()
//        {
//            throw new NotImplementedException();
//        }

//        public int GetListaHistoricos()
//        {
//            throw new NotImplementedException();
//        }

//        public int GetListaTurmas()
//        {
//            throw new NotImplementedException();
//        }

//        public void LimparBase()
//        {
//            throw new NotImplementedException();
//        }

//        public void Seed(int qtAlunos)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}