using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackLibCore
{


    //https://api.slack.com/events/file_change


    public class FileChangeEventArgs
    {


        private FileObject _file;


        public FileChangeEventArgs(dynamic Data)
        {
            _file = new FileObject(Data.file);
        }


        public FileObject file
        {
            get
            {
                return _file;
            }
        }

    
    }


}
