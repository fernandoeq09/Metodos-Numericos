using System;
using System.Collections.Generic;
using System.Text;

namespace Matematica
{
    /// <summary>
    /// Operações aritiméticas fundamentais com matrizes
    /// </summary>
    /// <exception cref="DimensaoMatrizException">Exceção lançada sempre que os parametros de dimensões da matriz exigidos pelo método não forem atendidos</exception>
    public static class Matriz
    {
        /// <summary>
        /// Realiza a soma entre duas matrizes com numero de linhas e colunas iguais.
        /// [Soma] = [MatrizA] + [MatrizB]
        /// </summary>
        /// <param name="matrizA">Primeira Matriz</param>
        /// <param name="matrizB">Segunda Matriz</param>
        /// <returns></returns>
        public static double[,] Soma(double [,] matrizA, double [,] matrizB){
            //verifica se as dimensões das duas matrizes são iguais
            int LinhasMatriz1 = matrizA.GetUpperBound(0);
            int LinhasMatriz2 = matrizB.GetUpperBound(0);
            int ColunaMatriz1 = matrizA.GetUpperBound(1);
            int ColunaMatriz2 = matrizB.GetUpperBound(1);
            if ((matrizA.GetUpperBound(0)==matrizB.GetUpperBound(0) && matrizA.GetUpperBound(1) == matrizB.GetUpperBound(1))==false)
            {
                throw new DimensaoMatrizException("Soma de matrizes só pode ser realizada com matrizes de dimensões iguais");
            }
            int numeroLinhas = matrizA.GetUpperBound(0);
            int numeroColunas= matrizA.GetUpperBound(1);
            double[,] soma = new double[numeroLinhas+1, numeroColunas+1];
            for (int i = 0; i <= numeroLinhas; i++)
            {
                for (int j = 0; j <= numeroColunas; j++)
                {
                    soma[i, j] = matrizA[i, j] + matrizB[i, j];
                }
            }
            return soma;
        }

        /// <summary>
        /// Realiza a subtração entre duas matrizes com numero de linhas e colunas iguais.
        /// [Subtração] = [MatrizA] - [MatrizB] 
        /// </summary>
        /// <param name="matrizA">Primeira Matriz</param>
        /// <param name="matrizB">Segunda Matriz</param>
        /// <returns></returns>
        public static double[,] Subtracao(double[,] matrizA, double[,] matrizB)
        {
            //verifica se as dimensões das duas matrizes são iguais
            if ((matrizA.GetUpperBound(0) == matrizB.GetUpperBound(0) && matrizA.GetUpperBound(1) == matrizB.GetUpperBound(1)) == false)
            {
                throw new DimensaoMatrizException("Subtração de matrizes só pode ser realizada com matrizes de dimensões iguais");
            }
            int numeroLinhas = matrizA.GetUpperBound(0);
            int numeroColunas = matrizA.GetUpperBound(1);
            double[,] subtracao = new double[numeroLinhas+1, numeroColunas+1];
            for (int i = 0; i <= numeroLinhas; i++)
            {
                for (int j = 0; j <= numeroColunas; j++)
                {
                    subtracao[i, j] = matrizA[i, j] - matrizB[i, j];
                }
            }
            return subtracao;
        }

        /// <summary>
        /// Realiza a multiplicação entre duas matrizes, desde que o número de colunas da matriz A seja igual ao número de linhas da matriz B.
        /// </summary>
        /// <param name="matrizA">Primeira matriz</param>
        /// <param name="matrizB">Segunda matriz</param>
        /// <returns></returns>
        public static double [,] Produto(double[,] matrizA, double[,] matrizB)
        {
            //verifica se as dimensões das duas matrizes são compatíveis com o produto
            //número de colunas da primeira matriz deve ser igual ao numero de linhas da segunda
            if ((matrizA.GetUpperBound(1) != matrizB.GetUpperBound(0)))
            {
                throw new DimensaoMatrizException("Multiplicação de matrizes só pode ser realizada se o número de colunas da primeira matriz for igual ao número de linhas da segunda");
            }
            //declara a matriz produto que contem o numero de linhas da primeira e o numero de colunas da segunda matriz
            double[,] produto = new double[matrizA.GetUpperBound(0) + 1, matrizB.GetUpperBound(1) + 1];

            for (int i = 0; i <= produto.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= produto.GetUpperBound(1); j++)
                {
                    produto[i, j] = ProdutoLinhaColuna(matrizA, matrizB, i, j);
                }
            }
            return produto;
        }

        /// <summary>
        /// Codigo auxiliar para multiplicacao de matrizes, realiza multiplicação de uma linha especificada da matriz A por uma coluna especificado da Matriz B
        /// </summary>
        /// <param name="matrizA">Primeira Matriz</param>
        /// <param name="matrizB">Segunda Matriz</param>
        /// <param name="linhaDeA">Linha da primeira matriz</param>
        /// <param name="colunaDeB">Coluna da segunda matriz</param>
        /// <returns></returns>
        private static double ProdutoLinhaColuna(double[,] matrizA, double[,] matrizB, int linhaDeA, int colunaDeB)
        {
            double produto = 0;
            for (int i = 0; i <= matrizA.GetUpperBound(1); i++)
            {
                produto += matrizA[linhaDeA, i] * matrizB[i, colunaDeB];
            }
            return produto;
        }

