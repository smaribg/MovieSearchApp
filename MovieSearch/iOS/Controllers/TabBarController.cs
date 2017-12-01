using System;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class TabBarController : UITabBarController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.TabBar.BackgroundColor = UIColor.White;
            this.TabBar.TintColor = UIColor.FromRGB(14,122,254);

            this.SelectedIndex = 0;
        }
    }
}
