using System;

namespace SlackLibCore
{
    public class Previous_Message
    {
        public String type { get; }
        public String user { get; }
        public String text { get; }
        public String ts { get; }

        public Previous_Message(dynamic Data)
        {
            text = Data.text;
            ts = Data.ts;
            type = Data.type;
            user = Data.user;
        }
    }
}
