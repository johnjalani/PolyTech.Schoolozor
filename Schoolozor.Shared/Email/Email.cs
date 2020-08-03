using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Schoolozor.Shared
{
    public class Email
    {
        private static string _smtpAccount;
        private static string _smtpPassword;
        private static string _smtpHost;
        private static int _portNumber;
        private static bool _enableSSL;
        private static string _Bccs;
        private static string _emailBodyHtmlPath;
        public Email(IOptions<EmailSettings> email)
        {
            _smtpAccount = email.Value.smtpAccount;
            _smtpPassword = email.Value.smtpPassword;
            _smtpHost = email.Value.smtpHost;
            _portNumber = email.Value.portNumber;
            _enableSSL = email.Value.enableSSL;
            _Bccs = email.Value.Bccs;
            _emailBodyHtmlPath = email.Value.emailBodyHtmlPath;
        }

        public void Send(string emailTo, string subject, string body, string fromDisplayName = null)
        {
            using (MailMessage mail = new MailMessage())
            {
                if (fromDisplayName != null)
                {
                    mail.From = new MailAddress(fromDisplayName, fromDisplayName);
                }
                else
                {
                    mail.From = new MailAddress(_smtpAccount);
                }
                foreach (var email in emailTo.Split(';'))
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        mail.To.Add(email);
                    }
                }
                if (!string.IsNullOrEmpty(_Bccs))
                {
                    foreach (var email in _Bccs.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(email))
                        {
                            mail.Bcc.Add(email);
                        }
                    }
                }
                //check if mail.To > 0
                if (mail.To.Count == 0)
                {
                    Log.Error("Invalid Recipient.");
                    throw new Exception("Invalid Recipient.");
                }

                mail.Body = body;
                mail.Subject = subject;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(_smtpHost, _portNumber))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_smtpAccount, _smtpPassword);
                    smtp.EnableSsl = _enableSSL;

                    Log.Information($"Send Email {emailTo}");
                    smtp.Send(mail);
                }
            }
        }
    }

    public class EmailSettings
    {
        public string smtpAccount { get; set; }
        public string smtpPassword { get; set; }
        public string smtpHost { get; set; }
        public int portNumber { get; set; }
        public bool enableSSL { get; set; }
        public string Bccs { get; set; }
        public string emailBodyHtmlPath { get; set; }
    }
}
