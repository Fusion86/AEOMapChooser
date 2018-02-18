using AEOMapChooser.Core.Enums;
using AEOMapChooser.WPF.Models;
using System.ComponentModel;

namespace AEOMapChooser.WPF.ViewModels
{
    internal class SelectableMapViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Map Map { get; }
        public SelectableMapType Type { get; }
        public bool IsSelected { get; set; }

        public SelectableMapViewModel(Map map, SelectableMapType type)
        {
            Map = map;
            Type = type;
        }
    }
}
