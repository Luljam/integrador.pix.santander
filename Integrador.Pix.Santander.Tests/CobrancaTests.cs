using Integrador.Pix.Santander.Tests.Config;
using Xunit;
using Integrador.Pix.Santander.Utils;
using Integrador.Pix.Santander.Extensions;

namespace Integrador.Pix.Santander.Tests
{
    [Collection(nameof(PixTestsFixtureCollection))]
    public class CobrancaTests
    {
        private readonly PixTestsFixture _pixTestsFixture;
        public CobrancaTests(PixTestsFixture pixTestsFixture)
        {
            _pixTestsFixture = pixTestsFixture;
        }

        [Fact(DisplayName = "Calcular CRC-16")]
        [Trait("Cobrança", "Manipular Cobranças")]
        public void CalcularCRC16_ObterCRC16_DeveRetornarValido()
        {
            // Arrange
            var payload = "00020126870014br.gov.bcb.pix2565pix-h.santander.com.br/qr/v2/a2029c05-4119-4efe-bd95-8557b58a59915204000053039865802BR5903PIX6010VILA VELHA62070503***6304";

            //Act
            var result = Crc16Ccitt.ConvertString(payload);

            // Assert
            Assert.True(result.Length == 4);
        }

        [Theory(DisplayName = "Formatar Campos Válidos")]
        [Trait("Cobrança", "Manipular Cobranças")]
        [InlineData("PIX", "VILA VELHA")]
        [InlineData("UNIVERSIDADE VILA VELHA", "VILA VELHA")]
        [InlineData("UFES", "VITÓRIA")]
        public void CalcularCampos_ValidarTamanhoDosCampos_DeveRetornarValido(string merchantName, string merchantCity)
        {
            // Arrange && Act
            merchantName = merchantName.ObterCampoFormatado(25);
            merchantCity = merchantCity.ObterCampoFormatado(15);

            // Assert
            Assert.True(merchantName.Length <= 25);
            Assert.True(merchantCity.Length <= 15);
        }

        [Theory(DisplayName = "Formatar Campos no Limite Máximo")]
        [Trait("Cobrança", "Manipular Cobranças")]
        [InlineData("UNIVERSIDADE VILA VELHA UNIVERSIDADE VILA VELHA", "VILA VELHA")]
        [InlineData("UNIVERSIDADE VILA VELHA", "VILA VELHA UNIVERSIDADE VILA VELHA")]
        public void CalcularCampos_ValidarTamanhoDosCampos_DeveRetornarValidoComNomeCortado(string merchantName, string merchantCity)
        {
            // Arrange && Act
            merchantName = merchantName.ObterCampoFormatado(25);
            merchantCity = merchantCity.ObterCampoFormatado(15);

            // Assert
            Assert.True(merchantName.Length - 2 <= 25);
            Assert.True(merchantCity.Length - 2 <= 15);
        }

        [Fact(DisplayName = "Criar Cobrança - Objeto Válido")]
        [Trait("Cobrança", "Manipular Cobranças")]
        public void CriarCobranca_PostValidoDinamico_DeveRetornarValido()
        {
            // Arrange
            var post = _pixTestsFixture.ObterCobrancaValida();

            // Act
            var result = _pixTestsFixture.Pix.Cobranca.CriarCobranca(post);

            // Assert
            Assert.NotEmpty(result.PayloadQRCode);
            Assert.NotEmpty(result.PayloadBase64());
            Assert.Equal(0, post.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Gerar Base64 do QRCode Válido")]
        [Trait("Cobrança", "Manipular Cobranças")]
        public void GerarQRCode_QRCodePayload_DeveRetornarValido()
        {
            //Arrange
            var payload = "payload";

            //Act
            var result = payload.GerarBase64QRCode();

            // Assert
            Assert.Contains(result, "data:image/png;base64,");
        }

        [Fact(DisplayName = "Obter Cobrança Válida para Pagamento")]
        [Trait("Cobrança", "Obter Cobrança")]
        public void ObterCobranca_GetValidaParaPagamento_DeveRetornarValido()
        {
            // Arrange
            var txtId = "txtId";

            // Act
            var result = _pixTestsFixture.Pix.Cobranca.ObterCobranca(txtId);

            // Assert
            Assert.True(result.EstaPendente);
        }

        [Fact(DisplayName = "Criar Cobrança - Objeto Vazio")]
        [Trait("Cobrança", "Manipular Cobranças")]
        public void CriarCobranca_PostVazioDinamico_DeveRetornarInvalido()
        {
            // Arrange && Act
            var result = _pixTestsFixture.Pix.Cobranca.CriarCobranca(null);

            // Assert
            Assert.Empty(result.PayloadQRCode);
        }

        [Fact(DisplayName = "Consultar Pix Recebidos Com Sucesso")]
        [Trait("Cobrança", "Manipular Cobranças")]
        public void ConsultarCobrancas_ListaDeCobrancas_DeveEfetuarPesquisaComSucesso()
        {
            // Arrange
            var parametros = _pixTestsFixture.ObterParametrosConsultarCobrancas();

            // Act
            var result = _pixTestsFixture.Pix.Cobranca.ConsultarCobrancasRecebidas(parametros);

            // Assert
            Assert.NotNull(result?.Parametros);
            Assert.NotNull(result?.Pixs);
        }
    }
}
