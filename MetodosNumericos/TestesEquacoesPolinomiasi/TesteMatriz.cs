using Matematica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testes
{
    [TestClass]
    public class TesteMatriz
    {    
        [TestMethod]
        public void TesteSoma()
        {
            //cria a primeira matriz
            double[,] A = new double[,] { { 1, 2 }, { 3, 4 } };
            double[,] B = new double[,] { { 5, 6 }, { 7, 8 } };
            var S = Matriz.Soma(A, B);
            Assert.AreEqual(6, S[0, 0]);
            Assert.AreEqual(10, S[1, 0]);
            Assert.AreEqual(8, S[0, 1]);
            Assert.AreEqual(12, S[1, 1]);
        }
        [TestMethod]
        public void TesteSubtracao()
        {
            //cria a primeira matriz
            double[,] A = new double[,] { { 1, 2 }, { 3, 4 } };
            double[,] B = new double[,] { { 5, 6 }, { 7, 8 } };
            var S = Matriz.Subtracao(A, B);
            Assert.AreEqual(-4, S[0, 0]);
            Assert.AreEqual(-4, S[1, 0]);
            Assert.AreEqual(-4, S[0, 1]);
            Assert.AreEqual(-4, S[1, 1]);
        }
        [TestMethod]
        public void TesteProduto()
        {
            //cria a primeira matriz
            double[,] A = new double[,] { { 2, 3 }, { 1, 0 }, { 4, 5 } };
            double[,] B = new double[,] { { 3, 1 }, { 2, 4 } };
            var P = Matriz.Produto(A, B);

            Assert.AreEqual(12, P[0, 0]);
            Assert.AreEqual(14, P[0, 1]);
            Assert.AreEqual(3, P[1, 0]);
            Assert.AreEqual(1, P[1, 1]);
            Assert.AreEqual(22, P[2, 0]);
            Assert.AreEqual(24, P[2, 1]);
        }
        [TestMethod]
        public void TesteIdentidade()
        {
            //cria a primeira matriz
            var I = Matriz.Identidade(2);
            Assert.AreEqual(1, I[0, 0]);
            Assert.AreEqual(0, I[1, 0]);
            Assert.AreEqual(0, I[0, 1]);
            Assert.AreEqual(1, I[1, 1]);
        }
        [TestMethod]
        public void TesteTransposta()
        {
            double[,] a = new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            var transposta=Matriz.Transposta(a);
            Assert.AreEqual(1, transposta[0, 0]);
            Assert.AreEqual(3, transposta[0, 1]);
            Assert.AreEqual(5, transposta[0, 2]);
            Assert.AreEqual(2, transposta[1, 0]);
            Assert.AreEqual(4, transposta[1, 1]);
            Assert.AreEqual(6, transposta[1, 2]);
        }

    }
}
