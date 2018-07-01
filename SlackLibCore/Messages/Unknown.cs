using System;

namespace SlackLibCore.Messages
{


    public class Unknown : IMessage
    {


        private String _type;
        private TimeStamp _ts;


        public Unknown(dynamic Data)
        {
            _type = Data.type;
            _ts = new TimeStamp((String)Data.message.ts);
        }


        public String Type
        {
            get
            {
                return _type;
            }
        }


        public TimeStamp ts
        {
            get
            {
                return _ts;
            }
        }


    }


}
