using System;
using System.Collections.Generic;
using EquacoesPolinomiais;
using Matematica;
using SistemasDeEquacoes;

namespace AuxiliarParaDebug
{
    class Program
    {
        static void Main(string[] args)
        {


            double[,] coef = new double [,] { { 3, 0, 1}, { 1, 1, 1}, { 0, 2, -1 } };
            double[] r = new double[] { -5, -2, -3};
            SistemaLinear s = new SistemaLinear(coef, r);
            var sol = s.GetSolucao();
            Console.WriteLine("processo finalizado");
            Console.ReadKey();
        }
    }
}
