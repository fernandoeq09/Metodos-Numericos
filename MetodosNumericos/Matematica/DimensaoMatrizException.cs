using System;

namespace Matematica
{
    public class DimensaoMatrizException:Exception
    {
        public DimensaoMatrizException()
        {

        }
        public DimensaoMatrizException(string mensagem):base(mensagem)
        {

        }
        public DimensaoMatrizException(string mensagem,Exception excecaoInterna) : base(mensagem,excecaoInterna)
        {

        }
    }
}
