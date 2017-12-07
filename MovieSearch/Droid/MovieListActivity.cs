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
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    [Activity(Label = "Movie List", Theme = "@style/MyTheme")]
    public class MovieListActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MovieList);
            // Create your application here
            var jsonString = this.Intent.GetStringExtra("movieList");
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonString);

            this.ListView.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this, typeof(MovieDetailsActivity));
                var movie = movieList[args.Position];
                intent.PutExtra("singleMovie", JsonConvert.SerializeObject(movie));
                this.StartActivity(intent);
            };
            this.ListAdapter = new MovieListAdapter(this, movieList);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "MovieSearch";
        }
    }
}