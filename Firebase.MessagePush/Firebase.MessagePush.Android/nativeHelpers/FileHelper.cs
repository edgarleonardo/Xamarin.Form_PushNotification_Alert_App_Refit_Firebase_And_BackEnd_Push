
using System.IO;
using Firebase.MessagePush.Droid.nativeHelpers;
using Firebase.MessagePush.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Firebase.MessagePush.Droid.nativeHelpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}