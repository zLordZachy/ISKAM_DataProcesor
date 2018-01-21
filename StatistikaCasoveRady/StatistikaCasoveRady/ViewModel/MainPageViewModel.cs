

using StatistikaCasoveRady.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StatistikaCasoveRady.ViewModel
{
    public class MainPageViewModel
    {
        public ObservableCollection<Obed> Obedy { get; set; }
        private readonly IObedService _obedService;

       public MainPageViewModel()
        {
            _obedService = new ObedService();
            Obedy = new ObservableCollection<Obed>();

            LoadObedy();
        }

        private void LoadObedy()
        {
            List<Obed> obedy = _obedService.NactiObedy();
            obedy.ForEach(x => Obedy.Add(x));
        }
    }
}
