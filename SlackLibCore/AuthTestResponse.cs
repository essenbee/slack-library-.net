using System;

namespace SlackLibCore
{
    //https://api.slack.com/methods/auth.test

    public class AuthTestResponse
    {
        public string team { get; }
        public string team_id { get; }
        public string url { get; }
        public string user { get; }
        public string user_id { get; }

        public AuthTestResponse(dynamic Response)
        {
            team = Response.team;
            team_id = Response.team_id;
            url = Response.url;
            user = Response.user;
            user_id = Response.user_id;
        }
    }
}
