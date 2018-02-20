using AEOMapChooser.Core.Enums;
using AEOMapChooser.WPF.Models;
using AEOMapChooser.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
            foreach (Map map in Map.GetAll<Map>())
            {
                SelectableMaps.Add(new SelectableMapViewModel(map, (SelectableMapType)map.Type, true));

                // Also add control maps as (additional) tiebreaker maps
                if (map.Type == MapType.Control)
                    SelectableMaps.Add(new SelectableMapViewModel(map, SelectableMapType.Tiebreaker));
            }
        }
    }
}
