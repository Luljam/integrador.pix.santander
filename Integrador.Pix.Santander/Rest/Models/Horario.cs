using Newtonsoft.Json;
using System;

namespace Integrador.Pix.Santander.Rest.Models
{
    public class Horario
    {
        [JsonProperty("solicitacao")]
        public DateTime HorarioSolicitacao { get; set; }

        [JsonProperty("liquidacao")]
        public DateTime HorarioLiquidacao { get; set; }
    }
}
