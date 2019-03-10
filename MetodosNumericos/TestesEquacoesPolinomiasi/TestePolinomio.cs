using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquacoesPolinomiais;
using System;
using System.Collections.Generic;

namespace Testes
{
    [TestClass]
    public class TestePolinomio
    {
        [TestMethod]
        public void TesteGrauDoPolinomio()
        {
            //cria o polin�mio P(x)
            //P(x) = x� - 5x + 6
            Polinomio p = new Polinomio(new List<double> { 1,-5,6});
            Assert.AreEqual(2, p.GetGrau());
        }
        [TestMethod]
        public void TesteValorDoPolinomioNoPonto()
        {
            //cria o polin�mio P(x)
            //P(x) = x� - 5x + 6
            Polinomio p = new Polinomio(new List<double> { 1, -5, 6 });
            Assert.AreEqual(6, p.GetValorPolinomio(5));
        }
        [TestMethod]
        public void TesteValorDaDerivadaDoPolinomioNoPonto()
        {
            //cria o polin�mio P(x)
            //P(x) = x� - 5x + 6
            Polinomio p = new Polinomio(new List<double> { 1, -5, 6 });
            Assert.AreEqual(5, p.GetValorDerivada(5));
        }
        [TestMethod]
        public void RaizDePolinomioDoPrimeiroGrau()
        {
            //cria o polin�mio P(x)
            //P(x) = 5x-15
            Polinomio p = new Polinomio(new List<double> { 5, -15});
            Assert.AreEqual(3, p.GetRaizes()[0]);
        }
        [TestMethod]
        public void RaizDePolinomioDoSegundoGrauComDeltaMaiorQueZero()
        {
            //cria o polin�mio P(x)
            //P(x) = x�-5x+6
            Polinomio p = new Polinomio(new List<double> { 1,-5, 6 });
            var raizes = p.GetRaizes();
            Assert.AreEqual(2, raizes.Count);
            Assert.AreEqual(true, raizes.Contains(2) && raizes.Contains(3));
        }
        [TestMethod]
        public void RaizDePolinomioDoSegundoGrauComDeltaIgualAZero()
        {
            //cria o polin�mio P(x)
            //P(x) = x�-4x+4
            Polinomio p = new Polinomio(new List<double> { 1, -4, 4 });
            var raizes = p.GetRaizes();
            Assert.AreEqual(1, raizes.Count);
            Assert.AreEqual(2, raizes[0]);
        }
        [TestMethod]
        public void RaizDePolinomioDoSegundoGrauComDeltaMenorQueZero()
        {
            //cria o polin�mio P(x)
            //P(x) = 5x�+x+6
            Polinomio p = new Polinomio(new List<double> { 5, 1, 6 });
            var raizes = p.GetRaizes();
            Assert.AreEqual(0, raizes.Count);
        }
        [TestMethod]
        public void RaizDePolinomioDoTerceiroGrauComDeltaMenorQueZero()
        {
            //cria o polin�mio P(x)
            //P(x) = x�+4x�-x-4
            Polinomio p = new Polinomio(new List<double> { 1, 4, -1,-4 });
            var raizes = p.GetRaizes();
            Assert.AreEqual(3, raizes.Count);
            Assert.AreEqual(true, raizes.Contains(1) && raizes.Contains(-1) && raizes.Contains(-4));

        }
        [TestMethod]
        public void RaizDePolinomioDoTerceiroGrauComDeltaMaiorQueZero()
        {
            //cria o polin�mio P(x)
            //P(x) = x�-2x�+x-2
            Polinomio p = new Polinomio(new List<double> { 1, -2, 1, -2 });
            var raizes = p.GetRaizes();
            Assert.AreEqual(1, raizes.Count);
            Assert.AreEqual(true, raizes.Contains(2));
        }
        [TestMethod]
        public void RaizDePolinomioDoTerceiroGrauComDeltaIgualAZero()
        {
            //cria o polin�mio P(x)
            //P(x) = x�-3x-2
            Polinomio p = new Polinomio(new List<double> { 1, 0, -3, -2 });
            var raizes = p.GetRaizes();
            Assert.AreEqual(2, raizes.Count);
            Assert.AreEqual(true, raizes.Contains(2) && raizes.Contains(-1));
        }
        [TestMethod]
        public void RaizDePolinomioDeGrauN()
        {
            //cria o polin�mio P(x)
            //P(x) = 1x^6 + 4X^5 - 58X^4 -148X^3 +813X^2 +144x-756
            Polinomio p = new Polinomio(new List<double> { 1, 4, -58, -148, 813, 144, -756 });
            var raizes = p.GetRaizes();
            Assert.AreEqual(6, raizes.Count);
            Assert.AreEqual(true, raizes.Contains(1) && raizes.Contains(-1) && raizes.Contains(6) && raizes.Contains(-6) && raizes.Contains(3) && raizes.Contains(-7));
        }

    }
}
