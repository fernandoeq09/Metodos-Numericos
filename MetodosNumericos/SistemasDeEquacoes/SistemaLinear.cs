using System;
using Matematica;

namespace SistemasDeEquacoes
{
    public class SistemaLinear
    {
        private readonly Vetor _vetorResposta;
        private readonly Matriz _matrizCoeficientes;

        public SistemaLinear(Matriz matrizCoeficientes,Vetor vetorResposta)
        {
            if (matrizCoeficientes.IsQuadrada() == false)
            {
                throw new DimensaoMatrizException("A matriz de coeficientes deve ser quadrada");
            }
            if (vetorResposta.Comprimento!=matrizCoeficientes.NumeroDeLinhas)
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
        public Vetor GetSolucao()
        {
            var matrizSolucao = _matrizCoeficientes.Inversa() * _vetorResposta;
            Vetor vetorSolucao = new Vetor(matrizSolucao.NumeroDeLinhas);
            for (int j = 0; j < vetorSolucao.Comprimento; j++)
            {
                vetorSolucao.Item[j] = AjustarParaTolerancia(matrizSolucao.Item[j, 0]);
            }
            return vetorSolucao;
        }

    }
}
