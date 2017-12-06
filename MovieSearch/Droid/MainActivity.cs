using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views.InputMethods;
using System.Collections.Generic;
using Android.Content;
using System.Linq;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    [Activity(Label = "Movie search", Icon = "@mipmap/icon", Theme = "@style/MyTheme")]
    public class MainActivity : Activity
    {
        public static IMovieApi Api;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var textField = FindViewById<EditText>(Resource.Id.titleInput);
            var listButton = FindViewById<Button>(Resource.Id.listButton);

            listButton.Click += async (object sender, EventArgs e) =>
            {
                List<Movie> movies = await Api.GetMoviesByTitle(textField.Text);
                var intent = new Intent(this,typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(movies));
                this.StartActivity(intent);
            };
        }
    }
}

