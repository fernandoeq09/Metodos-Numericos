using System;
using System.Collections.Generic;
using EquacoesPolinomiais;
using Matematica;
using ProgramacaoNaoLinear;
using SistemasDeEquacoes;

namespace AuxiliarParaDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            Vetor EstimativaInicial = new Vetor(new double[] { -100 });
            FuncaoObjetivo f = new FuncaoObjetivo();
            GradienteDescente gradienteDescente = new GradienteDescente(f);
            gradienteDescente.Tolerancia = 1e-8;
            ResultadoOtimizacao r = gradienteDescente.Otimizar(EstimativaInicial);
            Console.ReadKey();
        }
    }
}
