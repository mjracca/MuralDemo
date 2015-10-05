using System;
using UIKit;
using Mural.Core;
using CoreGraphics;
using ReactiveUI;
using System.Drawing;

namespace Mural.iOS
{
    public class WidgetView : UILabel
    {
        private WidgetViewModel widgetViewModel;

        public WidgetView(WidgetViewModel widgetViewModel)
        {
            this.InitView();

            this.widgetViewModel = widgetViewModel;
            this.widgetViewModel.WhenAnyValue(w => w.Id).Subscribe(this.OnIdChanged);
            this.widgetViewModel.WhenAnyValue(w => w.Position).Subscribe(this.OnPositionChanged);
        }

        private void InitView()
        {
            this.Frame = new CGRect(0, 0, 140, 140);
            this.BackgroundColor = UIColor.Yellow;
            this.TextAlignment = UITextAlignment.Center;
            this.UserInteractionEnabled = true;

            this.AddGestureRecognizer(new UIPanGestureRecognizer(this.OnTapped)
            { 
                MinimumNumberOfTouches = 1, 
                MaximumNumberOfTouches = 1 
            });
        }

        private void OnTapped(UIPanGestureRecognizer panGesture)
        {
            var position = panGesture.LocationInView(this.Superview);
            this.widgetViewModel.Position = position.AsPointF();
        }

        private void OnIdChanged(int id)
        {
            this.Text = id.ToString(); 
        }

        private void OnPositionChanged(PointF position)
        {
            this.Frame = new CGRect(position.AsCGPoint(), this.Frame.Size);
        }
    }
}