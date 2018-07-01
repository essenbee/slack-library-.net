using System;

namespace SlackLibCore
{
    public partial class Chat
    {
        public class PostMessageArguments
        {
            public struct attachment
            {
                public string type;
                public string value;
            }

            public string channel = "";
            public string text = "";
            public string username = "";
            public bool as_user = false;
            public string parse = "full";
            public int link_names = 1;
            public attachment[] attachments = null;
            public bool unfurl_links = true;
            public bool unfurl_media = true;
            public string icon_url = "";
            public string icon_emoji = "";

        }
    }
}
