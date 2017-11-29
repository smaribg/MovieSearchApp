using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;
namespace MovieSearch.iOS
{
    public class MovieSearchController: UIViewController
    {
        
        private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;
        private IMovieApi api;
        private List<string> _movieNames;

        public MovieSearchController(IMovieApi movieApi){
            api = movieApi;

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "MovieSearch";
            var loading = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray)
            {
                Frame = new CGRect(this.View.Bounds.Width / 2, this.View.Bounds.Height / 2, 10, 10)

            };
            this.View.BackgroundColor = UIColor.White;
            var promptLabel = new UILabel()
            {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter a movie title: "
            };
            this.View.AddSubview(promptLabel);
            var nameField = new UITextField()
            {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };
            this.View.AddSubview(nameField);
            var greetingButton = UIButton.FromType(UIButtonType.RoundedRect);
            greetingButton.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            greetingButton.SetTitle("Search", UIControlState.Normal);
            this.View.AddSubview(greetingButton);
            var greetingLabel = new UILabel()
            {
                Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height),

            };
            this.View.AddSubview(greetingLabel);
            loading.Hidden = true;
            this.View.AddSubview(loading);
            greetingButton.TouchUpInside += async (sender, args) =>
            {
                loading.StartAnimating();
                loading.Hidden = false;
                nameField.ResignFirstResponder();
                _movieNames = await api.GetMovieTitle(nameField.Text);
                loading.StopAnimating();
                loading.Hidden = true;
                this.NavigationController.PushViewController(new MovieListController(this._movieNames),true);
            };
        }
    }
}
