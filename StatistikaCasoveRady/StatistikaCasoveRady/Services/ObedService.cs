using StatistikaCasoveRady.Bussines;
using System.Collections.Generic;

namespace StatistikaCasoveRady.Services
{
    public class ObedService : IObedService
    {
        private readonly IFileManager _fileManager;

        public ObedService()
        {
            _fileManager = new FileManager();
        }

        public List<Obed> NactiObedy()
        {
            string[] files = _fileManager.GetAllFiles();
            List<Obed> obedy = new List<Obed>();

            foreach (var file in files)
            {
                if (file.Length > 4 && file.Substring(file.Length - 4) == "xlsx")
                {
                    List<Obed> noveObdy = _fileManager.ReadFile(file);
                    noveObdy.ForEach(x => obedy.Add(x));   
                }
            }
           
            return obedy;
        }

        public List<Obed> NactiVlastniObedy(string path)
        {

            List<Obed> obedy = new List<Obed>();
            if (path.Length > 4 && path.Substring(path.Length - 4) == "xlsx")
            {
                List<Obed> noveObdy = _fileManager.ReadFile(path);
                noveObdy.ForEach(x => obedy.Add(x));
            }
            return obedy;
        }
    }
}
