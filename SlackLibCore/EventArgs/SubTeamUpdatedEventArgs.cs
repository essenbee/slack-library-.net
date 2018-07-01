using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackLibCore
{


    //https://api.slack.com/events/subteam_updated


    public class SubTeamUpdatedEventArgs
    {


        private SubTeam _subteam;


        public SubTeamUpdatedEventArgs(dynamic Data)
        {
            _subteam = new SubTeam(Data.subteam);
        }


        public SubTeam subteam
        {
            get
            {
                return _subteam;
            }
        }


    }


}
