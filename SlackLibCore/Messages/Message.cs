using System;

namespace Slack.Messages
{
    public class Message : IMessage
    {
        public class Edited
        {
            public Edited(dynamic data)
            {
                ts = data.ts;
                user = data.user;
            }


            public String user { get; }


            public String ts { get; }
        }

        public Message(dynamic data)
        {
            edited = new Edited(data.message.edited);
            text = data.message.text;
            ts = new Slack.TimeStamp( (String)data.message.ts);
            user = data.message.user;
        }
        
        public String user { get; }
        
        public String text { get; }
        
        public Edited edited { get; }
        
        public String Type => "message";
        
        public Slack.TimeStamp ts { get; }
    }


}
