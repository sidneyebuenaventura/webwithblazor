using System.Net.Mail;

namespace DigitalHubUI.Dto;

public class MailBuilder
{
    private MailMessage _mailMessage;

    public MailBuilder()
    {
        _mailMessage = new MailMessage();
    }

    public MailBuilder IsBodyHtml(bool isBodyHtml)
    {
        _mailMessage.IsBodyHtml = isBodyHtml;
        return this;
    }

    public MailBuilder WithSubject(string subject)
    {
        _mailMessage.Subject = subject;
        return this;
    }

    public MailBuilder WithBody(string body)
    {
        _mailMessage.Body = body;
        return this;
    }

    public MailBuilder From(string mailAddress, string? displayName)
    {
        _mailMessage.From = new MailAddress(mailAddress, displayName);
        return this;
    }

    public MailBuilder To(string mailAddress, string? displayName)
    {
        _mailMessage.To.Add(new MailAddress(mailAddress, displayName));
        return this;
    }

    public MailBuilder CC(params string[] mailAddresses)
    {
        mailAddresses.ToList().ForEach(ma => _mailMessage.CC.Add(new MailAddress(ma)));
        return this;
    }

    public MailMessage Build()
    {
        return _mailMessage;
    }
}