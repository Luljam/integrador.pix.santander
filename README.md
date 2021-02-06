# INTEGRADOR PIX SANTANDER [![Build Status](https://secure.travis-ci.org/morrisjs/morris.js.png?branch=master)](http://travis-ci.org/morrisjs/morris.js)
.NET Class Library for integration with PIX SANTANDER API <br />
.NET Framework 4.6 <br />
.DLL focada na integração com o BANCO SANTANDER, entretanto caso queira utilizar para outro banco, basta alterar a geração do PAYLOAD, o restante das regras serão as mesmas.

## Configurações
### web.config
```c#
    <add key="pix:path_certificate" value="path_certificate" />
    <add key="pix:password_certificate" value="password_certificate" />
    
    <add key="pix:url" value="https://trust-pix-h.santander.com.br" />
    <add key="pix:chave_pix" value="chave_pix" />
    <add key="pix:client_id" value="client_id" />
    <add key="pix:client_secret" value="client_secret" />
```

## Reunião
### Criando um PIX dinâmico
```c#                                    
  using(var pix = new Pix(
                new Configuracao
                (
                    ConfigurationManager.AppSettings["pix:client_id"].ToString(), 
                    ConfigurationManager.AppSettings["pix:client_secret"].ToString(),
                    ConfigurationManager.AppSettings["pix:path_certificate"].ToString(),
                    ConfigurationManager.AppSettings["pix:password_certificate"].ToString()
                )))
  {
      var chave = ConfigurationManager.AppSettings["pix:chave_pix"].ToString();
      var cobranca = new Cobranca(chave, "txtid_opcional_gera_guid_caso_ignorado");
      var devedor = new Devedor(Faker.Person.Cpf(false), Faker.Person.FullName);
      var valor = new Valor(0.05M);
      
      var post = new CriarCobrancaModelPost(cobranca, devedor, valor);
      var result = pix.Cobranca.CriarCobranca(post);
  }                   
```

### Obter pix pelo id
```c#
  using(var pix = new Pix(
                new Configuracao
                (
                    ConfigurationManager.AppSettings["pix:client_id"].ToString(), 
                    ConfigurationManager.AppSettings["pix:client_secret"].ToString(),
                    ConfigurationManager.AppSettings["pix:path_certificate"].ToString(),
                    ConfigurationManager.AppSettings["pix:password_certificate"].ToString()
                )))
  {
                              
    var result = pix.Cobranca.ObterCobranca("txtid_da_cobranca");
  }   
```

### Obter pix's recibidos
```c#
  using(var pix = new Pix(
                new Configuracao
                (
                    ConfigurationManager.AppSettings["pix:client_id"].ToString(), 
                    ConfigurationManager.AppSettings["pix:client_secret"].ToString(),
                    ConfigurationManager.AppSettings["pix:path_certificate"].ToString(),
                    ConfigurationManager.AppSettings["pix:password_certificate"].ToString()
                )))
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
