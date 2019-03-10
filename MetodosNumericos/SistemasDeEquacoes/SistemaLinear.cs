using System;
using Matematica;

namespace SistemasDeEquacoes
{
    public class SistemaLinear
    {
        private readonly double[] _vetorResposta;
        private readonly double[,] _matrizCoeficientes;

        public SistemaLinear(double[,] matrizCoeficientes,double[] vetorResposta)
        {
            if (Matriz.IsMatrizQuadrada(matrizCoeficientes) == false)
            {
                throw new DimensaoMatrizException("A matriz de coeficientes deve ser quadrada");
            }
            if (vetorResposta.GetUpperBound(0)!=matrizCoeficientes.GetUpperBound(0))
            {
                throw new ArgumentException("A dimensão do vetor de resposta deve ser a mesma da matriz");
            }
            _vetorResposta = vetorResposta;
            _matrizCoeficientes = matrizCoeficientes;
            Tolerancia = 1e-8;
        }

        /// <summary>
        /// Tolerância do método numérico
        /// </summary>
        public double Tolerancia { get; set; }

        //Arredonda valor baseado no número de casas decimais descritos na tolerância do método
        private double AjustarParaTolerancia(double r)
        {
            return Math.Round(r, Math.Abs((int)Math.Log10(this.Tolerancia)));
        }

        /// <summary>
        /// Obtem a solução trivial do sistema de equações
        /// </summary>
        /// <returns></returns>
        public double[] GetSolucao()
        {
            var matrizResposta = Matriz.MatrizColuna(_vetorResposta);
            var matrizSolucao = Matriz.Produto( Matriz.Inversa(_matrizCoeficientes), matrizResposta);
            double[] vetorSolucao = new double[matrizSolucao.GetUpperBound(0) + 1];
            for (int j = 0; j <= vetorSolucao.GetUpperBound(0); j++)
            {
                vetorSolucao[j] = AjustarParaTolerancia(matrizSolucao[j, 0]);
            }
            return vetorSolucao;
        }

    }
}
