using System;
using System.Collections.Generic;
using System.Text;

namespace Matematica
{
    /// <summary>
    /// Operações aritiméticas fundamentais com matrizes
    /// </summary>
    /// <exception cref="DimensaoMatrizException">Exceção lançada sempre que os parametros de dimensões da matriz exigidos pelo método não forem atendidos</exception>
    public class Matriz
    {
        /// <summary>
        /// Construtor da matriz a partir de um Array bidimensional
        /// </summary>
        /// <param name="matriz">Array bidimensional</param>
        public Matriz(double[,] matriz)
        {
            Item = matriz;
        }
        /// <summary>
        /// Construtor da matriz a partir de especificações de números de linhas e colunas
        /// </summary>
        /// <param name="totalLinhas">número total de linhas da matriz</param>
        /// <param name="totalColunas">número total de colunas da matriz</param>
        public Matriz(int totalLinhas, int totalColunas)
        {
            Item = new double[totalLinhas, totalColunas];
        }

        /// <summary>
        /// Array com os itens da matriz
        /// </summary>
        public double[,] Item { get; set; }

        /// <summary>
        /// Retorna o número de linhas da matriz
        /// </summary>
        public int NumeroDeLinhas {
            get
            {
                return this.Item.GetUpperBound(0) + 1;
            }
        }
        
        /// <summary>
        /// Retorna o número de colunas da matriz
        /// </summary>
        public int NumeroDeColunas
        {
            get
            {
                return this.Item.GetUpperBound(1) + 1;
            }
        }
        
        /// <summary>
        /// Realiza a soma entre duas matrizes com numero de linhas e colunas iguais.
        /// [Soma] = [MatrizA] + [MatrizB]
        /// </summary>
        /// <param name="matrizA">Primeira Matriz</param>
        /// <param name="matrizB">Segunda Matriz</param>
        /// <returns></returns>
        public static Matriz operator + (Matriz matrizA, Matriz matrizB){
            //verifica se as dimensões das duas matrizes são iguais
            if ((matrizA.NumeroDeLinhas==matrizB.NumeroDeLinhas && matrizA.NumeroDeColunas == matrizB.NumeroDeColunas)==false)
            {
                throw new DimensaoMatrizException("Soma de matrizes só pode ser realizada com matrizes de dimensões iguais");
            }
            
            double[,] soma = new double[matrizA.NumeroDeLinhas, matrizA.NumeroDeColunas];
            for (int i = 0; i <= soma.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= soma.GetUpperBound(1); j++)
                {
                    soma[i, j] = matrizA.Item[i, j] + matrizB.Item[i, j];
                }
            }
            return new Matriz(soma);
        }

