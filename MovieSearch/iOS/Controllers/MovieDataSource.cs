using System;
using System.Collections.Generic;
using Foundation;
using MovieSearch.iOS.Views;
using UIKit;

namespace MovieSearch.iOS
{
    public class MovieDataSource :UITableViewSource
    {
        private readonly List<Movie> _movies;

        public readonly NSString NameListCellId = new NSString("NameListCell");
        private readonly Action<int> _onSelectedPerson;

        public MovieDataSource(List<Movie> movieList, Action<int> onSelectedPerson)
        {
            this._movies = movieList;
            this._onSelectedPerson = onSelectedPerson;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 120;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (MovieCell)tableView.DequeueReusableCell((NSString)this.NameListCellId);
            if (cell == null)
            {
                cell = new MovieCell(NameListCellId);
            }
            cell.UpdateCell(this._movies[indexPath.Row]);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return this._movies.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath){
            tableView.DeselectRow(indexPath,true);
            _onSelectedPerson(indexPath.Row);
        }
    }
}
