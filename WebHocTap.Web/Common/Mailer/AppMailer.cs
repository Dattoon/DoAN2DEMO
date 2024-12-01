using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace WebHocTap.Web.Common.Mailer
{
    public class AppMailer
    {
        private AppMailConfiguration mailConfig;
        public AppMailSender Sender { get; set; }
        public AppMailReciver Reciver { get; set; }

        public AppMailer(AppMailConfiguration _config)
        {
            mailConfig = _config;
        }

        private bool PrivateSend()
        {
            if (this.Sender == null || this.Reciver == null || this.mailConfig == null)
            {
                throw new Exception("Không thể gửi mail với dữ liệu rỗng");
            }

            if (String.IsNullOrEmpty(Reciver.Email))
            {
                return false;
            }

            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(Sender.Name, mailConfig.Email);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(Reciver.Name, Reciver.Email);
            message.To.Add(to);

            message.Subject = Sender.Subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = Sender.Content;
            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(mailConfig.SmtpServer, mailConfig.Port, true);
                    client.Authenticate(mailConfig.Email, mailConfig.Password);

                    client.Send(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Send()
        {
            this.PrivateSend();
        }

        // Gửi mail dưới nền, không cần chờ xử lý
        public void SendInBackground()
        {
            Thread thMail = new Thread(() =>
            {
                this.PrivateSend();
            });
            thMail.Start();
        }

        public async Task SendEmailAsync(AppMailSender sender, AppMailReciver reciver)
        {
            if (sender == null || reciver == null || mailConfig == null)
            {
                throw new Exception("Không thể gửi mail với dữ liệu rỗng");
            }

            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress(sender.Name, mailConfig.Email);
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(reciver.Name, reciver.Email);
            message.To.Add(to);

            message.Subject = sender.Subject;

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = sender.Content;
            message.Body = bodyBuilder.ToMessageBody();

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(mailConfig.SmtpServer, mailConfig.Port, true);
                    await client.AuthenticateAsync(mailConfig.Email, mailConfig.Password);

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void SendToList(AppMailSender sender, IEnumerable<AppMailReciver> recivers, AppMailConfiguration mailConfig)
        {
            if (sender == null || recivers == null || mailConfig == null || recivers.Count() == 0)
            {
                throw new Exception("Không thể gửi mail với dữ liệu rỗng");
            }

            try
            {
                Parallel.ForEach(recivers, (reciver) =>
                {
                    AppMailer mailer = new AppMailer(mailConfig);
                    mailer.Sender = sender;
                    mailer.Reciver = reciver;
                    mailer.Send();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void SendToListInBackground(AppMailSender sender, IEnumerable<AppMailReciver> recivers, AppMailConfiguration mailConfig)
        {
            Thread thMail = new Thread(() =>
            {
                SendToList(sender, recivers, mailConfig);
            });
            thMail.Start();
        }
    }
}
