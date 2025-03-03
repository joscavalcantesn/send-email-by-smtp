using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace SendEmail;

public class Email(string provider, string username, string password)
{
    private string Provider { get; set; } = provider ?? throw new ArgumentNullException(nameof(provider));
    private string Username { get; set; } = username ?? throw new ArgumentNullException(nameof(username));
    private string Password { get; set; } = password ?? throw new ArgumentNullException(nameof(password));

    public void SendEmail(List<string> emailsTo, string subject, string body, List<string> attachments)
    {
        var message = PreparateMailMessage(emailsTo, subject, body, attachments);

        SendEmailBySmtp(message);
    }

    private MailMessage PreparateMailMessage(List<string> emailsTo, string subject, string body, List<string> attachments)
    {
        var mail = new MailMessage();
        mail.From = new MailAddress(Username);

        foreach (var email in emailsTo)
        {
            if (ValidateEmail(email))
            {
                mail.To.Add(email);
            }
        }

        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = true;

        foreach (var file in attachments)
        {
            var data = new Attachment(file, MediaTypeNames.Application.Octet);
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = File.GetCreationTime(file);
            disposition.ModificationDate = File.GetLastWriteTime(file);
            disposition.ReadDate = File.GetLastAccessTime(file);

            mail.Attachments.Add(data);
        }

        return mail;
    }

    private bool ValidateEmail(string email)
    {
        Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
        if (expression.IsMatch(email))
            return true;

        return false;
    }

    private void SendEmailBySmtp(MailMessage message)
    {
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
        smtpClient.Host = Provider;
        smtpClient.Port = 587;
        smtpClient.EnableSsl = true;
        smtpClient.Timeout = 50000;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(Username, Password);
        smtpClient.Send(message);
        smtpClient.Dispose();
    }

}