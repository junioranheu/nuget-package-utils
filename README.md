
# @junioranheu-utils-package

Pacote NuGet contendo múltiplos métodos úteis para qualquer tipo de projeto em .NET
## Conteúdo/Fixtures

#### BulkCopy.cs

```
  Método para realizar insert em massa super performática (Bulk Copy).

  Recebe um resultado de uma query (LINQ) como parâmetro, 
  converte todos os dados para uma DataTable e, finalmente, realiza o Bulk Insert.

  Aceita uma conexão tanto para o SQL Server, quanto para o MySQL.
```

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
## Publicação
O projeto foi publicado em [junioranheu-utils-package](https://www.nuget.org/packages/junioranheu-utils-package/1.0.0)
