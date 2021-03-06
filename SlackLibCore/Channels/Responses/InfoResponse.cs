﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackLibCore.Channels
{


    //https://api.slack.com/methods/channels.info


    public class InfoResponse
    {


        private RTM.Channel _channel;


        public InfoResponse(dynamic Response)
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
