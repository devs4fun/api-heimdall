using ApiHeimdall.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApiHeimdall.Service
{
    public class EmailService : IEmailService
    {
        public bool Enviar(string email, string mensagem, string assunto)
        {
            try
            {
                // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage();
                // Remetente
                _mailMessage.From = new MailAddress("apiheimdall@gmail.com");

                // Destinatario seta no metodo abaixo

                //Constroi o MailMessage
                _mailMessage.CC.Add(email);
                _mailMessage.Subject = assunto;
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = mensagem;

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                //CONFIGURAÇÃO SEM PORTA
                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("apiheimdall@gmail.com", "RB25817087");

                _smtpClient.EnableSsl = true;

                _smtpClient.Send(_mailMessage);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
