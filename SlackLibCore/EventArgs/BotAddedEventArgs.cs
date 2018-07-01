namespace SlackLibCore
{


    //https://api.slack.com/events/bot_added


    public class BotAddedEventArgs
    {
        public BotAddedEventArgs(dynamic Data)
        {
            bot = new Bot(Data);
        }


        public Bot bot { get; }


    }


}
