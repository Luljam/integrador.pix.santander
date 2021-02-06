using Newtonsoft.Json;

namespace Integrador.Pix.Santander.Rest.Models
{
    public class Propriedade
    {
        [JsonProperty("nome")]
        public string Chave { get; set; }

        [JsonProperty("valor")]
        public string Valor { get; set; }
    }
}
