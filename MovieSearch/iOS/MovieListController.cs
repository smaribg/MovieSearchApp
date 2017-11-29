using System;
using System.Collections.Generic;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieListController : UITableViewController
    {
		private readonly List<string> _nameList;
		
        public MovieListController(List<string> nameList)
        {
            this._nameList = nameList;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Movie list";

            this.TableView.Source = new MovieDataSource(this._nameList);
        }
    }
}
