using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using Integrador.Pix.Santander.Rest.Models;
using Integrador.Pix.Santander.Extensions;
using System;
using System.Net;
using Phidelis.Integracao.Teams.Rest.Models.Get;
using System.Security.Cryptography.X509Certificates;

namespace Integrador.Pix.Santander.Rest.Base
{
    public abstract class RestBase
    {
        internal virtual string UrlBase => ConfigurationManager.AppSettings["pix:url"];
        internal virtual Configuracao Configuracao { get; set; }
        public RestBase(Configuracao configuracao)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Configuracao = configuracao;
            Configuracao.AdicionarCredencias();
            Configuracao.AdicionarCertificado();
        }

        protected virtual string ObterToken()
        {
            Token token = new Token();

            using (var client = new HttpClient(ObterHttpClientHandler()))
            {
                var parametros = new Dictionary<string, string>()
                {
                    ["client_id"] = Configuracao.ClientId,
                    ["client_secret"] = Configuracao.ClientSecret
                };
                var content = new FormUrlEncodedContent(parametros);

                var endpoint = $"{UrlBase}/oauth/token?grant_type=client_credentials";
                var response = client.PostAsync(endpoint, content).GetAwaiter().GetResult();
                token = response.ToStringDeserialize<Token>();
            }

            if (string.IsNullOrEmpty(token?.access_token))
                throw new Exception("Não foi possível gerar o token!");

            return token.access_token;
        }

        protected virtual T Get<T>(string endpoint) where T : class, new()
        {
            T valor = new T();

            using (var client = new HttpClient(ObterHttpClientHandler()))
            {
                client.AtribuirJsonMediaType();
                client.AtribuirToken(ObterToken());

                var response = client.GetAsync(endpoint).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                valor = response.ToStringDeserialize<T>();
            }

            return valor;
        }

        protected virtual List<T> GetAll<T>(string endpoint) where T : class, new()
        {
            List<T> valor = new List<T>();

            using (var client = new HttpClient(ObterHttpClientHandler()))
            {
                client.AtribuirJsonMediaType();
                client.AtribuirToken(ObterToken());

                var response = client.GetAsync(endpoint).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                valor = response.ToStringDeserialize<List<T>>();
            }

            return valor;
        }

        protected virtual TResult Post<TInput, TResult>(string endpoint, TInput value)
            where TInput : AbstractModel
            where TResult : new()
        {
            TResult valor = new TResult();

            using (var client = new HttpClient(ObterHttpClientHandler()))
            {
                client.AtribuirJsonMediaType();
                client.AtribuirToken(ObterToken());

                var json = new StringContent(value.ToJson(), Encoding.UTF8, "application/json");
                var response = client.PostAsync(endpoint, json).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                valor = response.ToStringDeserialize<TResult>();
            }

            return valor;
        }

        protected virtual TResult PostInList<TInput, TResult>(string endpoint, TInput value)
            where TResult : new()
        {
            TResult valor = new TResult();

            using (var client = new HttpClient(ObterHttpClientHandler()))
            {
                client.AtribuirJsonMediaType();
                client.AtribuirToken(ObterToken());

                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.None);
                var json = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = client.PostAsync(endpoint, json).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                valor = response.ToStringDeserialize<TResult>();
            }

            return valor;
        }

        protected virtual TResult Delete<TResult>(string endpoint)
            where TResult : new()
        {
            TResult valor = new TResult();

            using (var client = new HttpClient(ObterHttpClientHandler()))
            {
                client.AtribuirJsonMediaType();
                client.AtribuirToken(ObterToken());

                var response = client.DeleteAsync(endpoint).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                valor = response.ToStringDeserialize<TResult>();
            }

            return valor;
        }

        protected virtual TResult Put<TInput, TResult>(string endpoint, TInput value)
            where TInput : AbstractModel
            where TResult : new()
        {
            TResult valor = new TResult();

            using (var client = new HttpClient(ObterHttpClientHandler()))
            {
                client.AtribuirJsonMediaType();
                client.AtribuirToken(ObterToken());

                var json = new StringContent(value.ToJson(), Encoding.UTF8, "application/json");
                var response = client.PutAsync(endpoint, json).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                valor = response.ToStringDeserialize<TResult>();
            }

            return valor;
        }

        protected virtual TResult PutInList<TInput, TResult>(string endpoint, TInput value)
            where TResult : new()
        {
            TResult valor = new TResult();

            using (var client = new HttpClient(ObterHttpClientHandler()))
            {
                client.AtribuirJsonMediaType();
                client.AtribuirToken(ObterToken());

                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.None);
                var json = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = client.PutAsync(endpoint, json).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();

                valor = response.ToStringDeserialize<TResult>();
            }

            return valor;
        }

        protected string PercentEncode(string value)
        {
            const string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            var result = new StringBuilder();

            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                    result.Append(symbol);
                else
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
            }

            return result.ToString();
        }

        protected string KeyValueToString(Dictionary<string, string> payload)
        {
            string body = "";
            foreach (var item in payload)
            {
                var value = PercentEncode(item.Value ?? "");
                if (!string.IsNullOrEmpty(value))
                    body += String.Format("{0}={1}&", PercentEncode(item.Key), (item.Value ?? ""));
            }
            if (body[body.Length - 1] == '&') body = body.Substring(0, body.Length - 1);
            return body;
        }

        protected HttpClientHandler ObterHttpClientHandler()
        {
            var cert2 = new X509Certificate2(Configuracao.PathCertificate, Configuracao.PasswordCertificate);
            var clientHandler = new HttpClientHandler();
            clientHandler.ClientCertificates.Add(cert2);
            clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
            return clientHandler;
        }
    }
}
