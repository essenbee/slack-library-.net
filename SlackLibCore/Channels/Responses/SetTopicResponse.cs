using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SlackLibCore.Channels
{


    //https://api.slack.com/methods/channels.setTopic


    public class SetTopicResponse
    {


        public String topic;


        public SetTopicResponse(dynamic Response)
        {
            topic = Utility.TryGetProperty(Response, "topic");
        }


    }


}
