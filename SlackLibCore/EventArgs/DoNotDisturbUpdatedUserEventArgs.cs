using System;

namespace SlackLibCore
{


    public class DoNotDisturbUpdatedUserEventArgs
    {


        //https://api.slack.com/events/dnd_updated_user


        private Client _client;

        private String _user;
        private DoNotDisturbUserStatus _dnd_status;


        public DoNotDisturbUpdatedUserEventArgs(Client Client,dynamic Data)
        {
            _client = Client;
            _dnd_status = new DoNotDisturbUserStatus(Data.dnd_status);
            _user = Data.user;
        }


        public String user
        {
            get
            {
                return _user;
            }
        }


        public DoNotDisturbUserStatus dnd_status
        {
            get
            {
                return _dnd_status;
            }
        }


        public RTM.user UserInfo
        {
            get
            {
                foreach (RTM.user userItem in _client.MetaData.users)
                {
                    if (userItem.id == _user)
                    {
                        return userItem;
                    }
                }
                return null;
            }
        }

    
    }


}
