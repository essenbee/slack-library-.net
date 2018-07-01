using System;

namespace SlackLibCore
{

    public partial class Chat
    {


        public class UpdateMessageArguments
        {

            public struct attachment
            {
                public String type;
                public String value;
            }

            public String channel = "";
            public TimeStamp ts = null;
            public String text = "";
            public String parse = "full";
            public Int32 link_names = 1;
            public attachment[] attachments = null;

        }


    }
}
