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
using Fragment = Android.Support.V4.App.Fragment;
using Android.Views.InputMethods;
using Android.Graphics;

namespace MovieSearch.Droid
{
    class TitleInputFragment : Fragment
    {
        private readonly IMovieApi _api;
        private EditText textField;
        public TitleInputFragment(IMovieApi api)
        {
            this._api = api;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var rootView = inflater.Inflate(Resource.Layout.TitleInput, container, false);

            // Get our button from the layout resource,
            // and attach an event to it
            textField = rootView.FindViewById<EditText>(Resource.Id.titleInput);
            var listButton = rootView.FindViewById<Button>(Resource.Id.listButton);
            var spinner = rootView.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            spinner.Visibility = ViewStates.Invisible;


            listButton.Click += async (object sender, EventArgs e) =>
            {
                HideInput();
                listButton.Enabled = false;
                spinner.Visibility = ViewStates.Visible;
                List<Movie> movies = await _api.GetMoviesByTitle(textField.Text);
                spinner.Visibility = ViewStates.Invisible;
                listButton.Enabled = true;

                var intent = new Intent(this.Context, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(movies));
                this.StartActivity(intent);
            };

            return rootView;
        }

        public void HideInput()
        {
            var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
            manager.HideSoftInputFromWindow(textField.WindowToken, 0);
        }

    }
}