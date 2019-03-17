using System;
using InterfacesMetodosNumericos;
namespace Calculo
{
    public static class DerivadaNumerica
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcao"></param>
        /// <param name="x"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static double PrimeiraDerivada(IFuncaoDeUmaVariavel funcao,double x,double h=1e-3)
        {
            double derivada = (-funcao.GetValor(x + 2 * h) + 8 * funcao.GetValor(x + h) - 8 * funcao.GetValor(x - h) + funcao.GetValor(x - 2 * h)) / (12 * h);
            return derivada;
        }
        public static double SegundaDerivada(IFuncaoDeUmaVariavel funcao, double x, double h = 1e-3)
        {
            double derivada = (-funcao.GetValor(x + 2 * h) + 16 * funcao.GetValor(x + h) -30*funcao.GetValor(x)+ 16 * funcao.GetValor(x - h) - funcao.GetValor(x - 2 * h)) / (12 * Math.Pow(h,2.0));
            return derivada;
        }


    }
}
