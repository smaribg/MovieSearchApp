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

            // Create your application here
            var jsonString = this.Intent.GetStringExtra("movieList");
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonString);

            this.ListAdapter = new MovieListAdapter(this, movieList);
        }
    }
}