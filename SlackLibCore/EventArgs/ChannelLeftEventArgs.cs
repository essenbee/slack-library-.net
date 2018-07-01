using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackLibCore
{


    public class ChannelLeftEventArgs
    {


        //https://api.slack.com/events/channel_left


        private string _channel;


        public ChannelLeftEventArgs(dynamic Data)
        {
            _channel = Data.channel;
        }


        public string channel
        {
            get
            {
                return _channel;
            }
        }


    }


}
