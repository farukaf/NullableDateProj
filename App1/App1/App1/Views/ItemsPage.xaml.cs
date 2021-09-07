using App1.Models;
using App1.ViewModels;
using App1.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

                throw;
            }

            BindingContext = viewModel = new ItemsViewModel();
        }
        System.Timers.Timer myTimer;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
            await viewModel.ExecuteLoadItemsCommand();

            myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new ElapsedEventHandler(ItemsListView_BindingContextChanged);
            myTimer.Interval = 500;
            myTimer.Enabled = true;
            myTimer.Start();

        }

        private void TapTabStatus_Tapped(object sender, EventArgs e)
        {
            var grd = sender as Grid;
            var ctx = grd?.BindingContext;

            if (ctx != null && ctx is TabViewModel tabViewModel && !tabViewModel.IsSelected)
            {
                foreach (var item in viewModel.ListStatusTabs)
                {
                    item.IsSelected = false;
                }
                tabViewModel.IsSelected = true;
                viewModel.SelectedTab = tabViewModel;

                viewModel.OnPropertyChanged("ListStatusTabs");
                viewModel.OnPropertyChanged("SelectedTab");
            }
        }

        private void Right_Swiped(object sender, SwipedEventArgs e)
        {
            var indexSel = viewModel.ListStatusTabs.IndexOf(viewModel.SelectedTab);
            indexSel--;

            if (indexSel < 0)
            {
                return;
            }

            foreach (var item in viewModel.ListStatusTabs)
            {
                item.IsSelected = false;
            }

            viewModel.ListStatusTabs[indexSel].IsSelected = true;
            viewModel.SelectedTab = viewModel.ListStatusTabs[indexSel];

            viewModel.OnPropertyChanged("ListStatusTabs");
            viewModel.OnPropertyChanged("SelectedTab");
        }

        private void Left_Swiped(object sender, SwipedEventArgs e)
        {
            var indexSel = viewModel.ListStatusTabs.IndexOf(viewModel.SelectedTab);
            indexSel++;

            if (indexSel >= viewModel.ListStatusTabs.Count)
            {
                return;
            }

            foreach (var item in viewModel.ListStatusTabs)
            {
                item.IsSelected = false;
            }

            viewModel.ListStatusTabs[indexSel].IsSelected = true;
            viewModel.SelectedTab = viewModel.ListStatusTabs[indexSel];

            viewModel.OnPropertyChanged("ListStatusTabs");
            viewModel.OnPropertyChanged("SelectedTab");
        }

        private void ItemsListView_BindingContextChanged(object source, ElapsedEventArgs e)
        {
            Debug.WriteLine($"ItemsListView.Height: {ItemsListView.Height}");
            Debug.WriteLine($"ItemsListView InternalHeight: {viewModel.Items.Count * ItemsListView.RowHeight}");

            var aux = ItemsListView.Height - 20 - viewModel.Items.Count * ItemsListView.RowHeight;

            if (aux < 0) aux = 0;
            Debug.WriteLine($"ListViewFiller.HeightRequest: {aux}");
            Device.BeginInvokeOnMainThread(() =>
            {
                ListViewFiller.HeightRequest = aux; ;
            });

        }
    }
}