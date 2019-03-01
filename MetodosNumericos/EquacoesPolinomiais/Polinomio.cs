using System;

using System.Collections.Generic;
using System.Linq;


namespace EquacoesPolinomiais
{
    public class Polinomio
    {

        public Polinomio(List<double> coeficiente)
        {
            coeficiente.Reverse();
            this.coeficiente = new List<double>();
            foreach (var item in coeficiente)
            {
                this.coeficiente.Add(item);
            }
            this.grau = coeficiente.Count-1;

            this.Tolerancia = 1E-8;
            this.InteracaoMaxima = 5000;
        }
        private readonly int grau;
        public int GetGrau()
        {
            return grau;
        }

        private List<double> coeficiente;
        public List<double> Coeficiente
        {
            get { return coeficiente; }
            set { coeficiente = value; }
        }

        public double Tolerancia { get; set; }

        public int InteracaoMaxima { get; set; }

        public List<double> GetRaizes()
        {
            List<double> raizes = new List<double>();

            if (this.grau == 1)
            {
                raizes.Add(-this.coeficiente[0]/this.coeficiente[1]);
                return raizes;
            }
            else if(this.grau == 2)
            {
                double a = this.coeficiente[2];
                double b = this.coeficiente[1];
                double c = this.coeficiente[0];
                double delta = Math.Pow(b, 2) - 4 * a * c;
                if (delta > 0)
                {
                    double rdelta= Math.Pow(delta, 0.5);
                    double x1 = (-b + rdelta) / (2 * a);
                    double x2 = (-b - rdelta) / (2 * a);
                    raizes.Add(x1);
                    raizes.Add(x2);
                }
                else if (delta == 0)
                {
                    double x = -b / (2 * a);
                    raizes.Add(x);
                }
                return raizes;
            }
            else
            {
                double primeiraRaiz = this.GetZeroComNewton(this.GetCotaDeFujiwara());
                if (primeiraRaiz == 0 && this.GetValorPolinomio(primeiraRaiz) > this.Tolerancia)
                {
                    return raizes;
                }

                Polinomio polinomioAux = this.GetReduzirGrauPolinomio(primeiraRaiz);
                List<double> raizesPolinomioAux = polinomioAux.GetRaizes();

                raizes.Add(primeiraRaiz);
                foreach (var r in raizesPolinomioAux)
                {
                    raizes.Add(r);
                }
                return raizes;
            }
        }

        private Polinomio GetReduzirGrauPolinomio(double raiz)
        {
            List<double> coeficientePolinomioReduzido = new List<double>();

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
                    coeficientePolinomioReduzido.Add(cm+raiz* ckm1);
                }
            }
            return new Polinomio(coeficientePolinomioReduzido);
        }

        public double GetValorPolinomio(double x)
        {
            double ans = 0;
            for (int i = 0; i <= this.grau; i++)
            {
                ans += this.coeficiente[i] * Math.Pow(x,i);
            }
            return ans;
        }

        public double GetValorDerivada(double x)
        {
            double ans = 0;
            for (int i =1; i <= this.grau; i++)
            {
                ans += i*this.coeficiente[i] * Math.Pow(x, i-1);
            }
            return ans;
        }

        public double GetCotaDeFujiwara()
        {
            List<double> termo = new List<double>();
            int n = this.grau;
            for (int i = 0; i < n; i++)
            {
                double valorBase = Math.Abs((this.coeficiente[i] / this.coeficiente[n]));
                double valorExpoente = (1/((double)n - (double)i));
                double ans = Math.Pow(valorBase,valorExpoente);
                termo.Add(ans);
            }
            return 2*termo.Max();
        }

        public double GetZeroComNewton(double x0)
        {
            double x = x0;
            int interacoes = 0;
            while (Math.Abs(this.GetValorPolinomio(x))>=this.Tolerancia)
            {
                interacoes++;
                if (interacoes>this.InteracaoMaxima)
                {
                    break;
                }
                double fx = this.GetValorPolinomio(x);
                double dx = this.GetValorDerivada(x);
                x = x - (fx / dx);
            }
            if (interacoes<=this.InteracaoMaxima)
            {
                return Math.Round(x, Convert.ToInt32(-Math.Log10(Tolerancia)));
            }
            else
            {
                return 0;
            }
            
        }



    }
}
