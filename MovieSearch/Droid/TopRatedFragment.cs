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
using Fragment = Android.Support.V4.App.Fragment;
using Newtonsoft.Json;
using System.Threading;

namespace MovieSearch.Droid
{
    class TopRatedFragment : Fragment
    {
        IMovieApi _api;
        List<Movie> _movieList;
        ProgressBar _spinner;
        ListView listView;

        public TopRatedFragment(IMovieApi api)
        {
            this._api = api;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.topRated, container, false);
            _movieList = new List<Movie>();
            listView = rootView.FindViewById<ListView>(Resource.Id.listView);
            _spinner = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            listView.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this.Context, typeof(MovieDetailsActivity));
                var movie = _movieList[args.Position];
                intent.PutExtra("singleMovie", JsonConvert.SerializeObject(movie));
                this.StartActivity(intent);
            };
            listView.Adapter = new MovieListAdapter(this.Activity, _movieList);

            return rootView;

        }

        public async void GetMoviesAsync()
        {
            Activity.RunOnUiThread(() =>
            {
               _spinner.Visibility = ViewStates.Visible;
               listView.Adapter = new MovieListAdapter(Activity, new List<Movie>());
            });
            _movieList = await this._api.GetTopRatedMovies();
            Activity.RunOnUiThread(() =>
            {
                _spinner.Visibility = ViewStates.Gone;
                listView.Adapter = new MovieListAdapter(Activity, _movieList);
            });
        }
    }
}