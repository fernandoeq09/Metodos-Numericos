using System;
using System.Collections.Generic;
using System.Text;
using Matematica;
using ProgramacaoNaoLinear;
namespace AuxiliarParaDebug
{
    class FuncaoObjetivo : FuncaoObjetivoGradienteDescente
    {
        public override double GetDerivada(int indice, Vetor x)
        {
            return 2*(Math.Pow(x.Item[0], 2) - 5 * x.Item[0] + 6)*(2*x.Item[0]-5);
        }

        public override double GetValor(Vetor x)
        {
            return Math.Pow(Math.Pow(x.Item[0],2)-5*x.Item[0]+6,2);
        }
    }
}
