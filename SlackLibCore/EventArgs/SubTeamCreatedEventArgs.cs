namespace SlackLibCore
{


    //https://api.slack.com/events/subteam_created


    public class SubTeamCreatedEventArgs
    {


        private SubTeam _subteam;


        public SubTeamCreatedEventArgs(dynamic Data)
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
