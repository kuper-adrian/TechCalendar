using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace TechCalendar.Web.Handler.Event
{
    public class EventSubmissionHandler : ICommandHandler<EventSubmission>
    {
        private readonly string _gmailAddress;
        private readonly string _gmailAppPassword;

        public EventSubmissionHandler(string gmailAddress, string gmailAppPassword)
        {
            _gmailAddress = gmailAddress;
            _gmailAppPassword = gmailAppPassword;
        }

        public async Task HandleAsync(EventSubmission query)
        {
            try
            {
                string text = 
                    $"Title: {query.Title}\n"
                    + $"Start Date: {query.StartDate.ToString()}\n"
                    + $"Start Time: {query.StartTime.ToString()}\n"
                    + $"End Date: {query.EndDate.ToString()}\n"
                    + $"End Time: {query.EndTime.ToString()}\n"
                    + $"Stream URL: {query.StreamUrl}";

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("stream-schedule.io", _gmailAddress));
                message.To.Add(new MailboxAddress("Adrian Kuper", _gmailAddress));
                message.Subject = $"Event Submission - {query.Title}";
                message.Body = new TextPart("plain") { Text = text };

                using (var client = new SmtpClient()) 
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s,c,h,e) => true;

                    client.Connect("smtp.gmail.com", 465, true);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(_gmailAddress, _gmailAppPassword);
                    await client.SendAsync(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Event-Submission failed!");
                System.Console.WriteLine(e);
            }
        }
    }
}