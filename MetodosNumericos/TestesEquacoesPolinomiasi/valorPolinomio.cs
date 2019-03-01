using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquacoesPolinomiais;
using System;

namespace TestesEquacoesPolinomiasi
{
    [TestClass]
    public class valorPolinomio
    {
        [TestMethod]
        public void TesteGrauDoPolinomio()
        {
            //cria o polinômio P(x)
            //P(x) = x² - 5x + 6
            Polinomio p = new Polinomio(new double[] { 6,-5,1});
            Assert.AreEqual(2, p.GetGrau());
        }
        [TestMethod]
        public void TesteValorNoPonto()
        {
            //cria o polinômio P(x)
            //P(x) = x² - 5x + 6
            Polinomio p = new Polinomio(new double[] { 6, -5, 1 });
            Assert.AreEqual(6, p.Valor(5));
        }
        [TestMethod]
        public void TesteGetCotaFujiwara()
        {
            //cria o polinômio P(x)
            //P(x) = x^4 + 0x^3 -14x^2 + 24x - 10
            Polinomio p = new Polinomio(new double[] { -10,24,-14,0,1});
            Assert.AreEqual(7.48, Math.Round(p.GetCotaDeFujiwara(),2));
        }
    }
}
