using Mural.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Mural.Win
{
    public sealed partial class MainPage : Page
    {
        private MuralViewModel viewModel;

        public MainPage()
        {
            this.InitializeComponent();

            this.viewModel = new MuralViewModel();
            this.viewModel.Widgets.ItemsAdded.Subscribe(this.AddWidgetToMural);
            this.viewModel.ShareCommand.Subscribe(this.Share);

            this.mural.Tapped += OnMuralTap;

            this.DataContext = this.viewModel;
        }

        private void OnMuralTap(object sender, TappedRoutedEventArgs e)
        {
            var position = e.GetPosition(this.mural);
            this.viewModel.OnTapped(position.AsPointF());
        }

        private void AddWidgetToMural(WidgetViewModel widgetViewModel)
        {
            var widgetView = new WidgetView(widgetViewModel);
            this.mural.Children.Add(widgetView);
        }

        private void Share(object state)
        {
            DataTransferManager.ShowShareUI();
        }
    }
}
