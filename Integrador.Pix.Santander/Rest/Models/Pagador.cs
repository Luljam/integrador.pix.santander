using Newtonsoft.Json;

namespace Integrador.Pix.Santander.Rest.Models
{
    public class Pagador
    {
        public Pagador() { }
        public Pagador(string cpf, string nome)
        {
            CPF = cpf;
            Nome = nome;
        }

        [JsonProperty("cpf")]
        public string CPF { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}
