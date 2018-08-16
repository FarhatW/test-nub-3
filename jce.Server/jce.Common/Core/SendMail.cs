using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace jce.Common.Core
{
    public class SendMail
    {
        public async Task<int> SendMailAsync(string test)
        {
            var desti = "julien.caselli@gmail.com";
            string subject = "test de mail";
            string corps = "coucou :)";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("JouéClub Entreprise", "info@joueclubentreprise.fr"));
            var adresse1 = new MailboxAddress(desti, desti);
            //var adresse3 = new MailboxAddress("AdminJCE", "dhoareau@joueclub.fr");
            var adresse4 = new MailboxAddress("AdminJCE", "jcaselli@joueclub.fr");

            message.To.AddRange(new[] { adresse1 });
            message.Bcc.AddRange(new[] { adresse4 });
            //if (resp.Count == 0)
            //{
            //    message.To.AddRange(new[] { adresse1 });
            //    message.Bcc.AddRange(new[] { adresse3, adresse4 });
            //}
            //else
            //{
            //    foreach (var item in resp)
            //    {
            //        var adresse = new MailboxAddress("Responsable CE", item);
            //        message.To.AddRange(new[] { adresse });
            //    }

            //    message.To.AddRange(new[] { adresse1 });
            //    message.Bcc.AddRange(new[] { adresse3, adresse4 });
            //}
            message.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = string.Format(corps);
            message.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.Connect("mail.joueclubentreprise.fr", 25);
                client.Authenticate("info@joueclubentreprise.fr", "m73rj6b");
                client.Send(message);
                client.Disconnect(true);
            }
            return await Task.FromResult(0);
        }
    }
}
