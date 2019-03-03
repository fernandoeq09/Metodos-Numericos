using System;

using System.Collections.Generic;
using System.Linq;


namespace EquacoesPolinomiais
{
    public class Polinomio
    {
        //construtor da classe Polinômio
        //o argumento deve uma lista com os coeficientes de x na ordem decrescente
        public Polinomio(List<double> coeficiente)
        {
            coeficiente.Reverse();
            this.coeficiente = new List<double>();
            foreach (var item in coeficiente)
            {
                this.coeficiente.Add(item);
            }
            this.grau = coeficiente.Count - 1;

            this.Tolerancia = 1E-8;
            this.InteracaoMaxima = 5000;
        }

        //recupera o grau do polinômio
        private readonly int grau;
        public int GetGrau()
        {
            return grau;
        }

        //coeficientes do polinômio
        private List<double> coeficiente;
        
        //tolerância dos métodos numéricos (predefinição=1e-8)
        public double Tolerancia { get; set; }
        //número máximo de interações dos métodos númericos
        public int InteracaoMaxima { get; set; }

        //recupera as raizes dos polinômio
        public List<double> GetRaizes()
        {
            List<double> raizes = new List<double>();

            if (this.grau == 1)
            {
                double r = -this.coeficiente[0] / this.coeficiente[1];
                raizes.Add(Arredondar(r));
                return raizes;
            }
            else if (this.grau == 2)
            {
                double a = this.coeficiente[2];
                double b = this.coeficiente[1];
                double c = this.coeficiente[0];
                double delta = Math.Pow(b, 2) - 4 * a * c;
                if (delta > 0)
                {
                    double rdelta = Math.Pow(delta, 0.5);
                    double x1 = (-b + rdelta) / (2 * a);
                    double x2 = (-b - rdelta) / (2 * a);
                    raizes.Add(Arredondar(x1));
                    raizes.Add(Arredondar(x2));
                }
                else if (delta == 0)
                {
                    double x = -b / (2 * a);
                    raizes.Add(x);
                }
                return raizes;
            }//Fómula de Baskara
            else if (this.grau == 3)
            {
                //forma padrão da equação do terceiro grau
                //x³+ax²+bx+c=0
                //1- determina os valores de a,b,c
                double a = this.coeficiente[2] / this.coeficiente[3];
                double b = this.coeficiente[1] / this.coeficiente[3];
                double c = this.coeficiente[0] / this.coeficiente[3];

                //realiza mudanca de variável x=t-a/3
                double p = b - Math.Pow(a, 2) / 3;
                double q = c + (2 * Math.Pow(a, 3) - 9 * a * b) / 27;

                double delta = Math.Pow((q / 2.0), 2) + Math.Pow((p / 3.0), 3);

                if (delta < 0)
                {
                    double e = Math.Sqrt(-delta);
                    double r = Math.Sqrt(Math.Pow(q, 2) / 4 + Math.Pow(e, 2));
                    double t = Math.Acos(-q / (2 * r));
                    double x1 = 2 * GetSinal(r) * Math.Pow(Math.Abs(r), 1.0 / 3.0) * Math.Cos(t / 3.0) - a / 3.0;
                    double x2 = 2 * GetSinal(r) * Math.Pow(Math.Abs(r), 1.0 / 3.0) * Math.Cos((t + 2 * Math.PI) / 3.0) - a / 3.0;
                    double x3 = 2 * GetSinal(r) * Math.Pow(Math.Abs(r), 1.0 / 3.0) * Math.Cos((t + 4 * Math.PI) / 3.0) - a / 3.0;
                    raizes.Add(Arredondar(x1));
                    raizes.Add(Arredondar(x2));
                    raizes.Add(Arredondar(x3));

                    return raizes;
                }
                else
                {
                    double e = Math.Sqrt(delta);
                    double u = GetSinal(-q / 2.0 + e) * Math.Pow(Math.Abs(-q / 2.0 + e), 1.0 / 3.0);
                    double v = GetSinal(-q / 2.0 - e) *Math.Pow(Math.Abs(-q / 2.0 - e), 1.0 / 3.0);
                    double x1 = u + v - a / 3;

                    Polinomio polinomioAux = this.GetReduzirGrauPolinomio(x1);
                    List<double> raizesPolinomioAux = polinomioAux.GetRaizes();
                    raizes.Add(Arredondar(x1));
                    foreach (var r in raizesPolinomioAux)
                    {
                        raizes.Add(Arredondar(r));
                    }
                    return raizes;
                }
            }//Método de Cardano
            else
            {
                double primeiraRaiz = this.GetZeroComNewton(this.GetCotaDeFujiwara());
                if (primeiraRaiz == 0 && this.GetValorPolinomio(primeiraRaiz) > this.Tolerancia)
                {
                    return raizes;
                }

                Polinomio polinomioAux = this.GetReduzirGrauPolinomio(primeiraRaiz);
                List<double> raizesPolinomioAux = polinomioAux.GetRaizes();

                raizes.Add(Arredondar(primeiraRaiz));
                foreach (var r in raizesPolinomioAux)
                {
                    raizes.Add(Arredondar(r));
                }
                return raizes;
            }//Metodo de Newton com Briot Ruffini
        }

