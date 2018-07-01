using System;

namespace SlackLibCore
{


    public class ChannelDeletedEventArgs
    {


        //https://api.slack.com/events/channel_deleted


        private String _channel;


        public ChannelDeletedEventArgs(dynamic Data)
        {
            _channel = Data.channel;
        }


        public String channel
        {
            get
            {
                return _channel;
            }
        }


    }


}
