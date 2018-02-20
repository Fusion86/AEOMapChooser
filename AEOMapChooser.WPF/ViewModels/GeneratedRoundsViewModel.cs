using AEOMapChooser.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEOMapChooser.WPF.ViewModels
{
    public class GeneratedRoundsViewModel
    {
        public ObservableCollection<RoundViewModel> Rounds { get; } = new ObservableCollection<RoundViewModel>();

        public void SetRounds(Round[] rounds)
        {
            Rounds.Clear();

            for (int i = 0; i < rounds.Length; i++)
            {
                string name = "Round " + i;
                Rounds.Add(new RoundViewModel(name, rounds[i]));
            }
        }
    }
}
