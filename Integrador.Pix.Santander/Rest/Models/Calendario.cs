using Newtonsoft.Json;

namespace Integrador.Pix.Santander.Rest.Models
{
    public class Calendario
    {
        public Calendario(int expiracao)
        {
            Expiracao = expiracao;
        }

        [JsonProperty("expiracao")]
        public int Expiracao { get; set; }
    }
}
