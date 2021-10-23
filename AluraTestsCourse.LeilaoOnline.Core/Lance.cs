using System;
using System.Collections.Generic;
using System.Text;

namespace AluraTestsCourse.LeilaoOnline.Core
{
    public class Lance
    {
        public Interessada Cliente { get; }
        public double Valor {  get; }
        public Lance(Interessada cliente, double valor)
        {
            if(valor < 0)
            {
                throw new ArgumentException("Valor do lance não pode ser negativo, deve ser maior que zero.");
            }
            Cliente = cliente;
            Valor = valor;
        }
    }
}
