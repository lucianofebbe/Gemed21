namespace IP.Mails.MailsSettings
{
    public class MailsSettings
    {
        public string Provider { get; set; }
        public string ToEmail { get; set; }
        public List<CcMail> CcEmail { get; set; }
        public string Subject { get; set; }
        public List<EmailAttachments> Attachments { get; set; }
        public string Body { get; set; }
        public bool BodyHtml { get; set; }
    }

    public class EmailAttachments
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
        public string ContentType { get; set; }
    }
    public class CcMail
    {
        public string ToEmail { get; set; }
    }
}
