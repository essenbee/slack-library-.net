using System;
using Newtonsoft.Json.Linq;

namespace SlackLibCore
{
    public partial class IM
    {
        private Client _client;

        public IM(Client Client)
        {
            _client = Client;
        }

        public OpenResponse Open(String strUserID)
        {
            //https://api.slack.com/methods/im.open

            dynamic Response;

            try
            {
                var strURL =
                    "https://slack.com/api/im.open?token=" + _client.APIKey +
                    "&user=" + System.Web.HttpUtility.UrlEncode(strUserID);
                var strResponse = _client.APIRequest(strURL);
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not list channels.", ex);
            }
            _client.CheckForError(Response);

            return new OpenResponse(Response);
        }

        public ListResponse List()
        {
            //https://api.slack.com/methods/im.list
            dynamic Response;
            try
            {
                var strResponse = _client.APIRequest("https://slack.com/api/im.list?token=" + _client.APIKey);
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not list channels.", ex);
            }
            _client.CheckForError(Response);

            return new ListResponse(_client, Response);
        }

        public bool Close(String channel)
        {
            //https://api.slack.com/methods/im.close

            dynamic Response;

            try
            {
                String strURL =
                    "https://slack.com/api/im.close?token=" + _client.APIKey +
                    "&channel=" + System.Web.HttpUtility.UrlEncode(channel);
                var strResponse = _client.APIRequest(strURL);
                Response = JObject.Parse(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not list channels.", ex);
            }
            _client.CheckForError(Response);

            return Utility.TryGetProperty(Response, "ok", false);
        }
    }
}
