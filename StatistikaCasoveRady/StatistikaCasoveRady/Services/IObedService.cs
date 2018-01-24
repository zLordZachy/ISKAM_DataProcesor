using System.Collections.Generic;

namespace StatistikaCasoveRady.Services
{
    public interface IObedService
    {
        List<Obed> NactiObedy();
        List<Obed> NactiVlastniObedy(string path);
    }
}
