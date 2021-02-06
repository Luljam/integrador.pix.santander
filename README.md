# INTEGRADOR PIX SANTANDER [![Build Status](https://secure.travis-ci.org/morrisjs/morris.js.png?branch=master)](http://travis-ci.org/morrisjs/morris.js)
.NET Class Library for integration with PIX SANTANDER API <br />
.NET Framework 4.6

## Configurações
### web.config
```c#
    <add key="pix:url" value="https://trust-pix-h.santander.com.br" /> <!-- Homologação-->
    <add key="pix:url" value="https://trust-pix.santander.com.br" /> <!-- Produção -->
    <add key="pix:client_id" value="client_id" />
    <add key="pix:client_secret" value="client_secret" />
```

## Reunião
### Criando um PIX dinâmico
```c#                                    
  using(var pix = new Pix())
  {
      var cobranca = new CriarCobrancaModelPost(Faker.Random.Guid().ToString().Replace("-", "").ToUpper(),
                                          "chave_pix",
                                          "sua_mensagem",
                                          "seu_prazo_expiracao",
                                          "cpf_pagador_seu_pontos",
                                          "nome_pagador",
                                          string.Format("{0:0.00}", "valor_pagar").Replace(",", "."));
                                          
      var result = pix.Cobranca.CriarCobranca(cobranca);
  }                   
```

### Obter pix's recibidos
```c#
  using(var pix = new Pix())
  {
    var parametros = new ConsultarCobrancasParametrosModelGet
    {
        Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 03, 0, 0, 0).ToString("s") + "Z",
        Fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 03, 23, 59, 59).ToString("s") + "Z",
        ItensPorPagina = 1000
    };
                                          
    var result = pix.Cobranca.ConsultarCobrancaRecebidas(parametros);
  }   
```
