using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MovieSearch.iOS.Views
{
    public class MovieCell : UITableViewCell
    {

        private readonly UILabel _headingLabel;
        private readonly UILabel _subheadingLabel;
        private readonly UILabel _ratingView;
        private readonly UIImageView _imageView;

        private int _imageSize = 100;


        public MovieCell(NSString cellID) : base(UITableViewCellStyle.Default,cellID)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Blue;

            this._imageView = imageView();
            this._headingLabel = headingLabel();
            this._subheadingLabel = subheadingLabel();
            this._ratingView = ratingView();

            this.ContentView.AddSubviews(new UIView[] { this._headingLabel, this._subheadingLabel, this._imageView, this._ratingView });
            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;

        }

        private UILabel ratingView()
        {
            return new UILabel()
            {
                Frame = new CGRect(82, 72.5, this.ContentView.Bounds.Width - 60, 25),
                Font = UIFont.FromName("ArialMT", 11f),
                TextColor = UIColor.DarkGray,
                BackgroundColor = UIColor.Clear
            };
        }

        private UILabel subheadingLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(82, 47.5, this.ContentView.Bounds.Width - 60, 25),
                Font = UIFont.FromName("ArialMT", 11f),
                TextColor = UIColor.DarkGray,
                BackgroundColor = UIColor.Clear
            };
        }

        private UILabel headingLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(82, 22.5, this.ContentView.Bounds.Width - 60, 25),
                Font = UIFont.FromName("Helvetica", 18f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };
        }

        private UIImageView imageView()
        {
            return new UIImageView()
            {
                Frame = new CGRect(5, 10, _imageSize - 30, _imageSize)
            };
        }

        public void UpdateCell(Movie movie)
        {
            this._headingLabel.Text = movie.Title + " (" + movie.Year + ")";
            this._subheadingLabel.Text = getActors(movie);
            this._imageView.Image = UIImage.FromFile(movie.ImageLocal);
            this._ratingView.Text = "★  ";
            this._ratingView.Text += movie.AverageVote.ToString();

        }
        private string getActors(Movie movie){
            var g = "";
            foreach (string actor in movie.Actors)
            {
                g += actor;
                g += ", ";
            }
            if (g.Length != 0)
                g = g.Remove(g.Length - 2);

            return g;
        }


    }
}
