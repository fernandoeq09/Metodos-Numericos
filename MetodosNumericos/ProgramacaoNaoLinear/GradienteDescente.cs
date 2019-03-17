using System;
using System.Collections.Generic;
using System.Text;
using Matematica;


namespace ProgramacaoNaoLinear
{
    public class GradienteDescente
    {
        private FuncaoObjetivoGradienteDescente funcaoObjetivo;
        public GradienteDescente(FuncaoObjetivoGradienteDescente funcaoObjetivo, double tolerancia=1e-8)
        {
            this.funcaoObjetivo = funcaoObjetivo;
            Tolerancia = tolerancia;
        }
        public double Tolerancia { get; set; }
        public ResultadoOtimizacao Otimizar(Vetor x0)
        {
            List<Interacao> interacoes = new List<Interacao>();
            Vetor x = new Vetor(x0.Item);
            
            Vetor dk = this.funcaoObjetivo.GetGradiente(x0);
            bool bit = true;
            int k = 0;
            double alfa;
            while (bit) { 
                k++;

                //determinação de dk=vetor oposto ao gradiente
                dk = (-1) * (this.funcaoObjetivo.GetGradiente(x));

                //determinação do passo alfa
                alfa = PassoArmijo(x, dk, funcaoObjetivo.GetGradiente(x));

                //calcula o novo valor do vetor x
                Vetor xAnterior = new Vetor(x.Item);
                x = x + alfa * dk;

                double erro = this.Erro(x, xAnterior);
                Interacao interacao = new Interacao(k, x, this.funcaoObjetivo.GetValor(x), erro);
                interacoes.Add(interacao);
                if (erro<= this.Tolerancia)
                {
                    bit = false;
                }
            }
            ResultadoOtimizacao resultadoOtimizado = new ResultadoOtimizacao("Gradiente Descente", interacoes, this.Tolerancia);
            return resultadoOtimizado;
        }

        private double Erro(Vetor xNovo, Vetor xAnterior)
        {
            Vetor erro = xNovo - xAnterior;
            for (int i = 0; i < erro.Comprimento; i++)
            {
                erro.Item[i] = Math.Abs(erro.Item[i]);
            }
            return erro.Maximo;
        }

        private double PassoArmijo(Vetor x, Vetor dk, Vetor vetorGradiente) {
            double beta = 0.1;
            double teta = 2.0;
            double alfa=0;
            double alfaPadrao = 1.0;
            double t=0;
            
            bool bit = true;
            while (bit) {
                alfa = alfaPadrao / Math.Pow(teta, t);

                Vetor aux = x + alfa * dk;

                double primeiroTermo = funcaoObjetivo.GetValor(aux);

                double segundoTermo = funcaoObjetivo.GetValor(x) + beta * alfa * (vetorGradiente*dk);

                if (primeiroTermo <= segundoTermo) {
                    bit = false;
                }
                else
                {
                    t = t + 1;
                }
            }
            return alfa;
        }

    }

}
