using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaLG02
{
    internal class Contato
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }

        public override string ToString()
        {
            return $"{Nome};{Telefone};{DataNasc.Date}";
        }
    }
}
