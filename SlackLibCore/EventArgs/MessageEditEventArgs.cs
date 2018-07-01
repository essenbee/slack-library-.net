using System;
using SlackLibCore.Messages;

namespace SlackLibCore
{


    public class MessageEditEventArgs
    {


        //https://api.slack.com/events/message


        private Client _client;

        private Messages.Message _message;
        private String _subtype;
        private Boolean _hidden;
        private String _channel;
        private Previous_Message _previous_message;
        private String _event_ts;
        private TimeStamp _ts;


        public MessageEditEventArgs(Client Client, dynamic Data)
        {
            _client = Client;
            _channel = Data.channel;
            _event_ts = Data.event_ts;
            _hidden = Data.hidden;

            if (Utility.HasProperty(Data, "message"))
            {
                    _message = new Message(Data);
            }
            _previous_message = new Previous_Message(Data.previous_message);
            _subtype = Data.subtype;
            _ts = new TimeStamp((String)Data.ts);
        }


        public Messages.Message message
        {
            get
            {
                return _message;
            }
        }


        public String subtype
        {
            get
            {
                return _subtype;
            }
        }


        public Boolean hidden
        {
            get
            {
                return _hidden;
            }
        }


        public String channel
        {
            get
            {
                return _channel;
            }
        }


        public Previous_Message previous_message
        {
            get
            {
                return _previous_message;
            }
        }


        public String event_ts
        {
            get
            {
                return _event_ts;
            }
        }


        public TimeStamp ts
        {
            get
            {
                return _ts;
            }
        }


        public RTM.channel ChannelInfo
        {
            get
            {
                foreach (RTM.channel channel in _client.MetaData.channels)
                {
                    if (channel.id == _channel)
                    {
                        return channel;
                    }
                }
                return null;
            }
        }


        public ims IMSInfo
        {
            get
            {
                foreach (ims ims in _client.MetaData.ims)
                {
                    if (ims.id == _channel)
                    {
                        return ims;
                    }
                }
                return null;
            }
        }


        public RTM.user UserInfo
        {
            get
            {
                foreach (RTM.user user in _client.MetaData.users)
                {
                    if (user.id == _previous_message.user)
                    {
                        return user;
                    }
                }
                return null;
            }
        }


    }


}
