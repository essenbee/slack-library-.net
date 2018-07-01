using System;

namespace SlackLibCore
{


    public class UserTypingEventArgs
    {


        //https://api.slack.com/events/user_typing


        private Client _client;

        private String _channel;
        private String _user;


        public UserTypingEventArgs(Client Client, dynamic Data)
        {
            _client = Client;
            _channel = Data.channel;
            _user = Data.user;
        }


        public String channel
        {
            get
            {
                return _channel;
            }
        }


        public Ims IMSInfo
        {
            get
            {
                foreach (Ims ims in _client.MetaData.ims)
                {
                    if (ims.id == _channel)
                    {
                        return ims;
                    }
                }
                return null;
            }
        }


        public String user
        {
            get
            {
                return _user;
            }
        }


        public RTM.User UserInfo
        {
            get
            {
                foreach(RTM.User user in  _client.MetaData.users)
                {
                    if (user.id == _user)
                    {
                        return user;
                    }
                }
                return null;
            }
        }


    }


}
