
using System.Collections.Generic;

namespace StatistikaCasoveRady.Bussines
{
    public interface IFileManager
    {
        List<Obed> NactiObedy();
        string[] GetAllFiles();
        List<Obed> ReadFile(string file);
    }
}
