using SlackLibCore.Messages;
using System;
using System.Collections.Generic;

namespace SlackLibCore.Channels
{


    //https://api.slack.com/methods/channels.history


    public class HistoryResponse
    {


        private Client _client;
        private Channels.HistoryRequestArgs _args;
        private TimeStamp _latest;
        private List<Messages.IMessage> _messages;
        private Boolean _hasMore;


        public HistoryResponse(Client Client, Channels.HistoryRequestArgs args, dynamic Response)
        {
            _client = Client;
            _args = args;
            _latest = new TimeStamp(Utility.TryGetProperty(Response, "latest", 0).ToString());
            _hasMore = Utility.TryGetProperty(Response, "has_more", false);
            _messages = new List<IMessage>();
            String strType;
            foreach (dynamic message in Response.messages)
            {
                strType = message.type;
                switch (strType)
                {
                    case "message":
                        _messages.Add(new Messages.Text(_client, message));
                        break;
                    default:
                        _messages.Add(new Unknown(message));
                        break;
                }
            }
        }


        public HistoryResponse NextPage()
        {
            if (!_hasMore)
            {
                return this;
            }
            _args.latest = _messages[_messages.Count - 1].ts;
            return _client.Channels.History(_args);
        }


        public TimeStamp latest
        {
            get
            {
                return _latest;
            }
        }


        public List<IMessage> Messages
        {
            get
            {
                return _messages;
            }
        }


        public Boolean HasMore
        {
            get
            {
                return _hasMore;
            }
        }


    }


}
