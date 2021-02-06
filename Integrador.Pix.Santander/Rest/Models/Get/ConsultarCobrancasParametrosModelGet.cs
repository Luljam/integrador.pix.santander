using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Integrador.Pix.Santander.Rest.Models.Get
{
    public class ConsultarCobrancasParametrosModelGet
    {
        [JsonProperty("inicio")]
        public string Inicio { get; set; }

        [JsonProperty("fim")]
        public string Fim { get; set; }

        [JsonProperty("txId")]
        public string TxtId { get; set; }

        [JsonProperty("cpf")]
        public string CpfPagador { get; set; }

        [JsonProperty("cnpj")]
        public string CnpjPagador { get; set; }

        [JsonProperty("paginaAtual")]
        public int? PaginaAtual { get; set; }

        [JsonProperty("itensPorPagina")]
        public int? ItensPorPagina { get; set; }
    }
}
