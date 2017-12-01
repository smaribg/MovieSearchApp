using System;
using System.Collections.Generic;
using MovieDownload;
using UIKit;
using MovieSearch;
using CoreGraphics;

namespace MovieSearch.iOS.Controllers
{
    public class TopRatedController : UITableViewController
    {
        private IMovieApi _api;
        private List<Movie> _movies;
        private ImageDownloader _imgDl;
		private UIActivityIndicatorView _loading;
        private bool _fromDetails = false;

        public TopRatedController(MovieApi movieApi, ImageDownloader im)
        {
            _api = movieApi;
            _imgDl = im;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 0);
            this.TabBarItem.Title = "Top Rated";
            _api.GetTopRatedMovies();

        }

        public override void ViewDidLoad(){
            base.ViewDidLoad();
            this.Title = "Top Rated";
			_loading = loadingView();
			this.View.AddSubview(_loading);
            
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (!_fromDetails)
            {
                loadData();
            }
			this._fromDetails = false;
        }

        private void onSelectedMovie(int row)
        {
            _fromDetails = true;
            this.NavigationController.PushViewController(new MovieDetailsController(_movies[row]), false);
        }

        private UIActivityIndicatorView loadingView()
        {
            return new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray)
            {
                Frame = new CGRect(this.View.Bounds.Width / 2, this.View.Bounds.Height / 3, 10, 10)

            };
        }

        private async void loadData(){
            this.TableView.Source = new MovieDataSource(new List<Movie>(), onSelectedMovie);
            this.TableView.ReloadData();

            this.TableView.ScrollEnabled = false;

            _loading.StartAnimating();
            _loading.Hidden = false;

            // Fetch images
            this._movies = await this._api.GetTopRatedMovies();
            await _imgDl.DownloadImages(_movies);
            await _imgDl.DownloadBackdrops(_movies);

            _loading.StopAnimating();
            _loading.Hidden = true;

            this.TableView.ScrollEnabled = true;

            this.TableView.Source = new MovieDataSource(this._movies, onSelectedMovie);
            this.TableView.ReloadData();
        }

    }
}
