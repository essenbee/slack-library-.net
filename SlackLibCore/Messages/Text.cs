using System;

namespace SlackLibCore.Messages
{
    public class Text : IMessage
    {
        private Client _client;
        public String Type { get; }
        public TimeStamp ts { get; }
        public String text { get; }
        public String User { get; }
        
        public Text(Client Client, dynamic Data)
        {
            _client = Client;
            Type = Utility.TryGetProperty(Data, "type");
            ts = new TimeStamp(Utility.TryGetProperty(Data, "ts", 0));
            User = Utility.TryGetProperty(Data, "user");

            if (User.Equals(string.Empty))
            {
                User = Utility.TryGetProperty(Data, "username");
            }

            text = Utility.TryGetProperty(Data, "text");
        }

        public RTM.User UserInfo
        {
            get
            {
                foreach (RTM.User user in _client.MetaData.users)
                {
                    if (user.id == User)
                    {
                        return user;
                    }
                }

                return null;
            }
        }
    }
}
