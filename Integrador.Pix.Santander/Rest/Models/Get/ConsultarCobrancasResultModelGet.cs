using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Integrador.Pix.Santander.Rest.Models.Get
{
    public class ConsultarCobrancasResultModelGet
    {
        [JsonProperty("parametros")]
        public ConsultarCobrancasParametrosModelGet Parametros { get; set; }

        [JsonProperty("pix")]
        public List<Pixs> Pixs { get; set; }
    }

    public class Pixs
    {
        [JsonProperty("endToEndId")]
        public string EndToEndId { get; set; }

        [JsonProperty("txId")]
        public string TxId { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }

        [JsonProperty("horario")]
        public DateTime HorarioPagamento { get; set; }

        [JsonProperty("pagador")]
        public Pagador Pagador { get; set; }

        [JsonProperty("infoPagador")]
        public string InformacoesExtrasPagador { get; set; }

        [JsonProperty("devolucoes")]
        public Devolucao Devolucao { get; set; }
    }
}
