using System;

namespace SlackLibCore.Messages
{
    public class Message : IMessage
    {
        public class Edited
        {
            public Edited(dynamic data)
            {
                ts = data?.ts ?? string.Empty;
                user = data?.user ?? string.Empty;
            }

            public string user { get; }
            public string ts { get; }
        }

        public Message(dynamic data)
        {
            edited = new Edited(data.message.edited);
            text = data.message.text;
            ts = new TimeStamp( (String)data.message.ts);
            user = data.message.user;
        }
        
        public String user { get; }  
        public String text { get; }
        public Edited edited { get; }
        public String Type => "message";
        public TimeStamp ts { get; }
    }
}
