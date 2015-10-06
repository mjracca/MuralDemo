using Mural.Core;
using ReactiveUI;
using System;
using System.Collections.Generic;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App6
{
    public sealed partial class WidgetView : UserControl
    {
        private WidgetViewModel widgetViewModel;

        public WidgetView(WidgetViewModel widgetViewModel)
        {
            this.InitializeComponent();

            this.widgetViewModel = widgetViewModel;

            this.widgetViewModel.WhenAnyValue(w => w.Position).Subscribe(p => 
            {
                this.RenderTransform = new TranslateTransform()
                {
                    X = p.X,
                    Y = p.Y
                };
            });

            this.DataContext = this.widgetViewModel;
        }

        protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs e)
        {
            var position = this.widgetViewModel.Position;
            var translation = e.Delta.Translation.AsPointF();
            this.widgetViewModel.Position = position.Add(translation);
        }
    }
}
