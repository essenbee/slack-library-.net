using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackLibCore
{


    public class FileSharedEventArgs
    {


        //https://api.slack.com/events/file_shared


        public FileObject _file;


        public FileSharedEventArgs(dynamic Data)
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
