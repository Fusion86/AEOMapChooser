using AEOMapChooser.Core.Enums;
using AEOMapChooser.Core.Models;

namespace AEOMapChooser.WPF.ViewModels
{
    public class RoundViewModel
    {
        public string Name { get; }
        public Map[] Maps { get; }
        public Map Tiebreaker { get; }

        public RoundViewModel(string name, Round round)
        {
            Name = name;
            Maps = round.Maps;
            Tiebreaker = round.Tiebreaker;
        }
    }
}
