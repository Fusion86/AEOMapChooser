using AEOMapChooser.Core.Enums;
using AEOMapChooser.WPF.Enums;
using AEOMapChooser.WPF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace AEOMapChooser.WPF
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<SelectableMapViewModel> SelectableMaps { get; } = new ObservableCollection<SelectableMapViewModel>();
        public ObservableCollection<RoundViewModel> GeneratedRounds { get; } = new ObservableCollection<RoundViewModel>();

        public int NumberOfRounds { get; set; } = 3;
        public int NumberOfMatchesPerRound { get; set; } = 3;

        public MainWindowViewModel()
        {
            List<SelectableMapViewModel> tiebreakers = new List<SelectableMapViewModel>();

            foreach (Map map in Map.GetAll<Map>().OrderBy(x => x.Type))
            {
                SelectableMaps.Add(new SelectableMapViewModel(
                    map,
                    (SelectableMapType)map.Type,
                    map.Type != MapType.Arcade) // Disable Arcade maps by default
                );

                // Also add control maps as (additional) tiebreaker maps
                if (map.Type == MapType.Control)
                    tiebreakers.Add(new SelectableMapViewModel(map, SelectableMapType.Tiebreaker));
            }

            // We add the tiebreakers later to keep our list nicely ordered A-Z with tiebreakers at the end
            foreach (var map in tiebreakers)
                SelectableMaps.Add(map);
        }
    }
}
