using System;
using System.Collections.Generic;
using EquacoesPolinomiais;
namespace AuxiliarParaDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Polinomio p = new Polinomio(new List<double> {1,0,-3,-2});
            foreach (var r in p.GetRaizes())
            {
                Console.WriteLine("raiz: " +r);
                Console.WriteLine("funcao: " + p.GetValorPolinomio(r));
            }
            Console.WriteLine("processo finalizado");
            Console.ReadKey();
        }
    }
}
