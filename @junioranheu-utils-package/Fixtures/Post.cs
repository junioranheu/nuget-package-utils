﻿using junioranheu_utils_package.Entities.Output;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using static junioranheu_utils_package.Fixtures.Convert;

namespace junioranheu_utils_package.Fixtures
{
    public static class Post
    {
        /// <summary>
        /// Envia um e-mail (SMTP) via Gmail;
        /// www.youtube.com/watch?v=FZfneLNyE4o&ab_channel=AWPLife 
        /// </summary>
        public static async Task<bool> EnviarEmail(string _emailDominio, string _emailPorta, string _emailEmail, string _emailChave, string _emailRemetente,
            string emailTo, string assunto, string nomeArquivo, List<EmailDadosReplaceOutput> listaDadosReplace)
        {
            if (string.IsNullOrEmpty(emailTo) || string.IsNullOrEmpty(assunto) || string.IsNullOrEmpty(nomeArquivo))
            {
                return false;
            }

            string caminhoFinalArquivoHTML = $"{Directory.GetCurrentDirectory()}/Emails/{nomeArquivo}";
            string conteudoEmailHTML = AjustarConteudoEmailHTML(caminhoFinalArquivoHTML, listaDadosReplace);

            try
            {
                MailMessage mail = new()
                {
                    From = new MailAddress(_emailEmail, _emailRemetente)
                };

                mail.To.Add(new MailAddress(emailTo));
                // mail.CC.Add(new MailAddress(emailTo));

                mail.Subject = assunto;
                mail.Body = conteudoEmailHTML;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                // mail.Attachments.Add(new Attachment(arquivo));

                using SmtpClient smtp = new(_emailDominio, System.Convert.ToInt32(_emailPorta));
                smtp.Credentials = new NetworkCredential(_emailEmail, _emailChave);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
            catch (Exception)
            {
                return false;
            }

            return true;

            static string AjustarConteudoEmailHTML(string caminhoFinalArquivoHTML, List<EmailDadosReplaceOutput>? listaDadosReplace)
            {
                string conteudoEmailHtml = string.Empty;

                using (var reader = new StreamReader(caminhoFinalArquivoHTML))
                {
                    // #1 - Ler arquivo;
                    string readFile = reader.ReadToEnd();
                    string strContent = readFile;

                    // #2 - Remover tags desnecessárias;
                    strContent = strContent.Replace("\r", string.Empty);
                    strContent = strContent.Replace("\n", string.Empty);

                    // #3 - Replaces utilizando o parâmetro "listaDadosReplace";[
                    if (listaDadosReplace?.Count > 0)
                    {
                        foreach (var item in listaDadosReplace)
                        {
                            strContent = strContent.Replace($"[{item.Key}]", item.Value);
                        }
                    }

                    // #4 - Gerar resultado final;
                    conteudoEmailHtml = strContent.ToString();
                }

                return conteudoEmailHtml;
            }
        }

        /// <summary>
        /// arquivo = o arquivo em si, a variável IFormFile;
        /// nomeArquivo = o nome do novo objeto em questão;
        /// nomePasta = nome do caminho do arquivo, da pasta. Por exemplo: /Uploads/Usuarios/. "Usuarios" é o caminho;
        /// nomeArquivoAnterior = o nome do arquivo que constava anterior, caso exista;
        /// hostingEnvironment = o caminho até o wwwroot. Ele deve ser passado por parâmetro, já que não funcionaria aqui diretamente no BaseController;
        /// </summary>
        public static async Task<string?> UparImagem(IFormFile arquivo, string nomeArquivo, string nomePasta, string? nomeArquivoAnterior, string webRootPath)
        {
            if (arquivo.Length <= 0)
            {
                return string.Empty;
            }

            // Procedimento de inicialização para salvar nova imagem;
            string restoCaminho = $"/{nomePasta}/"; // Acesso à pasta referente; 

            // Verificar se o arquivo tem extensão, se não tiver, adicione;
            if (!Path.HasExtension(nomeArquivo))
            {
                nomeArquivo = $"{nomeArquivo}.jpg";
            }

            // Verificar se já existe uma foto caso exista, delete-a;
            if (!string.IsNullOrEmpty(nomeArquivoAnterior))
            {
                string caminhoArquivoAtual = webRootPath + restoCaminho + nomeArquivoAnterior;

                // Verificar se o arquivo existe;
                if (System.IO.File.Exists(caminhoArquivoAtual))
                {
                    System.IO.File.Delete(caminhoArquivoAtual); // Se existe, apague-o; 
                }
            }

            // Salvar aquivo;
            string caminhoDestino = webRootPath + restoCaminho + nomeArquivo; // Caminho de destino para upar;
            using (FileStream fs = File.Create(caminhoDestino))
            {
                await arquivo.CopyToAsync(fs);
            }

            return nomeArquivo;
        }

        /// <summary>
        /// Exemplo de streaming de um arquivo dividido em chunks;
        /// 
        /// Sites para testar/validar os chunks gerados:
        /// Base64 to .mp4: base64.guru/converter/decode/video;
        /// Base64 to .jpg: onlinejpgtools.com/convert-base64-to-jpg;
        /// 
        /// Como chamar o método: ibb.co/rsqnY42 [Imagem de exemplo];
        /// </summary>
        public static async IAsyncEnumerable<StreamingFileOutput> StreamFileEmChunks(string arquivo, long chunkSizeBytes, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            if (arquivo is null || chunkSizeBytes < 1)
            {
                throw new Exception("Os parâmetros 'nomeArquivo' e 'chunkSizeBytes' não devem ser nulos");
            }

            Stream? stream = await ConverterPathParaStream(arquivo, chunkSizeBytes) ?? throw new Exception("Houve um erro interno ao buscar arquivo no servidor e convertê-lo em Stream");
            byte[]? buffer = new byte[chunkSizeBytes];

            while (!cancellationToken.IsCancellationRequested && (await stream.ReadAsync(buffer, cancellationToken) > 0))
            {
                byte[]? chunk = new byte[chunkSizeBytes];

                try
                {
                    buffer.CopyTo(chunk, 0);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Houve um erro interno. Mais informações: {ex.Message}");
                }

                yield return new StreamingFileOutput()
                {
                    PorcentagemCompleta = System.Convert.ToDouble(stream.Position) / System.Convert.ToDouble(stream.Length) * 100,
                    Chunk = chunk
                };
            }
        }
    }
}