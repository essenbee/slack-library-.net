namespace SlackLibCore
{
    public class Ims
    {
        private RTM.MetaData _metaData;

        public TimeStamp created;
        public bool has_pins;
        public string id;
        public bool is_im;
        public bool is_open;
        public string last_read;
        public int unread_count;
        public int unread_count_display;
        public string user;

        public Ims(RTM.MetaData MetaData)
        {
            _metaData = MetaData;
        }

        public TimeStamp CreatedDate
        {
            get
            {
                return created;
            }
        }

        public RTM.User UserInfo
        {
            get
            {
                foreach (RTM.User userItem in _metaData.users)
                {
                    if (userItem.id == user)
                    {
                        return userItem;
                    }
                }
                return null;
            }
        }
    }
}