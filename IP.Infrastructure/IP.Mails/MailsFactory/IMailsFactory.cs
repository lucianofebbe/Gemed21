using IP.Mails.Mails;

namespace IP.Mails.MailsFactory
{
    public interface IMailsFactory
    {
        Mails.Mails Create();
    }
}
