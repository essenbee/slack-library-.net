using System;

namespace Slack
{


    //https://api.slack.com/methods/auth.test


    public class AuthTestResponse
    {
        public AuthTestResponse(dynamic Response)
        {
            team = Response.team;
            team_id = Response.team_id;
            url = Response.url;
            user = Response.user;
            user_id = Response.user_id;
        }


        public String team { get; }


        public String team_id { get; }


        public String url { get; }


        public String user { get; }


        public String user_id { get; }
    }


}
