using System;
using SQLite;

namespace Xaminals.Interfaces
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
