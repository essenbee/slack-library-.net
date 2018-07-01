using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SlackLibCore.Channels
{


    //https://api.slack.com/methods/channels.join


    public class JoinResponse
    {


        private RTM.Channel _channel;


        public JoinResponse(dynamic Response)
        {
            _channel = new RTM.Channel();
            _channel.id = Response.channel.id;
            _channel.name = Response.channel.name;
            _channel.created = Response.channel.created;
            _channel.creator = Response.channel.creator;
            _channel.is_archived = Response.channel.is_archived;
            _channel.is_member = Response.channel.is_member;
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
