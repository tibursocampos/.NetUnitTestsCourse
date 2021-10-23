using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AluraTestsCourse.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilanAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private Interessada _ultimoCliente;
        private IList<Lance> _lances;
        private string v;
        private IModalidadeAvaliacao avaliador;

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }

        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilanAntesDoPregao;
            this.avaliador = avaliador;
        }

        private bool NovoLanceEhAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento) && (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceEhAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;

            }

        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new System.InvalidOperationException();
            }

            Ganhador = this.avaliador.Avalia(this);
            Estado = EstadoLeilao.LeilaoFinalizado;

            // Após criada a interface para desacoplamento
            //if (ValorDestino > 0)
            //{
            //    // modalidade oferta superior mais próxima
            //    Ganhador = Lances
            //        .DefaultIfEmpty(new Lance(null, 0))
            //        .Where(l => l.Valor > ValorDestino)
            //        .OrderBy(l => l.Valor)
            //        .FirstOrDefault();
            //}
            //else
            //{
            //    // modalidade maior valor
            //    Ganhador = Lances
            //        .DefaultIfEmpty(new Lance(null, 0))
            //        .OrderBy(l => l.Valor)
            //        .LastOrDefault();

            //    Estado = EstadoLeilao.LeilaoFinalizado;
            //}

        }


    }
}
