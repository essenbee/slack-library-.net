using System;

namespace SlackLibCore
{

    public partial class Chat
    {


        public class PostMessageArguments
        {

            public struct attachment
            {
                public String type;
                public String value;
            }

            public String channel = "";
            public String text = "";
            public String username = "";
            public Boolean as_user = false;
            public String parse = "full";
            public Int32 link_names = 1;
            public attachment[] attachments = null;
            public Boolean unfurl_links = true;
            public Boolean unfurl_media = true;
            public String icon_url = "";
            public String icon_emoji = "";

        }


    }
}
