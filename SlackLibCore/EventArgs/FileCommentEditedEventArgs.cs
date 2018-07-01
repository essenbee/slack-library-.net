using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SlackLibCore
{


    //https://api.slack.com/events/file_comment_edited


    public class FileCommentEditedEventArgs
    {


        private FileObject _file;
        private String _comment;


        public FileCommentEditedEventArgs(dynamic Data)
        {
            _file = new FileObject(Data.file);
            _comment = Data.comment;
        }


        public FileObject file
        {
            get
            {
                return _file;
            }
        }


        public String comment
        { 
            get
            {
                return _comment;
            }
        }


    }


}
