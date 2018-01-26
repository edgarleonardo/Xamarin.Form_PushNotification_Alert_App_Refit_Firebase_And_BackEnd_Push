using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.MessagePush.Interfaces
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
