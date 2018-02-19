using AEOMapChooser.Core.Enums;
using AEOMapChooser.Core.Helpers;
using AEOMapChooser.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvSelectableMaps.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Type");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void btnBreak_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debugger.Break();
        }

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

        #endregion

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            var maps = vm.SelectableMaps.Where(x => x.IsSelected).Select(x => x.Map);

            var rounds = MapChooser.GetRounds(
                maps,
                vm.NumberOfRounds,
                vm.NumberOfMatchesPerRound,
                true,
                true
                );

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < rounds.Count(); i++)
            {
                sb.AppendLine($"Round {i}");

                foreach(var map in rounds[i].Maps)
                {
                    sb.AppendLine($"{map.Type} - {map.Name}");
                }

                sb.AppendLine();
            }

            txtOutput.Text = sb.ToString();
        }
    }
}
