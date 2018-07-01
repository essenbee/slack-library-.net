using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackLibCore
{


    //https://api.slack.com/events/team_profile_reorder


    public class TeamProfileReorderEventArgs
    {


        private dynamic _profile;


        public TeamProfileReorderEventArgs(dynamic Data)
        {
            _profile = Data.profile;
        }


        public dynamic profile
        {
            get
            {
                return _profile;
            }
        }


    }


}
