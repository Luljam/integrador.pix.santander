using System;
using System.Net.Http;
using Integrador.Pix.Santander.Rest;
using Integrador.Pix.Santander.Rest.Base;
using Integrador.Pix.Santander.Rest.Models;

namespace Integrador.Pix.Santander
{
    public class Pix : IDisposable
    {
        public Pix(Configuracao configuracao)
        {
            this.Cobranca = new RestCobranca(configuracao);
        }

        public void Dispose()
        {
            this.Cobranca = null;
        }

        public RestCobranca Cobranca { get; private set; }
    }
}
