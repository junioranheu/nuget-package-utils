
# @junioranheu-utils-package

Pacote NuGet contendo múltiplos métodos úteis para qualquer tipo de projeto em <b>.NET 9</b>.

## Publicação
O projeto foi publicado em [junioranheu-utils-package](https://www.nuget.org/packages/junioranheu-utils-package/)


## Utilização

👉 Uma vez que você instalou o pacote <i>junioranheu_utils_package</i>, você deve importá-lo da seguinte maneira:

```
using static junioranheu_utils_package.Fixtures.[Convert | Encrypt | Format | Get | HttpService | Post | Validate];
```

## Conteúdo/Fixtures

#### BulkCopy.cs

Os metodos referentes ao Bulk Copy foram alocados em um [novo projeto](https://github.com/junioranheu/nuget-package-Bulk.Sql).

#### Convert.cs

```
  Métodos genéricos para converter tipos de dados. 
  
  Exemplo: converte Base64 para File.
```

#### Encrypt.cs

```
  Métodos de criptografia e descriptografia, utilizando o pacote BCrypt.Net-Next.
```

#### Format.cs

```
  Métodos genéricos para formatar dados. 
  
  Exemplo: formata bytes para gigabytes.
```

#### Get.cs

```
  Métodos genéricos para obter ou gerar dados/informações. 
  
  Exemplos: gera o horário atual de Brasilia, gera uma string aleatória ou obtem se o ambiente é debug ou prod.
```

#### Post.cs

```
  Métodos diversos para fins distintos que, por sua vez, têm relação com requisições de ação. 
  
  Exemplos: envia um e-mail a um destinatário ou sobe uma imagem ao servidor.
```

#### Validate.cs

```
  Métodos específicos para validar dados. 
  
  Exemplo: valida e-mail ou valida senha.
```
