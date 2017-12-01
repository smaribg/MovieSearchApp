using System;
using System.Collections.Generic;
using MovieSearch.iOS.Controllers;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListController : UITableViewController
    {
		private readonly List<Movie> _movies;
		
        public MovieListController(List<Movie> movieList)
        {
            this._movies = movieList;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Favorites, 0);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Movie list";

            this.TableView.Source = new MovieDataSource(this._movies, onSelectedMovie);
        }

        private void onSelectedMovie(int row){
            this.NavigationController.PushViewController(new MovieDetailsController(_movies[row]), false);
        }
    }
}
