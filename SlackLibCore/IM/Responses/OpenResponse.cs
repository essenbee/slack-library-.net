using System;

namespace SlackLibCore
{
    public partial class IM
    {
        public class OpenResponse
        {
            public String ChannelID { get; } = "";

            public OpenResponse(dynamic Response)
            {
                if (!Utility.HasProperty(Response, "channel"))
                {
                    return;
                }
                ChannelID = Utility.TryGetProperty(Response.channel, "id");
            }
        }
    }
}