        //reduz o grau do polinômio utilizando o método de briot ruffini
        private Polinomio GetReduzirGrauPolinomio(double raiz)
        {
            List<double> coeficientePolinomioReduzido = new List<double>();
            if (this.GetValorPolinomio(raiz)<=this.Tolerancia)
            {
                for (int i = this.grau; i > 0; i--)
                {
                    if (i == this.grau)
                    {
                        coeficientePolinomioReduzido.Add(this.coeficiente[i]);
                    }
                    else
                    {
                        double ckm1 = coeficientePolinomioReduzido.Last();
                        double cm = this.coeficiente[i];
                        coeficientePolinomioReduzido.Add(cm + raiz * ckm1);
                    }
                }
            }
            return new Polinomio(coeficientePolinomioReduzido);
        }

        //recupera o valor numérico do polinômio aplicado ao ponto x
        public double GetValorPolinomio(double x)
        {
            double ans = 0;
            for (int i = 0; i <= this.grau; i++)
            {
                ans += this.coeficiente[i] * Math.Pow(x, i);
            }
            return ans;
        }
        //recupera o valor numérico da derivada do polinômio aplicado ao ponto x
        public double GetValorDerivada(double x)
        {
            double ans = 0;
            for (int i = 1; i <= this.grau; i++)
            {
                ans += i * this.coeficiente[i] * Math.Pow(x, i - 1);
            }
            return ans;
        }

        //determina a cota de Fujiwara
        //-CotaFujiwara =< raiz =< CotaFujiwara
        private double GetCotaDeFujiwara()
        {
            List<double> termo = new List<double>();
            int n = this.grau;
            for (int i = 0; i < n; i++)
            {
                double valorBase = Math.Abs((this.coeficiente[i] / this.coeficiente[n]));
                double valorExpoente = (1 / ((double)n - (double)i));
                double ans = Math.Pow(valorBase, valorExpoente);
                termo.Add(ans);
            }
            return 2 * termo.Max();
        }

        //Aplica Metodo de Newton para recuperar uma raíz do polinômio com estimativa inicial x0
        private double GetZeroComNewton(double x0)
        {
            double x = x0;
            int interacoes = 0;
            while (Math.Abs(this.GetValorPolinomio(x)) >= this.Tolerancia)
            {
                interacoes++;
                if (interacoes > this.InteracaoMaxima)
                {
                    break;
                }
                double fx = this.GetValorPolinomio(x);
                double dx = this.GetValorDerivada(x);
                x = x - (fx / dx);
            }
            if (interacoes <= this.InteracaoMaxima)
            {
                return Math.Round(x, Convert.ToInt32(-Math.Log10(Tolerancia)));
            }
            else
            {
                return 0;
            }

        }
  
        //Arredonda valor baseado no número de casas decimais descritos na tolerância do método
        private double Arredondar(double r)
        {
            return Math.Round(r, Math.Abs((int)Math.Log10(this.Tolerancia)));
        }
        //Recupera o sinal de um número
        private static double GetSinal(double x)
        {
            if (x >= 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

    }
}
