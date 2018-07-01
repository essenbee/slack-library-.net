using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SlackLibCore.Channels
{


    //https://api.slack.com/methods/channels.list


    public class ListResponse
    {


        private List<RTM.Channel> _channels;


        public ListResponse(RTM.MetaData MetaData, dynamic Response)
        {
            _channels = new List<RTM.Channel>();
            if (Utility.HasProperty(Response, "channels"))
            {
                RTM.Channel rtmChannel;
                foreach (dynamic channel in Response.channels)
                {
                    rtmChannel = new RTM.Channel(MetaData);
                    rtmChannel.id = Utility.TryGetProperty(channel, "id");
                    rtmChannel.name = Utility.TryGetProperty(channel, "name");
                    rtmChannel.created = new TimeStamp(Utility.TryGetProperty(channel, "created", 0));
                    rtmChannel.creator = Utility.TryGetProperty(channel, "creator");
                    rtmChannel.is_archived = Utility.TryGetProperty(channel, "is_archived", false);
                    rtmChannel.is_member = Utility.TryGetProperty(channel, "is_member", false);
                    _channels.Add(rtmChannel);
                }
            }
        }


        public List<RTM.Channel> channels
        {
            get
            {
                return _channels;
            }
        }


    }


}
