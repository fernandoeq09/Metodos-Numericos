using System;
using System.Collections.Generic;
using System.Text;

namespace EquacoesNaoLineares
{
    class NewtonRaphson
    {
        public NewtonRaphson(IFuncaoDeUmaVariavel funcao, double EstimativaInicial)
        {
            funcao.FuncaoValor(EstimativaInicial);
        }
    }
}
