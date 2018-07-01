using System;

namespace SlackLibCore.Messages
{
    public class Unknown : IMessage
    {
        public String Type { get; }
        public TimeStamp ts { get; }


        public Unknown(dynamic Data)
        {
            Type = Data.type;
            ts = new TimeStamp((String)Data.message.ts);
        }
    }
}
