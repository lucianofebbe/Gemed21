﻿using IP.MongoDb.MongoDbFactory;
using MongoDB.Driver;
using System.Net;
using System.Net.Mail;

namespace IP.Mails.Mails
{
    public class Mails : IMails
    {
        private readonly IMongoDbFactory<MailsConfig.MailsConfig> mongoDbFactory;
        public Mails(IMongoDbFactory<MailsConfig.MailsConfig> mongoDb)
        {
            this.mongoDbFactory = mongoDb;
        }

        public async Task SendMail(MailsSettings.MailsSettings settings)
        {
            var mailConfig = await GetConfigurations(settings.Provider);
            var email = new MailMessage();
            email.From = new MailAddress(mailConfig.SenderEmail);
            email.To.Add(new MailAddress(settings.ToEmail));
            email.Subject = settings.Subject;

            if (settings.CcEmail != null)
                settings.CcEmail.ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item.ToEmail))
                        email.CC.Add(new MailAddress(item.ToEmail));
                });

            if (settings.Attachments != null)
                settings.Attachments.ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item.FileName))
                        email.Attachments.Add(new Attachment(new MemoryStream(item.FileBytes), item.FileName));
                });

            email.Body = settings.Body;
            email.IsBodyHtml = settings.BodyHtml;

            SmtpClient smtp = new SmtpClient(mailConfig.Host, mailConfig.Port);
            smtp.EnableSsl = mailConfig.EnableSsl;
            smtp.UseDefaultCredentials = mailConfig.UseDefaultCredentials;
            smtp.Credentials = new NetworkCredential(mailConfig.SenderEmail, mailConfig.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(email);
        }

        private async Task<MailsConfig.MailsConfig> GetConfigurations(string provider)
        {
            var result = await mongoDbFactory.Create("ConfigMails").Get(Builders<MailsConfig.MailsConfig>.Filter.Where(o => o.Provider == provider), new CancellationToken());
            return result;
        }
    }
}