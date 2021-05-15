using System.Windows.Controls;
using Domain.Models;
using WPF.ViewModels;

namespace WPF.Views
{
    /// <summary>
    ///     Interaction logic for ItemsViewModel.xaml
    /// </summary>
    public partial class ItemsView : UserControl
    {
        public ItemsView()
        {
            InitializeComponent();
        }

        private void Shares_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as ItemsViewModel;
            var combobox = sender as ComboBox;
            var share = combobox.SelectedItem as Share;
            viewModel.LoadItemsCommand.Execute(share.Id.ToString());
        }
    }
}