namespace SlackLibCore
{
    public partial class Chat
    {
        //https://api.slack.com/methods/chat.postMessage

        public class PostMessageResponse
        {
            public string channel;
            public TimeStamp ts;
            public Messages.Text message;

            public PostMessageResponse(Client client, dynamic Response)
            {
                channel = Utility.TryGetProperty(Response, "channel");
                ts = new TimeStamp(Utility.TryGetProperty(Response, "ts"));
                message = new Messages.Text(client, Response.message);
            }
        }
    }
}