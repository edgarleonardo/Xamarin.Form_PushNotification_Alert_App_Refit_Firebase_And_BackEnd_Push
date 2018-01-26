using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Firebase.MessagePush.Interfaces;
using Firebase.MessagePush.iOS.nativeHelpers;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Firebase.MessagePush.iOS.nativeHelpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}