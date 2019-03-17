using System;
using System.Collections.Generic;
using System.Text;

namespace EquacoesNaoLineares
{
    interface IFuncaoDeUmaVariavel
    {
        double FuncaoValor(double x);
        double PrimeiraDerivadaValor(double x);
        double SegundaDerivadaValor(double x);
    }
}
