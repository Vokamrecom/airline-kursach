using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace Airline.Logic.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, int? userId, int? ticketId, int? bookingId)
        {
            var emailMessage = new MimeMessage();

            var body = "";
            if (userId.HasValue)
                body += $"We apologize but your account and all your tickets and bookings has been deleted by the moderator.";
            else if (ticketId.HasValue)
                body += $"We apologize but your ticket {ticketId} has been deleted by the moderator.";
            else if (bookingId.HasValue)
                body += $"We apologize but your booking {bookingId} has been deleted by the moderator.";

            emailMessage.From.Add(new MailboxAddress("Ermakoff Airline notification", "ermakovkiril@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Notification";
            emailMessage.Body = new TextPart("Plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("ermakovkiril@gmail.com", "");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async Task SendConfirmationMessage(string email, string callBack)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Ermakoff Airline notification", "ermakovkiril@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Notification";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "You need to confirm your email by this <a href=\"" + callBack + "\">href</a>"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("ermakovkiril@gmail.com", "");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}