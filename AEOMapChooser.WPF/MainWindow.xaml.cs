using AEOMapChooser.Core.Enums;
using AEOMapChooser.Core.Extensions;
using AEOMapChooser.Core.Helpers;
using AEOMapChooser.Core.Models;
using AEOMapChooser.WPF.Enums;
using AEOMapChooser.WPF.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace AEOMapChooser.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CollectionView lvSelectableMapsView = (CollectionView)CollectionViewSource.GetDefaultView(lvSelectableMaps.ItemsSource);
            PropertyGroupDescription selectableMapsGroupDescription = new PropertyGroupDescription("Type");
            lvSelectableMapsView.GroupDescriptions.Add(selectableMapsGroupDescription);

            CollectionView lvGeneratedRoundsView = (CollectionView)CollectionViewSource.GetDefaultView(lvGeneratedRounds.ItemsSource);
            PropertyGroupDescription generatedRoundsGroupDescription = new PropertyGroupDescription("Name");
            lvGeneratedRoundsView.GroupDescriptions.Add(generatedRoundsGroupDescription);
        }

        #region Commands

        private void cmdGenerateRounds_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = vm.SelectableMaps.Where(x => x.IsSelected).Count() > 1;
        }

        private void cmdGenerateRounds_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var maps = vm.SelectableMaps.Where(x => x.IsSelected && x.Type != SelectableMapType.Tiebreaker).Select(x => x.Map);

            var rounds = MapChooser.GetRounds(
                maps,
                vm.NumberOfRounds,
                vm.NumberOfMatchesPerRound,
                true
                );

            vm.GeneratedRounds.Clear();

            for (int i = 0; i < rounds.Count; i++)
            {
                string name = "Round " + (i + 1);
                vm.GeneratedRounds.Add(new RoundViewModel(name, rounds[i]));
            }

            var tiebreakers = vm.SelectableMaps.Where(x => x.IsSelected && x.Type == SelectableMapType.Tiebreaker).Select(x => x.Map);
            if (tiebreakers.Count() > 0)
                vm.GeneratedRounds.Add(new RoundViewModel("Tiebreaker", new Round() { Maps = new[] { tiebreakers.Random() } }));
        }

        private void cmdCopyRoundsAsMarkdown_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = vm.GeneratedRounds.Count > 0;
        }

        private void cmdCopyRoundsAsMarkdown_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            for (int roundNr = 0; roundNr < vm.GeneratedRounds.Count; roundNr++)
            {
                RoundViewModel round = vm.GeneratedRounds[roundNr];
                sb.AppendLine($"**{round.Name}**");

                for (int mapNr = 0; mapNr < vm.GeneratedRounds[roundNr].Maps.Length; mapNr++)
                {
                    Map map = round.Maps[mapNr];
                    sb.Append($"| *{map.Type}:* ");
                    sb.AppendLine(map.Name);
                }
                sb.AppendLine();
            }

            Clipboard.SetText(sb.ToString());
        }

        #endregion

        #region Selectable Maps Group Header

        private IEnumerable<SelectableMapViewModel> selectableMapsGroupHeader_GetAllInGroup(object sender)
            => vm.SelectableMaps.Where(x => x.Type.ToString() == ((Control)sender).Tag.ToString());

        private void lvSelectableMapsGroupLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (SelectableMapViewModel map in selectableMapsGroupHeader_GetAllInGroup(sender))
                map.IsSelected = !map.IsSelected;
        }

        private void lvSelectableMapsGroupLabel_EnableAll(object sender, RoutedEventArgs e)
        {
            foreach (SelectableMapViewModel map in selectableMapsGroupHeader_GetAllInGroup(sender))
                map.IsSelected = true;
        }

        private void lvSelectableMapsGroupLabel_DisableAll(object sender, RoutedEventArgs e)
        {
            foreach (SelectableMapViewModel map in selectableMapsGroupHeader_GetAllInGroup(sender))
                map.IsSelected = false;
        }

        #endregion Selectable Maps Group Header

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            cmdGenerateRounds_Executed(sender, null);
        }

        private void generatorInput_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            cmdGenerateRounds_Executed(sender, null);
        }
    }
}
