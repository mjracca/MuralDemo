using Mural.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App6
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MuralViewModel viewModel;

        public MainPage()
        {
            this.InitializeComponent();

            this.viewModel = new MuralViewModel();
            this.viewModel.Widgets.ItemsAdded.Subscribe(widgetViewModel =>
            {
                this.mural.Children.Add(new WidgetView(widgetViewModel));
            });

            this.mural.Tapped += OnMuralTap;
        }

        private void OnMuralTap(object sender, TappedRoutedEventArgs e)
        {
            var pos = e.GetPosition(this.mural);
            this.viewModel.TappedCommand.Execute(pos.AsPointF());
        }
    }


    public static class PointExtensions
    {
        public static PointF AsPointF(this Windows.Foundation.Point point)
        {
            return new PointF()
            {
                X = (float)point.X,
                Y = (float)point.Y
            };
        }

        public static PointF Add(this PointF self, PointF value)
        {
            return new PointF()
            {
                X = self.X + value.X,
                Y = self.Y + value.Y
            };
        }
    }
}
