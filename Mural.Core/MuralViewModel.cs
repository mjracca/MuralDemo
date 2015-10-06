using System;
using ReactiveUI;
using System.Drawing;

namespace Mural.Core
{
    public class MuralViewModel : ReactiveObject
    {
        public ReactiveList<WidgetViewModel> Widgets { set; get; }

        public ReactiveCommand<object> ShareCommand { set; get; }

        public MuralViewModel()
        {
            this.Widgets = new ReactiveList<WidgetViewModel>();

            this.ShareCommand = ReactiveCommand.Create();
        }

        public void OnTapped(PointF position)
        {
            this.Widgets.Add(new WidgetViewModel()
            {
                Id = this.Widgets.Count,
                Position = position
            });
        }
    }
}