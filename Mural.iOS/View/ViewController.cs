using System;

using UIKit;
using CoreGraphics;
using System.Drawing;
using ReactiveUI;
using Foundation;
using Mural.Core;

namespace Mural.iOS
{
    public partial class ViewController : UIViewController
    {
        private MuralViewModel viewModel;

        private UIScrollView scrollview;
        private UIView muralView;
        private UIBarButtonItem shareButton;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.InitView();

            this.viewModel = new MuralViewModel();
            this.viewModel.Widgets.ItemsAdded.Subscribe(this.AddWidgetToMural);
            this.viewModel.ShareCommand.Subscribe(this.Share);
        }

        private void InitView()
        {
            this.scrollview = new UIScrollView(this.View.Frame);
            this.scrollview.BackgroundColor = UIColor.DarkGray;
            this.scrollview.ContentInset = new UIEdgeInsets(100, 100, 100, 100);
            this.scrollview.MinimumZoomScale = 0.1f;
            this.scrollview.MaximumZoomScale = 2;
            this.scrollview.ZoomScale = 1;
            this.View.AddSubview(this.scrollview);

            this.scrollview.AddGestureRecognizer(new UITapGestureRecognizer(this.OnMuralTapped)
            { 
                NumberOfTapsRequired = 1, 
                NumberOfTouchesRequired = 1 
            });

            this.muralView = new UIView(new CGRect(CGPoint.Empty, new CGSize(6000, 3000)))  { BackgroundColor = UIColor.LightGray };
            this.scrollview.ContentSize = this.muralView.Frame.Size;
            this.scrollview.ViewForZoomingInScrollView += sv => this.muralView;
            this.scrollview.AddSubview(this.muralView);

            this.shareButton = new UIBarButtonItem("Share", UIBarButtonItemStyle.Done, null);
            var navigationBar = new UINavigationBar(new CGRect(0, 0, this.View.Frame.Width, 60));
            var navigationItem = new UINavigationItem("Mural") { RightBarButtonItem = this.shareButton };
            navigationBar.PushNavigationItem(navigationItem, false);
            this.View.AddSubview(navigationBar); 

            this.shareButton.Clicked += this.OnShareButtonTapped;
        }

        private void OnMuralTapped(UITapGestureRecognizer tapGesture)
        {
            var position = tapGesture.LocationInView(this.muralView);
            this.viewModel.OnTapped(position.AsPointF()); 
        }

        private void OnShareButtonTapped(object sender, EventArgs e)
        {
            var shareMessage = "Sharing from iOS app";
            this.viewModel.ShareCommand.Execute(shareMessage);
        }

        private void AddWidgetToMural(WidgetViewModel widgetViewModel)
        {
            var widgetView = new WidgetView(widgetViewModel);
            this.muralView.Add(widgetView);
        }

        private void Share(object state)
        {
            var activityController = new UIActivityViewController(new NSObject[]{ new NSString(state.ToString()) }, null);
            this.PresentViewController(activityController, true, null);
        }
    }
}