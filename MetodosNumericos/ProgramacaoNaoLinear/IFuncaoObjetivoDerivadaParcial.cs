using Matematica;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramacaoNaoLinear
{
    interface IFuncaoObjetivoGradiente:IFuncaoObjetivo
    {
        /// <summary>
        /// Método retorna a derivada parcial (em relação à x[indice]) da função objetivo em um ponto.
        /// </summary>
        /// <param name="indice">índice da variável da derivada parcial</param>
        /// <param name="x">Ponto de aplicação da derivada</param>
        /// <returns></returns>
        double Derivada(int indice,Vetor x);
    }
}
