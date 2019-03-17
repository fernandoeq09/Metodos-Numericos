using System;
using System.Collections.Generic;
using System.Text;

namespace Matematica
{
    /// <summary>
    /// Vetor
    /// </summary>
    public class Vetor
    {
        /// <summary>
        /// Inicia um nova instância de um Vetor a partir de um array unidimensional
        /// </summary>
        /// <param name="vetor">Array unidimensional de double</param>
        public Vetor(double[] vetor)
        {
            Item = vetor;
        }

        /// <summary>
        /// Inicia um nova instância de um Vetor a partir do comprimento
        /// </summary>
        /// <param name="comprimento">Comprimento do Vetor</param>
        public Vetor(int comprimento)
        {
            Item = new double[comprimento];
        }

        /// <summary>
        /// Número de posições do vetor
        /// </summary>
        public int Comprimento {
            get
            {
                return Item.GetUpperBound(0) + 1;
            }
        }

        /// <summary>
        /// Array com os valores do vetor
        /// </summary>
        public double[] Item { get; set; }

        /// <summary>
        /// Retorna o máximo valor do vetor
        /// </summary>
        public double Maximo
        {
            get
            {
                //cria um clone dos itens
                double[] v = (double[]) Item.Clone();
                //ordena do menor para o maior
                Array.Sort(v);
                return v[v.GetUpperBound(0)];
            }
        }
        /// <summary>
        /// Retorna o mínimo valor do vetor
        /// </summary>
        public double Minimo
        {
            get
            {
                //cria um clone dos itens
                double[] v = (double[])Item.Clone();
                //ordena do menor para o maior
                Array.Sort(v);
                return v[v.GetLowerBound(0)];
            }
        }

        /// <summary>
        /// Retorna a soma de dois vetores
        /// </summary>
        /// <param name="vetorA">Primeiro Vetor</param>
        /// <param name="vetorB">Segundo Vetor</param>
        /// <returns></returns>
        public static Vetor operator + (Vetor vetorA, Vetor vetorB)
        {
            return new Vetor(Soma(vetorA.Item, vetorB.Item));
        }

        /// <summary>
        /// Retorna a subtração de dois vetores
        /// </summary>
        /// <param name="vetorA">Primeiro Vetor</param>
        /// <param name="vetorB">Segundo Vetor</param>
        /// <returns></returns>
        public static Vetor operator -(Vetor vetorA, Vetor vetorB)
        {
            return new Vetor(Soma(vetorA.Item, ((-1)*vetorB).Item));
        }

        /// <summary>
        /// Retorna o produto de uma constate por um vetor
        /// </summary>
        /// <param name="vetor">Vetor</param>
        /// <param name="c">Constante</param>
        /// <returns></returns>
        public static Vetor operator * (Vetor vetor, double c)
        {
            return new Vetor(ProdutoConstanteVetor(c, vetor.Item));
        }

        /// <summary>
        /// Retorna o produto de uma constate por um vetor
        /// </summary>
        /// <param name="vetor">Vetor</param>
        /// <param name="c">Constante</param>
        /// <returns></returns>
        public static Vetor operator *( double c, Vetor vetor)
        {
            return new Vetor(ProdutoConstanteVetor(c, vetor.Item));
        }

        /// <summary>
        /// Retorna o produto escalar entre dois vetores
        /// </summary>
        /// <param name="vetorA"></param>
        /// <param name="vetorB"></param>
        /// <returns></returns>
        public static double operator * (Vetor vetorA,Vetor vetorB)
        {
            return ProdutoEscalarAux(vetorA.Item, vetorB.Item);
        }

        /// <summary>
        /// Realiza o produto de uma constante por um Vetor
        /// </summary>
        /// <param name="c"></param>
        /// <param name="vetor"></param>
        /// <returns></returns>
        private static double[] ProdutoConstanteVetor(double c, double[] vetor)
        {
            double[] vetorResposta = new double[vetor.GetUpperBound(0) + 1];
            for (int i = 0; i <= vetor.GetUpperBound(0); i++)
            {
                    vetorResposta[i] = vetor[i] * c;
                
            }
            return vetorResposta;
        }

        private static double[] Soma(double[] vetorA, double[] vetorB)
        {
            double[] soma = new double[vetorA.GetUpperBound(0) + 1];
            for (int i = 0; i <= soma.GetUpperBound(0); i++)
            {
                soma[i] = vetorA[i] + vetorB[i];
            }
            return soma;
        }

        private static double ProdutoEscalarAux(double[] vetorA, double[] vetorB)
        {
            double produtoEscalar = 0;

            for (int i = 0; i <= vetorA.GetUpperBound(0); i++)
            {
                produtoEscalar += vetorA[i] * vetorB[i];
            }
            return produtoEscalar;
        }

    }
}
