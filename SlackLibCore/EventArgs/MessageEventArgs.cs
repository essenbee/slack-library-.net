using System;

namespace SlackLibCore
{
    public class MessageEventArgs
    {
        //https://api.slack.com/events/message

        private Client _client;

        public string channel { get; }
        public string user { get; }
        public string text { get; }
        public TimeStamp ts { get; }
        public string team { get; }

        public MessageEventArgs(Client Client, dynamic Data)
        {
            _client = Client;
            channel = Data.channel;
            user = Data.user;
            text = Data.text;
            ts = new TimeStamp((String) Data.ts);
            team = Data.team;
        }

        public RTM.Channel ChannelInfo
        {
            get
            {
                foreach (RTM.Channel channel in _client.MetaData.channels)
                {
                    if (channel.id == this.channel)
                    {
                        return channel;
                    }
                }
                return null;
            }
        }

        public Ims IMSInfo
        {
            get
            {
                foreach (Ims ims in _client.MetaData.ims)
                {
                    if (ims.id == channel)
                    {
                        return ims;
                    }
                }
                return null;
            }
        }

        public RTM.User UserInfo
        {
            get
            {
                foreach (RTM.User user in _client.MetaData.users)
                {
                    if (user.id == this.user)
                    {
                        return user;
                    }
                }
                return null;
            }
        }
    }
}
