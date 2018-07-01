using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SlackLibCore.Channels
{


    //https://api.slack.com/methods/channels.rename


    public class RenameResponse
    {


        private RTM.Channel _channel;


        public RenameResponse(dynamic Response)
        {
            if (Utility.HasProperty(Response, "channel"))
            {
                _channel = new RTM.Channel(Response.channel);
            }
            else
            {
                _channel = new RTM.Channel();
            }
        }


        public RTM.Channel channel
        {
            get
            {
                return _channel;
            }
        }


    }


}
