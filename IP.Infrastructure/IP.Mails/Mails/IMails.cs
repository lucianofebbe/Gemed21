using IP.Mails.MailsSettings;

namespace IP.Mails.Mails
{
    public interface IMails
    {
        Task SendMail(MailsSettings.MailsSettings settings);
    }
}
