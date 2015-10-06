using System;
using ReactiveUI;
using System.Drawing;

namespace Mural.Core
{
    public class WidgetViewModel : ReactiveObject
    {
        private int _Id;

        public int Id
        {
            get { return _Id; }
            set { this.RaiseAndSetIfChanged(ref _Id, value); }
        }

        private PointF _Position;

        public PointF Position
        {
            get { return _Position; }
            set { this.RaiseAndSetIfChanged(ref _Position, value); }
        }

        public WidgetViewModel()
        {
        }
    }
}