using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Request;

namespace MovieSearch.Droid
{
    class MovieListAdapter : BaseAdapter<Movie>
    {

        private readonly Activity _context;
        private readonly List<Movie> _movieList;
        private readonly string ImageUrl = "http://image.tmdb.org/t/p/original";
        public MovieListAdapter(Activity context, List<Movie> movieList)
        {
            this._context = context;
            this._movieList = movieList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView; // re-use an existing view if one is available

            if (view == null)
                view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);

            var movie = this._movieList[position];
            view.FindViewById<TextView>(Resource.Id.title).Text = movie.Title;

            view.FindViewById<TextView>(Resource.Id.info).Text = String.Join(",", movie.Actors.ToArray());
            var imageView = view.FindViewById<ImageView>(Resource.Id.picture);
            Glide.With(_context).Load(ImageUrl + movie.ImageRemote).Into(imageView);
            view.FindViewById<TextView>(Resource.Id.rating).Text = "★" + movie.AverageVote;

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count => this._movieList.Count;

        public override Movie this[int position] => this._movieList[position];
    }
}