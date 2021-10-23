using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AluraTestsCourse.LeilaoOnline.Core
{
    public class ModalidadeAvaliacao : IModalidadeAvaliacao
    {
        public double ValorDestino { get; }

        public ModalidadeAvaliacao(double valorDestino)
        {
            ValorDestino = valorDestino;
        }
        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .Where(l => l.Valor > ValorDestino)
                .OrderBy(l => l.Valor)
                .FirstOrDefault();
        }
    }
}
