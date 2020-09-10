using System;
using System.IO;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xaminals.Droid.Dependancies;
using Xaminals.Interfaces;

[assembly: Dependency(typeof(FileHelper))]
namespace Xaminals.Droid.Dependancies
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
