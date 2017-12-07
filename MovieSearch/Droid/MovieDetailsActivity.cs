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
using Com.Bumptech.Glide;

namespace MovieSearch.Droid
{
    [Activity(Label = "Movie List", Theme = "@style/MyTheme")]
    class MovieDetailsActivity : Activity
    {
        private readonly string ImageUrl = "http://image.tmdb.org/t/p/original";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MovieDetails);

            var jsonString = this.Intent.GetStringExtra("singleMovie");
            var movie = JsonConvert.DeserializeObject<Movie>(jsonString);

            this.FindViewById<TextView>(Resource.Id.title).Text = movie.Title + " (" + movie.Year + ")";
            string genres = "";
            foreach(string s in movie.Genres)
            {
                genres += s + ", ";
            }
            genres = genres.Remove(genres.Length - 2);

            this.FindViewById<TextView>(Resource.Id.info).Text = movie.Runtime + " min | " + genres;
            var poster = this.FindViewById<ImageView>(Resource.Id.poster);
            Glide.With(this).Load(ImageUrl + movie.ImageRemote).Into(poster);
            var backdrop = this.FindViewById<ImageView>(Resource.Id.backdrop);
            Glide.With(this).Load(ImageUrl + movie.BackdropRemote).Into(backdrop);
            this.FindViewById<TextView>(Resource.Id.description).Text = movie.Description;


            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "MovieSearch";
        }
    }
}