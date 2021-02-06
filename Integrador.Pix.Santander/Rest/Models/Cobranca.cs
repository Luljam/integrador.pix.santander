using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Pix.Santander.Rest.Models
{
    public class Cobranca
    {
        public Cobranca() { }
        public Cobranca(string chave, string txtId = "", string mensagem = "")
        {
            Chave = chave;

            TxtId = txtId;
            if (string.IsNullOrEmpty(TxtId))
                TxtId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();

            Mensagem = mensagem;
            if (string.IsNullOrEmpty(Mensagem))
                Mensagem = "Cobrança Financeira";
        }

        public string Chave { get; private set; }
        public string TxtId { get; private set; }
        public string Mensagem { get; private set; }
    }
}
