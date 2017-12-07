using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views.InputMethods;
using System.Collections.Generic;
using Android.Content;
using System.Linq;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.V4.App;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using System.Threading;

namespace MovieSearch.Droid
{
    [Activity(Label = "Movie search", Icon = "@mipmap/icon", Theme = "@style/MyTheme")]
    public class MainActivity : FragmentActivity
    {
        public static IMovieApi Api;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var fragments = new Fragment[]
            {
                new TitleInputFragment(Api),
                new TopRatedFragment(Api)
            };

            var titles = CharSequence.ArrayFromStringArray(new[] { "Movie list", "Top Rated" });

            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            tabLayout.TabSelected += (sender, args) =>
            {
                var titleInput = (TitleInputFragment)fragments[0];
                titleInput.HideInput();
                if(args.Tab.Text == "Top Rated")
                {
                    var toprated = (TopRatedFragment)fragments[1];
                    var th = new Thread(toprated.GetMoviesAsync);
                    th.Start();
                }
            };

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "MovieSearch";
        }
    }
}

