using System;

namespace Slack.Messages
{

    public interface IMessage
    {


        String Type
        {
            get;
        }


        Slack.TimeStamp ts
        {
            get;
        }


    }


}
