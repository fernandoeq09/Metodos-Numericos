using Matematica;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgramacaoNaoLinear
{
    public class ResultadoOtimizacao
    {
        /// <summary>
        /// Resumo dos resultados da otimização
        /// </summary>
        /// <param name="metodo">Nome do método de otimização</param>
        /// <param name="interacoes">Lista com as interações realizadas no método</param>
        /// <param name="tolerancia">Valor da tolerância do método</param>
        /// <param name="toleranciaAtingida">Indica se a função objetivo foi otimizada</param>
        public ResultadoOtimizacao(string metodo, List<Interacao> interacoes, double tolerancia)
        {
            this.Metodo = metodo;
            this.Interacoes = interacoes;
            this.Tolerancia = tolerancia;
            Interacao inter = (from f in interacoes
                               select f).Last();

            Vetor x = new Vetor(inter.X.Item);
            for (int i = 0; i < x.Comprimento; i++)
            {
                x.Item[i] = AjustarValorComTolerancia(x.Item[i]);
            }
            this.SolucaoOtima = x;
        }
        private double AjustarValorComTolerancia(double valor)
        {
            return Math.Round(valor, Convert.ToInt32(Math.Abs(Math.Log10(Tolerancia))));
        }

        public string Metodo { get;}

        public double Tolerancia { get; }

        public Vetor SolucaoOtima { get; }

        public List<Interacao> Interacoes { get; }

    }
    
    public class Interacao
    {
        /// <summary>
        /// Interacao do processo de otimização
        /// </summary>
        /// <param name="numero">número da interação</param>
        /// <param name="x">valor das variáveis independentes</param>
        /// <param name="funcaoObjetivo">valor da função objetivo</param>
        /// <param name="erro">Erro</param>
        public Interacao(int numero,Vetor x,double funcaoObjetivo, double erro)
        {
            this.Numero = numero;
            this.X = x;
            this.FuncaoObjetivo = funcaoObjetivo;
            this.Erro = erro;
        }
        public int Numero { get; }
        public Vetor X { get; }
        public double FuncaoObjetivo { get; }
        public double Erro { get; }
    }
}
