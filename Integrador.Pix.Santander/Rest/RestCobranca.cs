using Integrador.Pix.Santander.Rest.Base;
using Integrador.Pix.Santander.Rest.Models;
using Integrador.Pix.Santander.Rest.Models.Get;
using Integrador.Pix.Santander.Rest.Models.Post;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Integrador.Pix.Santander.Rest
{
    public class RestCobranca : RestBase
    {
        public RestCobranca(Configuracao configuracao)
             : base(configuracao)
        { 
        }

        public CriarCobrancaModelGet CriarCobranca(CriarCobrancaModelPost post)
        {
            if (!post?.EhValido() ?? true) 
                return new CriarCobrancaModelGet { PayLoad = string.Empty };

            var endpoint = $"{UrlBase}/api/v1/cob/{post.TxId}";
            var model = Put<CriarCobrancaModelPost, CriarCobrancaModelGet>(endpoint, post);
            return model;
        }

        public CriarCobrancaModelGet ObterCobranca(string txtId)
        {
            var endpoint = $"{UrlBase}/api/v1/cob/{txtId}";
            var model = Get<CriarCobrancaModelGet>(endpoint);
            return model;
        }

        public ConsultarCobrancasResultModelGet ConsultarCobrancasRecebidas(ConsultarCobrancasParametrosModelGet parametros)
        {
            var jsonData = JsonConvert.SerializeObject(parametros);
            var dicionario = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
            var endpoint = $"{UrlBase}/api/v1/pix?{KeyValueToString(dicionario)}";

            var model = Get<ConsultarCobrancasResultModelGet>(endpoint);
            return model;
        }
    }
}
