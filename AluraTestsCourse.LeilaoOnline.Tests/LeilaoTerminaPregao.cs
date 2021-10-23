using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AluraTestsCourse.LeilaoOnline.Core;
using Xunit;

namespace AluraTestsCourse.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao    
    {     

        [Theory]
        [InlineData(1000, new double[] { 700, 800, 900, 1000 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 700 })]        
        [InlineData(800, new double[] { 800 })]       
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert          
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");            

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }

    }
}
