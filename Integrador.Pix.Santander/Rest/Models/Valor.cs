using Newtonsoft.Json;

namespace Integrador.Pix.Santander.Rest.Models
{
    public class Valor
    {
        public Valor() { }
        public Valor(decimal valor, string formato = "{0:0.00}")
        {
            Original = string.Format(formato, valor).Replace(",", ".");
        }

        [JsonProperty("original")]
        public string Original { get; private set; }
    }
}
