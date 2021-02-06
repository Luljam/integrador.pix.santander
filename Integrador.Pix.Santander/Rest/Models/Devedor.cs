using Newtonsoft.Json;

namespace Integrador.Pix.Santander.Rest.Models
{
    public class Devedor
    {
        public Devedor() { }
        public Devedor(string cpf, string nome)
        {
            CPF = cpf;
            Nome = nome;
        }

        [JsonProperty("cpf")]
        public string CPF { get; private set; }

        [JsonProperty("nome")]
        public string Nome { get; private set; }
    }
}
