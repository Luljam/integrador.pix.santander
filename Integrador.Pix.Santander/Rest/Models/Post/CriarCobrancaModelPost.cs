using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Integrador.Pix.Santander.Rest.Models.Post
{
    public class CriarCobrancaModelPost : AbstractModel
    {
        [JsonProperty("txId")]
        public string TxId { get; private set; }

        [JsonProperty("chave")]
        public string Chave { get; private set; }

        [JsonProperty("solicitacaoPagador")]
        public string Mensagem { get; private set; }

        [JsonProperty("calendario")]
        public Calendario Calendario { get; private set; }

        [JsonProperty("devedor")]
        public Devedor Devedor { get; private set; }

        [JsonProperty("valor")]
        public Valor Valor { get; private set; }

        [JsonProperty("infoAdicionais")]
        public List<Propriedade> InformacoesExtras { get; private set; }

        protected CriarCobrancaModelPost() { }
        public CriarCobrancaModelPost(
            Cobranca cobranca,
            Devedor devedor,
            Valor valor,
            Calendario calendario = null,
            List<Propriedade> informacoesExtras = null)
        {
            Chave = cobranca.Chave;
            TxId = cobranca.TxtId;
            Mensagem = cobranca.Mensagem;

            Calendario = calendario;
            if (Calendario == null)
                Calendario = new Calendario(int.MaxValue);

            Devedor = devedor;
            Valor = valor;

            InformacoesExtras = new List<Propriedade>();
            if (informacoesExtras != null) InformacoesExtras.AddRange(informacoesExtras);
        }

        public override bool EhValido()
        {
            ValidationResult = new CobrancaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CobrancaValidation : AbstractValidator<CriarCobrancaModelPost>
    {
        public CobrancaValidation()
        {
            RuleFor(c => c.TxId)
                .NotEmpty()
                .WithMessage("Número identificador do cliente inválido");

            RuleFor(c => c.Chave)
                .NotEmpty()
                .WithMessage("Chave pix do recebedor inválida");

            RuleFor(c => c.Valor.Original)
                .NotEmpty()
                .WithMessage("Valor inválido");
        }
    }
}