        /// <summary>
        /// Retorma uma matriz identidade quando especificada a ordem. Ex: para retornar uma identidade 2x2 o valor da ordem deve ser 2.
        /// </summary>
        /// <param name="ordem">Número de linhas/colunas da matriz identidade</param>
        /// <returns></returns>
        public static double[,] Identidade(int ordem)
        {
            double[,] matriz = new double[ordem, ordem];
            for (int i = 0; i <= matriz.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= matriz.GetUpperBound(1); j++)
                {
                    if (i == j)
                    {
                        matriz[i, j] = 1;
                    }
                    else
                    {
                        matriz[i, j] = 0;
                    }
                }
            }
            return matriz;
        }

        /// <summary>
        /// Retorna a transposta de uma matriz
        /// </summary>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        public static double[,] Transposta(double[,] matriz)
        {
            double[,] transposta = new double[matriz.GetUpperBound(1)+1, matriz.GetUpperBound(0) + 1];
            for (int i = 0; i <= transposta.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= transposta.GetUpperBound(1); j++)
                {
                    transposta[i, j] = matriz[j, i];
                }
            }
            return transposta;
        }

        /// <summary>
        /// Retorna o produto de uma matriz por uma constante
        /// </summary>
        /// <param name="c">Constante</param>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        public static double[,] ProdutoConstanteMatriz(double c, double[,] matriz)
        {
            double[,] matrizResposta = new double[matriz.GetUpperBound(0) + 1, matriz.GetUpperBound(1) + 1];
            for (int i = 0; i <= matriz.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= matriz.GetUpperBound(1); j++)
                {
                    matrizResposta[i, j] = matriz[i, j] * c;
                }
            }
            return matrizResposta;
        }

        /// <summary>
        /// Retorna a oposta da matriz
        /// </summary>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        public static double[,] Oposta(double[,] matriz)
        {
            return ProdutoConstanteMatriz(-1.0, matriz);
        }


        /// <summary>
        /// Retorna o determinante da matriz
        /// </summary>
        /// <param name="matriz">Matriz</param>
        /// <returns>Retorna o valor do determinante de qualquer matriz</returns>
        public static double Determinante(double[,] matriz)
        {
            if (IsMatrizQuadrada(matriz)==false)
            {
                throw new DimensaoMatrizException("Só é possível determinar determinante para matrizes quadradas");
            }
            double[,] parametro = matriz;
            double resultado = 0;


            if (matriz.GetLength(0) == 1)
            {
                return matriz[0, 0];
            }

            for (int i = 0; i < parametro.GetLength(1); i++)
            {
                matriz = TrimArray(0, i, parametro);
                resultado += parametro[0, i] * (float)Math.Pow(-1, 0 + i) * Determinante(matriz);
            }

            return resultado;
        }

        /// <summary>
        /// Método para remover linhas e colunas, utilizado com o método para gerarDeterminante
        /// </summary>
        /// <param name="rowToRemove">Recebe a linha para remover</param>
        /// <param name="columnToRemove">Recebe a coluna para remover</param>
        /// <param name="originalArray">Recebe a matriz original</param>
        /// <returns>Retorna a matriz com linha e coluna removidos</returns>
        private static double[,] TrimArray(int rowToRemove, int columnToRemove, double[,] originalArray)
        {
            double[,] result = new double[originalArray.GetLength(0) - 1, originalArray.GetLength(1) - 1];

            for (int i = 0, j = 0; i < originalArray.GetLength(0); i++)
            {
                if (i == rowToRemove)
                    continue;

                for (int k = 0, u = 0; k < originalArray.GetLength(1); k++)
                {
                    if (k == columnToRemove)
                        continue;

                    result[j, u] = originalArray[i, k];
                    u++;
                }
                j++;
            }
            return result;
        }

        /// <summary>
        /// Retorna inversa da matriz
        /// </summary>
        /// <param name="matriz">Matriz</param>
        /// <returns>Retorna a inversa da matriz</returns>
        public static double[,] Inversa(double[,] matriz)
        {
            double determinante = Determinante(matriz);
            if(determinante==0)
            {
                throw new DimensaoMatrizException("A matriz não é inversível");
            }

            double[,] originalMatrix = matriz;
            double[,] cofator = new double[matriz.GetLength(0), matriz.GetLength(1)];
            double[,] adjunta = new double[matriz.GetLength(1), matriz.GetLength(0)];
            double[,] resultado = new double[matriz.GetLength(1), matriz.GetLength(0)];

            for (int i = 0; i <= matriz.GetLength(0); i++)
            {
                for (int j = 0; j <= matriz.GetLength(1); j++)
                {
                    matriz = TrimArray(i, j, originalMatrix);
                    cofator[i, j] = (double)Math.Round((double)Math.Pow(-1, i + j) * Determinante(matriz));
                }
            }
            adjunta = Transposta(cofator);
            resultado = ProdutoConstanteMatriz(1 / Determinante(originalMatrix), adjunta);
            return resultado;
        }

        /// <summary>
        /// Verifica se a matriz é quadrada
        /// </summary>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        public static bool IsMatrizQuadrada(double[,] matriz)
        {
            return matriz.GetUpperBound(0) == matriz.GetUpperBound(1);
        }

        /// <summary>
        /// Transforma um vetor em uma matriz com uma linha
        /// </summary>
        /// <param name="vetor">vetor</param>
        /// <returns></returns>
        public static double[,] MatrizColuna(double[] vetor)
        {
            double[,] matrizResposta = new double[vetor.GetUpperBound(0) + 1, 1];
            for (int i = 0; i <= vetor.GetUpperBound(0); i++)
            {
                matrizResposta[i, 0] = vetor[i];
            }
            return matrizResposta;
        }

    }
}
