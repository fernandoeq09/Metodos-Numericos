using Matematica;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramacaoNaoLinear
{
    public abstract class FuncaoObjetivoGradienteDescente:IFuncaoObjetivo
    {
        public abstract double GetDerivada(int indice,Vetor x);

        public Vetor GetGradiente(Vetor x) {
            Vetor gradiente = new Vetor(x.Comprimento);
            for (int i = 0; i < x.Comprimento; i++)
            {
                gradiente.Item[i] = this.GetDerivada(i, x);
            }
            return gradiente;
        }

        public abstract double GetValor(Vetor x);
        
    }
}
