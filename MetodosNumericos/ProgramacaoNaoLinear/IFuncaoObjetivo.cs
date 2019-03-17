using Matematica;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramacaoNaoLinear
{
    interface IFuncaoObjetivo
    {
        double GetValor(Vetor x);
    }
}
