using FileDelivery.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDelivery.Util
{
    public class Mail
    {

        public static async Task SendEmailAsync(Entry entry, MailboxAddress to)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("File Delivery Application", "files@eikcalb.dev"));
            message.To.Add(to);
            message.Subject = "Your Files are ready!";
            message.Importance = MessageImportance.High;
            message.Priority = MessagePriority.Normal;

            BodyBuilder body = new BodyBuilder();
            body.HtmlBody = @$"Hello {entry.Name},
            <br />
            <p>Your files are ready for download.</p>
            <br />
            <p>The files have been added as attachments to this mail and can also be accessed in the application.</p>
            <br />
            <p>To access these files in the application, use the following details:</p>
            <p>    Email Address - {entry.EmailAddress}
            <p>    Transaction ID - {entry.TransactionID}

            <br /><br />
            <p>Thank you for using File Delivery</p>
            <p>-- Eikcalb</p>";

            foreach (Upload upload in entry.Uploads)
            {
                if (System.IO.File.Exists(upload.Path))
                {
                    body.Attachments.Add(upload.Path).ContentDisposition.FileName = upload.FileName;
                }
            }

            // Set the message body.
            message.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                // Oauth2 not implemented for mailbox yet.
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(Environment.GetEnvironmentVariable("SMTP_CLIENT_ADDRESS"), Environment.GetEnvironmentVariable("SMTP_CLIENT_PASSWORD"));

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }


    }
}
