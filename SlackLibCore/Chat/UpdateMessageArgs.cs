namespace SlackLibCore
{
    public partial class Chat
    {
        public class UpdateMessageArguments
        {
            public struct attachment
            {
                public string type;
                public string value;
            }

            public string channel = "";
            public TimeStamp ts = null;
            public string text = "";
            public string parse = "full";
            public int link_names = 1;
            public attachment[] attachments = null;
        }
    }
}
