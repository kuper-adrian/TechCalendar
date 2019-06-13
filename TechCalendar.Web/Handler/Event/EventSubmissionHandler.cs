using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using RestSharp.Authenticators;
using TechCalendar.Web.Models;
using TechCalendar.Web.Persistence;
using TechCalendar.Web.Util;

namespace TechCalendar.Web.Handler.Event
{
    public class EventSubmissionHandler : ICommandHandler<EventSubmission>
    {
        private readonly string _mailgunDomain;
        private readonly string _mailgunApiKey;

        public EventSubmissionHandler(string mailgunDomain, string mailgunApiKey)
        {
            _mailgunDomain = mailgunDomain;
            _mailgunApiKey = mailgunApiKey;
        }

        public async Task HandleAsync(EventSubmission query)
        {
            try
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri ("https://api.mailgun.net/v3/");
                client.Authenticator = new HttpBasicAuthenticator("api", _mailgunApiKey);
                RestRequest request = new RestRequest ();
                request.AddParameter ("domain", _mailgunDomain, ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter ("from", $"stream-schedule.io <mailgun@{_mailgunDomain}>");
                request.AddParameter ("to", "adrian.kuper@hotmail.de");
                request.AddParameter ("subject", $"Event Submission - {query.Title}");

                string text = 
                    $"Title: {query.Title}"
                    + $"Start Date: {query.StartDate.ToString()}"
                    + $"Start Time: {query.StartTime.ToString()}"
                    + $"End Date: {query.EndDate.ToString()}"
                    + $"End Time: {query.EndTime.ToString()}"
                    + $"Stream URL: {query.StreamUrl}";
                
                request.AddParameter ("text", text);
                request.Method = Method.POST;

                await client.ExecuteTaskAsync(request);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Event-Submission failed!");
                System.Console.WriteLine(e);
            }
        }
    }
}