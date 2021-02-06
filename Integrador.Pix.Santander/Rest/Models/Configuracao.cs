using System.Configuration;

namespace Integrador.Pix.Santander.Rest.Models
{
    public class Configuracao
    {
        public Configuracao() { }
        public Configuracao(string client_id = "", string client_secret = "", string pathCertificate = "", string passwordCertificate = "")
        {
            ClientId = client_id;
            ClientSecret = client_secret;

            PathCertificate = pathCertificate;
            PasswordCertificate = passwordCertificate;
        }

        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string PathCertificate { get; private set; }
        public string PasswordCertificate { get; private set; }

        public void AdicionarCredencias()
        {
            ClientId = !string.IsNullOrEmpty(ClientId) ? ClientId : ConfigurationManager.AppSettings["pix:client_id"];
            ClientSecret = !string.IsNullOrEmpty(ClientSecret) ? ClientSecret : ConfigurationManager.AppSettings["pix:client_secret"];
        }

        public void AdicionarCertificado()
        {
            PathCertificate = !string.IsNullOrEmpty(PathCertificate) ? PathCertificate : ConfigurationManager.AppSettings["pix:path_certificate"];
            PasswordCertificate = !string.IsNullOrEmpty(PasswordCertificate) ? PasswordCertificate : ConfigurationManager.AppSettings["pix:password_certificate"];
        }
    }
}