using System;

namespace SlackLibCore
{
    public partial class Chat
    {


        //https://api.slack.com/methods/chat.update


        public class UpdateMessageResponse
        {


            public String channel;
            public TimeStamp ts;
            public String text;


            public UpdateMessageResponse(dynamic Response)
            {
                channel = Utility.TryGetProperty(Response, "channel");
                ts = new TimeStamp(Utility.TryGetProperty(Response, "ts"));
                text = Utility.TryGetProperty(Response, "text");
            }


        }


    }
}