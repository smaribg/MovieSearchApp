using System;
using System.Collections.Generic;
using CoreGraphics;
using MovieDownload;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieSearchController: UIViewController
    {
        
        private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;
        private IMovieApi _api;
        private List<Movie> _movies;
        private ImageDownloader _imgDl;


        public MovieSearchController(IMovieApi movieApi,ImageDownloader im){
            _imgDl = im;
            _api = movieApi;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "MovieSearch";
			this.View.BackgroundColor = UIColor.White;

            //UILabel promptLabel = displayLabel();
            UIActivityIndicatorView loading = Loading();
            UITextField name = nameField();
            name.Placeholder = "Enter Title";
            UIButton searchButt = searchButton(loading,name);
            UILabel greetingLabel = greeting();

            loading.Hidden = true;

            this.View.AddSubviews(new UIView[] {loading,name,searchButt,greetingLabel});


        }

        private UILabel greeting()
        {
            return new UILabel()
            {
                Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height),

            };
        }

        private UIButton searchButton(UIActivityIndicatorView loading,UITextField name)
        {
            var sb = UIButton.FromType(UIButtonType.RoundedRect);
            sb.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            sb.SetTitle("Search", UIControlState.Normal);

            sb.TouchUpInside += async (sender, args) =>
            {
                loading.StartAnimating();
                loading.Hidden = false;
                name.ResignFirstResponder();
                _movies = await _api.GetMoviesByTitle(name.Text);
                await _imgDl.DownloadImages(_movies);
                await _imgDl.DownloadBackdrops(_movies);
                loading.StopAnimating();
                loading.Hidden = true;
                this.NavigationController.PushViewController(new MovieListController(this._movies), true);
            };

            return sb;
        }

        private UITextField nameField()
        {
            return new UITextField()
            {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };
        }

        private UILabel displayLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter a movie title: "
            };
        }

        private UIActivityIndicatorView Loading()
        {
            return new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray)
            {
                Frame = new CGRect(this.View.Bounds.Width / 2, this.View.Bounds.Height / 2, 10, 10)

            };
        }
    }
}
