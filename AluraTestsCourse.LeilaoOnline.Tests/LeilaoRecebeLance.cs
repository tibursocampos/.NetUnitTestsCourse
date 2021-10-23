using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AluraTestsCourse.LeilaoOnline.Core;
using System.Drawing;

namespace AluraTestsCourse.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimolance()
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            leilao.RecebeLance(fulano, 800);

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            //Assert
            var qtdeEsperada = 1;
            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }

        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 1000, 900, 1200, 500 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdeEsperada, double[] ofertas)
        {
            //Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);
            leilao.IniciaPregao();
            
           for(int i = 0; i < ofertas.Length; i++)
            {
                if(i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, ofertas[i]);
                }
                else
                {
                    leilao.RecebeLance(maria, ofertas[i]);
                }
            }

            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);


            //Assert            
            var qtdeObtida = leilao.Lances.Count();

            Assert.Equal(qtdeEsperada, qtdeObtida);
        }
    }
}