        /// <summary>
        /// Realiza a subtração entre duas matrizes com numero de linhas e colunas iguais.
        /// [Subtração] = [MatrizA] - [MatrizB] 
        /// </summary>
        /// <param name="matrizA">Primeira Matriz</param>
        /// <param name="matrizB">Segunda Matriz</param>
        /// <returns></returns>
        public static Matriz operator - (Matriz matrizA, Matriz matrizB)
        {
            //verifica se as dimensões das duas matrizes são iguais
            if ((matrizA.NumeroDeLinhas == matrizB.NumeroDeLinhas && matrizA.NumeroDeColunas == matrizB.NumeroDeColunas) == false)
            {
                throw new DimensaoMatrizException("Subtração de matrizes só pode ser realizada com matrizes de dimensões iguais");
            }

            double[,] subtracao = new double[matrizA.NumeroDeLinhas, matrizA.NumeroDeColunas];
            for (int i = 0; i <= subtracao.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= subtracao.GetUpperBound(1); j++)
                {
                    subtracao[i, j] = matrizA.Item[i, j] - matrizB.Item[i, j];
                }
            }
            return new Matriz(subtracao);
        }

        /// <summary>
        /// Realiza a multiplicação entre duas matrizes, desde que o número de colunas da matriz A seja igual ao número de linhas da matriz B.
        /// </summary>
        /// <param name="matrizA">Primeira matriz</param>
        /// <param name="matrizB">Segunda matriz</param>
        /// <returns></returns>
        public static Matriz operator * (Matriz matrizA, Matriz matrizB)
        {
            //verifica se as dimensões das duas matrizes são compatíveis com o produto
            //número de colunas da primeira matriz deve ser igual ao numero de linhas da segunda
            if ((matrizA.NumeroDeColunas != matrizB.NumeroDeLinhas))
            {
                throw new DimensaoMatrizException("Multiplicação de matrizes só pode ser realizada se o número de colunas da primeira matriz for igual ao número de linhas da segunda");
            }
            //declara a matriz produto que contem o numero de linhas da primeira e o numero de colunas da segunda matriz
            double[,] produto = new double[matrizA.NumeroDeLinhas, matrizB.NumeroDeColunas];

            for (int i = 0; i <= produto.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= produto.GetUpperBound(1); j++)
                {
                    produto[i, j] = ProdutoLinhaColuna(matrizA.Item, matrizB.Item, i, j);
                }
            }
            return new Matriz(produto);
        }

        /// <summary>
        /// Realiza o produto entre uma constante e uma matriz
        /// </summary>
        /// <param name="c">Constante</param>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        public static Matriz operator * (double c, Matriz matriz)
        {
            return new Matriz(ProdutoConstanteMatriz(c, matriz.Item));
        }
       
        /// <summary>
        /// Realiza o produto entre uma constante e uma matriz
        /// </summary>
        /// <param name="c">Constante</param>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        public static Matriz operator *(Matriz matriz, double c )
        {
            return new Matriz(ProdutoConstanteMatriz(c, matriz.Item));
        }

        /// <summary>
        /// Realiza o produto entre uma constante e uma matriz
        /// </summary>
        /// <param name="vetor">Vetor</param>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        public static Matriz operator *(Matriz matriz, Vetor vetor)
        {
            Matriz matrizVetor = new Matriz(MatrizColuna(vetor.Item));
            return matriz*matrizVetor;
        }
        /// <summary>
        /// Realiza o produto entre uma constante e uma matriz
        /// </summary>
        /// <param name="vetor">Vetor</param>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        public static Matriz operator *(Vetor vetor, Matriz matriz)
        {
            Matriz matrizVetor = new Matriz(MatrizColuna(vetor.Item));
            return matrizVetor*matriz;
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
        /// Retorna o produto de uma matriz por uma constante
        /// </summary>
        /// <param name="c">Constante</param>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        private static double[,] ProdutoConstanteMatriz(double c, double[,] matriz)
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
        /// Retorma uma matriz identidade quando especificada a ordem. Ex: para retornar uma identidade 2x2 o valor da ordem deve ser 2.
        /// </summary>
        /// <param name="ordem">Número de linhas/colunas da matriz identidade</param>
        /// <returns></returns>
        public static Matriz Identidade(int ordem)
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
            return new Matriz(matriz);
        }

        /// <summary>
        /// Retorna a transposta da matriz
        /// </summary>
        public Matriz Transposta()
        {
            double[,] transposta = new double[this.NumeroDeLinhas, this.NumeroDeColunas];
            for (int i = 0; i <= transposta.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= transposta.GetUpperBound(1); j++)
                {
                    transposta[i, j] = this.Item[j, i];
                }
            }
            return new Matriz(transposta);
        }

        /// <summary>
        /// Retorna a oposta da matriz
        /// </summary>
        /// <returns></returns>
        public Matriz Oposta()
        {
            return new Matriz(ProdutoConstanteMatriz(-1.0, this.Item));
        }

        /// <summary>
        /// Retorna o determinante da matriz
        /// </summary>
        /// <returns></returns>
        public double Determinante()
        {
            return DeterminanteAux(this.Item);
        }

        /// <summary>
        /// Retorna o determinante da matriz
        /// </summary>
        private double DeterminanteAux(double[,] matriz)
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
                resultado += parametro[0, i] * (float)Math.Pow(-1, 0 + i) * DeterminanteAux(matriz);
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
        /// Retorna a Inversa da matriz
        /// </summary>
        /// <returns></returns>
        public Matriz Inversa()
        {
            return new Matriz(InversaAux(Item));
        }

        /// <summary>
        /// Retorna inversa da matriz
        /// </summary>
        /// <param name="matriz">Matriz</param>
        /// <returns>Retorna a inversa da matriz</returns>
        private double[,] InversaAux(double[,] matriz)
        {
            double determinante = DeterminanteAux(matriz);
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
                    cofator[i, j] = (double)Math.Round((double)Math.Pow(-1, i + j) * DeterminanteAux(matriz));
                }
            }
            adjunta = new Matriz(cofator).Transposta().Item;
            resultado = ProdutoConstanteMatriz(1 / DeterminanteAux(originalMatrix), adjunta);
            return resultado;
        }

        /// <summary>
        /// Retorna verdadeiro caso a matriz seja quadrada
        /// </summary>
        /// <returns></returns>
        public bool IsQuadrada()
        {
            return IsMatrizQuadrada(Item);
        }

        /// <summary>
        /// Verifica se a matriz é quadrada
        /// </summary>
        /// <param name="matriz">Matriz</param>
        /// <returns></returns>
        private static bool IsMatrizQuadrada(double[,] matriz)
        {
            return matriz.GetUpperBound(0) == matriz.GetUpperBound(1);
        }

        /// <summary>
        /// Transforma um vetor em uma matriz com uma linha
        /// </summary>
        /// <param name="vetor">vetor</param>
        /// <returns></returns>
        private static double[,] MatrizColuna(double[] vetor)
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
