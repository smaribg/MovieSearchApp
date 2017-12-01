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
        private readonly UIImageView _imageView;

        private int _imageSize = 100;


        public MovieCell(NSString cellID) : base(UITableViewCellStyle.Default,cellID)  
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Blue;

            this._imageView = new UIImageView()
            {
                Frame = new CGRect(5, 10, _imageSize-30, _imageSize)
            };
            this._headingLabel = new UILabel()
            {
                Frame = new CGRect(82, 35, this.ContentView.Bounds.Width - 60, 25),
                Font = UIFont.FromName("Helvetica", 18f),
                TextColor = UIColor.Black,
                BackgroundColor = UIColor.Clear
            };

            this._subheadingLabel = new UILabel()
            {
                Frame = new CGRect(82, 60, this.ContentView.Bounds.Width - 60, 25),
                Font = UIFont.FromName("ArialMT", 11f),
                TextColor = UIColor.DarkGray,
                BackgroundColor = UIColor.Clear
            };
            this.ContentView.AddSubviews(new UIView[]{this._headingLabel,this._subheadingLabel,this._imageView});
            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;

        }

        public void UpdateCell(Movie movie)
        {
            this._headingLabel.Text = movie.Title + " (" + movie.Year + ")";
            this._subheadingLabel.Text = getActors(movie);
            this._imageView.Image = UIImage.FromFile(movie.ImageLocal);
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
