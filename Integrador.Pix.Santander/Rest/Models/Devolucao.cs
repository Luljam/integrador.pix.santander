using Newtonsoft.Json;
using System;

namespace Integrador.Pix.Santander.Rest.Models
{
    public class Devolucao
    {
        [JsonProperty("id")]
        public string IdDevolucao { get; set; }

        [JsonProperty("rtrId")]
        public string IdRetornoPACS004 { get; set; }

        [JsonProperty("valor")]
        public string ValorDevolvido { get; set; }

        [JsonProperty("horario")]
        public Horario Horario { get; set; }

        /// <summary>
        /// EM_PROCESSAMENTO, DEVOLVIDO E NAO_REALIZADO
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
