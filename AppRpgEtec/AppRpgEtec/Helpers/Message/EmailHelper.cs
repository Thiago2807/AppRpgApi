﻿using System.Net;
using System.Net.Mail;
using System.Text;

namespace AppRpgEtec.Helpers.Message;

public class EmailHelper
{
    public async Task EnviarEmail(Models.Email email)
    {
		try
		{
			string toEmail = email.Destinatario;

			MailMessage mailMessage = new MailMessage()
			{
				From = new MailAddress(email.Remetente, "App Rpg Etec")
			};

			mailMessage.To.Add(new MailAddress(toEmail));

			if (!string.IsNullOrEmpty(email.DestinatarioCopia))
				mailMessage.CC.Add(new MailAddress(email.DestinatarioCopia));

			mailMessage.Subject = "App RPG Etec - " + email.Assunto;
			mailMessage.Body = GerarCorpoEmail(email.Mensagem);
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;

			using (SmtpClient smtp = new SmtpClient(email.DominioPrimario, email.PortaPrimaria))
			{
				smtp.Credentials = new NetworkCredential(email.Remetente, email.RemetentePassword);
				smtp.EnableSsl = true;
				await smtp.SendMailAsync(mailMessage); 
			}
        }
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
    }

	public string GerarCorpoEmail(string mensagem)
	{
		try
		{
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<html> ");
            sb.AppendLine("<body> ");
            sb.AppendLine(" <div style = 'text-align:center'> ");
            sb.AppendLine(" <div style = 'text-align:left'> ");
            sb.AppendLine(" <table style = 'width: 600px; border:1px solid #0089cf;' border = '0' cellspacing = '0' cellpadding = '0' > ");
            sb.AppendLine(" <tbody> ");
            sb.AppendLine(" <tr style = 'background- color: #0089cf;'> ");
            sb.AppendLine(" <td> ");
            sb.AppendLine(" <table style = 'width: 100%;' border = '0' cellspacing = '0' cellpadding = '0' > ");
            sb.AppendLine(" <tbody> ");
            sb.AppendLine(" <tr> ");
            sb.AppendLine(" <td style = 'width: 224px;'><img src ='https://etechoracio.com.br/imagens/logo_selo_positivo.png' alt = 'Etec Professor Horácio Augusto da Silveira' width = '224px' height = '148' /></td> ");
            sb.AppendLine(" <td style = 'font-family: Arial; font-size: 40px;color: #fff; text-align: center;'> Etec HAS </td> ");
            sb.AppendLine(" </tr> ");
            sb.AppendLine(" </tbody> ");
            sb.AppendLine(" </table> ");
            sb.AppendLine(" </td> ");
            sb.AppendLine(" </tr> ");
            sb.AppendLine(" <tr ");
            sb.AppendLine(" <td style = 'padding:5px; height: 400px; font-size: 1.2rem; lineheight:1.467; font - family: ''''Segoe UI'''',''''Segoe WP'''',Arial,Sans - Serif; color: #333;vertical - align:top'> ");
            sb.AppendFormat("{0}", mensagem);
            sb.AppendLine(" </td> ");
            sb.AppendLine(" </tr> ");
            sb.AppendLine(" <tr style = 'background- color: #0089cf; color: #fff;'> ");
            sb.AppendLine(" <td style = 'padding:5px'> ");
            sb.AppendLine(" Etec Professor Horácio Augusto da Silveira <br/> ");
            sb.AppendLine(" Curso Técnico em Desenvolvimento de Sistemas <br/> ");
            sb.AppendLine(" Rua Alcântara, 113 - Vila Guilherme <br/> São Paulo/SP CEP: 02110-010 ");
            sb.AppendLine(" </td> ");
            sb.AppendLine(" </tr> ");
            sb.AppendLine(" </tbody> ");
            sb.AppendLine(" </table> ");
            sb.AppendLine("</div> ");
            sb.AppendLine("</div> ");
            sb.AppendLine("</body> ");
            sb.AppendLine("</html> ");

            return sb.ToString();
        }
		catch (Exception ex)
		{

			throw new Exception(ex.Message);
		}
	}
}
