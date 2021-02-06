using Integrador.Pix.Santander.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Integrador.Pix.Santander.Extensions;

namespace Integrador.Pix.Santander.Rest.Models.Get
{
    public class CriarCobrancaModelGet
    {
        [JsonProperty("calendario")]
        public Calendario Calendario { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("txId")]
        public string TxtId { get; set; }

        [JsonProperty("revisao")]
        public int Revisao { get; set; }

        [JsonProperty("location")]
        public string PayLoad { get; set; }

        [JsonProperty("chave")]
        public string ChavePix { get; private set; }

        [JsonProperty("solicitacaoPagador")]
        public string Mensagem { get; private set; }

        [JsonProperty("devedor")]
        public Devedor Devedor { get; set; }

        [JsonProperty("valor")]
        public Valor Valor { get; set; }

        [JsonProperty("infoAdicionais")]
        public List<Propriedade> InformacoesExtras { get; set; }

        [JsonIgnore]
        public bool EstaPendente => Status.Equals("ATIVA");
        public bool EstaPago => Status.Equals("PAGO");
        public string PayloadQRCode => PayLoad.ConvertPayloadInStringQRCode();
        public string PayloadBase64(int width = 200, int heigth = 200) => PayloadQRCode.GerarBase64QRCode(width, heigth);
    }
}