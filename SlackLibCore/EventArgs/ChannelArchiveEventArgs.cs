using System;

namespace SlackLibCore
{


    public class ChannelArchiveEventArgs
    {


        //https://api.slack.com/events/channel_archive


        private String _channel;
        private String _user;


        public ChannelArchiveEventArgs(dynamic Data)
        {
            _channel = Data.channel;
            _user = Data.user;
        }

        
        public String channel
        {
            get
            {
                return channel;
            }
        }


        public String user
        {
            get
            {
                return _user;
            }
        }


    }


}
