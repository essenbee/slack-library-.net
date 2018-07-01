using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackLibCore
{


    public class MessageEventArgs
    {


        //https://api.slack.com/events/message


        private Client _client;

        private String _channel;
        private String _user;
        private String _text;
        private TimeStamp _ts;
        private String _team;


        public MessageEventArgs(Client Client, dynamic Data)
        {
            _client = Client;
            _channel = Data.channel;
            _user = Data.user;
            _text = Data.text;
            _ts = new TimeStamp((String) Data.ts);
            _team = Data.team;
        }


        public String channel
        {
            get
            {
                return _channel;
            }
        }


        public String user
        {
            get
            {
                return _user;   
            }
        }


        public String text
        {
            get
            {
                return _text;
            }
        }


        public TimeStamp ts
        {
            get
            {
                return _ts;
            }
        }


        public String team
        {
            get
            {
                return _team;
            }
        }


        public RTM.Channel ChannelInfo
        {
            get
            {
                foreach (RTM.Channel channel in _client.MetaData.channels)
                {
                    if (channel.id == _channel)
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
                    if (ims.id == _channel)
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
                    if (user.id == _user)
                    {
                        return user;
                    }
                }
                return null;
            }
        }


    }


}
