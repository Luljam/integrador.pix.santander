using Bogus;
using System;
using Xunit;
using Bogus.Extensions.Brazil;
using Integrador.Pix.Santander.Rest.Models.Get;
using Integrador.Pix.Santander.Rest.Models.Post;
using System.Configuration;
using Integrador.Pix.Santander.Rest.Models;

namespace Integrador.Pix.Santander.Tests.Config
{
    [CollectionDefinition(nameof(PixTestsFixtureCollection))]
    public class PixTestsFixtureCollection : ICollectionFixture<PixTestsFixture> { }

    public class PixTestsFixture
    {
        public Faker Faker;
        public Pix Pix;
        public PixTestsFixture()
        {
            Faker = new Faker("pt_BR");
            Pix = new Pix(
                new Configuracao
                (
                    ConfigurationManager.AppSettings["pix:client_id"].ToString(), 
                    ConfigurationManager.AppSettings["pix:client_secret"].ToString(),
                    ConfigurationManager.AppSettings["pix:path_certificate"].ToString(),
                    ConfigurationManager.AppSettings["pix:password_certificate"].ToString()
                ));
        }

        public CriarCobrancaModelPost ObterCobrancaValida()
        {
            var chave = ConfigurationManager.AppSettings["pix:chave_pix"].ToString();
            var cobranca = new Cobranca(chave);
            var devedor = new Devedor(Faker.Person.Cpf(false), Faker.Person.FullName);
            var valor = new Valor(0.05M);
            var post = new CriarCobrancaModelPost(cobranca, devedor, valor);
            return post;
        }

        public ConsultarCobrancasParametrosModelGet ObterParametrosConsultarCobrancas()
        {
            var get = new ConsultarCobrancasParametrosModelGet
            {
                Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00).ToString("s") + "Z",
                Fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59).ToString("s") + "Z",
                ItensPorPagina = 1000
            };
            return get;
        }
    }
}
