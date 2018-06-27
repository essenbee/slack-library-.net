using System;

namespace Slack
{


    //https://api.slack.com/events/bot_added


    public class Bot
    {
        public Bot(dynamic Data)
        {
            id = Data.id;
            name = Data.name;
            icons = new Icons(Data.icons);
        }


        public String id { get; }


        public String name { get; }


        public Icons icons { get; }
    }


}
