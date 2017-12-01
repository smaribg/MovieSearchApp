using System;
using CoreGraphics;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieDetailsController : UIViewController
    {
        private Movie _movie;
        private const double StartX = 20;
        private const double StartY = 80;
        private const double Height = 50;

        public MovieDetailsController(Movie movie)
        {
            this._movie = movie;
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.BackgroundColor = UIColor.White;
            this.Title = "Movie info";
            var backdrop = backdropView();
            var titl = titleLabel();
            titl.SizeToFit();
            titl.TextContainer.LineBreakMode = UILineBreakMode.TailTruncation;
            titl.TextContainer.MaximumNumberOfLines = 2;
            if (titl.Frame.Height > 50){
                titl.Frame = new CGRect(135, 220, this.View.Bounds.Width - 155, 50);

            }
            else{
                titl.Frame = new CGRect(135, 220, this.View.Bounds.Width - 155, titl.Frame.Height);

            }
            var titlHeight = titl.Frame.Height;
            var info = infoLabel(titlHeight);
            var image = imageView();
			var desc = descriptionLabel();
            this.View.AddSubviews(new UIView[] { backdrop,titl,info,image,desc });


        }
        private UIImageView backdropView()
        {
            return new UIImageView()
            {
                Frame = new CGRect(0, 50, this.View.Bounds.Width, 170),
                Image = UIImage.FromFile(_movie.BackdropLocal)

            };
        }

        private UITextView titleLabel()
        {
            return new UITextView()
            {
                Frame = new CGRect(135, 220, this.View.Bounds.Width - 155, 50),
                Text = _movie.Title,
                Font = UIFont.FromName("ArialMT", 18f),
                TextColor = UIColor.Black,
            };
        }

        private UILabel infoLabel(nfloat height)
        {
            var g = "";
            foreach(String s in _movie.Genres){
                g +=s;
                g += ", ";
            }
            if(g.Length != 0)
                g = g.Remove(g.Length-2);
            return new UILabel()
            {
                
                Frame = new CGRect(135, 220+height, this.View.Bounds.Width - 155, 15),
                Text = _movie.Runtime + " min | " + g,
                Font = UIFont.FromName("ArialMT", 12f),
                TextColor = UIColor.Gray,
                 
                LineBreakMode = UILineBreakMode.Clip
                                   
            };
        }



        private UIImageView imageView(){
            return new UIImageView()
            {
                Frame = new CGRect(20, 140, 100, 150),
                Image = UIImage.FromFile(_movie.ImageLocal)
                               
            };
        }

        private UITextView descriptionLabel()
		{
            return new UITextView()
            {
                Frame = new CGRect(5, 290, this.View.Bounds.Width - 5, 90),
                Text = _movie.Description,
                Font = UIFont.FromName("ArialMT", 12f),
                TextColor = UIColor.DarkGray
                                 
			};
		}

    }
}
