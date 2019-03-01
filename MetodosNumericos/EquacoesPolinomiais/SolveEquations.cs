using System;
using System.Collections.Generic;
using System.Text;

namespace EquacoesPolinomiais
{
    public class SolveEquations
    {
        private const double semSolucao = -100000;
        /*!
         * Se a  Equação for a * x^3 + b * x^2 + c * x + d = 0
         * Os coeficientes serão: <a,b,c,d>
         */
        public static List<double> SolucionarEquacaoPolinomial(List<double> coeficiente)
        {
            while (Math.Abs(coeficiente[0]) < 0.01)
            {
                coeficiente.RemoveAt(0);
            }

            List<double> solucoes = new List<double>();
            if (coeficiente.Count > 3)
            {
                List<double> dcoeff = new List<double>();
                for (int i = 0; i < coeficiente.Count - 1; i++)
                    dcoeff.Add(coeficiente[i] * (coeficiente.Count - 1 - i));

                double sol = UmaSolucao_Polinomial(coeficiente, dcoeff, 1);
                if (sol != semSolucao)
                {
                    solucoes.Add(sol);

                    List<double> coEff = new List<double>();
                    coEff.Add(coeficiente[0]);
                    for (int i = 1; i < coeficiente.Count - 1; i++)
                    {
                        coEff.Add(coeficiente[i] + sol * coEff[i - 1]);
                    }
                    List<double> partialSol = SolucionarEquacaoPolinomial(coEff);
                    solucoes.AddRange(partialSol);
                }
            }
            else if (coeficiente.Count == 3)
            {
                double delta = Math.Pow(coeficiente[1], 2) - 4 * coeficiente[0] * coeficiente[2];
                if (delta >= 0)
                {
                    double rootVal = Math.Sqrt(delta);
                    solucoes.Add((-1 * coeficiente[1] + rootVal) / (2 * coeficiente[0]));
                    solucoes.Add((-1 * coeficiente[1] - rootVal) / (2 * coeficiente[0]));
                }
            }
            else if (coeficiente.Count == 2)
            {
                solucoes.Add(-1 * coeficiente[1] / coeficiente[0]);
            }
            return solucoes;
        }

        private static double UmaSolucao_Polinomial(List<double> coefficient, List<double> dCoeff, double initX)
        {
            double dFunc, func, x = initX, x0 = 0;
            const int numeroMaximoIteracoes = 1000;
            int itr = 0;

            while (Math.Abs(x - x0) > 0.0001)
            {
                if (itr++ > numeroMaximoIteracoes)
                {
                    return semSolucao;
                }
                x0 = x;
                func = 0; dFunc = 0;
                for (int i = 0; i < coefficient.Count; i++)
                {
                    func += coefficient[i] * Math.Pow(x, coefficient.Count - 1 - i);
                }
                for (int i = 0; i < dCoeff.Count; i++)
                    dFunc += dCoeff[i] * Math.Pow(x, dCoeff.Count - 1 - i);

                if (dFunc != 0)
                    x = x - func / dFunc;
                else if (func < 0.0001)
                    return x;
                else
                    x += 1;
            }
            return x;
        }

        public static double[] SolucionarEquacaoLinear(double[,] equationMatrix)
        {
            if (equationMatrix.GetLength(0) + 1 == equationMatrix.GetLength(1))
            {
                int nVar = equationMatrix.GetLength(0);

                for (int i = 0; i < nVar; i++)
                {
                    if (equationMatrix[i, i] == 0)
                    {
                        int j;
                        for (j = i + 1; j < nVar; j++)
                        {
                            if (equationMatrix[j, i] != 0)
                            {
                                for (int k = i; k < nVar + 1; k++)
                                    equationMatrix[i, k] += equationMatrix[j, k];
                                break;
                            }
                        }
                        if (j == nVar)
                            throw new Exception("Existem equações repetidas. Não é possível solucionar.");
                    }

                    //faz o elemento da diagonal iguala  1
                    for (int k = nVar; k >= i; k--)
                        equationMatrix[i, k] /= equationMatrix[i, i];

                    //usa operação ros para fazer uma matrix
                    for (int j = i + 1; j < nVar; j++)
                    {
                        for (int k = nVar; k >= i; k--)
                            equationMatrix[j, k] -= equationMatrix[i, k] * equationMatrix[j, i];
                    }
                }

                for (int i = nVar - 1; i > 0; i--)
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        equationMatrix[j, nVar] -= equationMatrix[j, i] * equationMatrix[i, nVar];
                        equationMatrix[j, i] = 0;
                    }
                }

                double[] ans = new double[nVar];
                for (int j = 0; j < nVar; j++)
                    ans[j] = equationMatrix[j, nVar];

                return ans;
            }
            else
                throw new Exception("Esta equação não tem solução.");
        }
    }
}
