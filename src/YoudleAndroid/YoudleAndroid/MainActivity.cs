using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace YoudleAndroid
{
    [Activity(Label = "YoUdle", MainLauncher = false, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
		//private GestureDetector _gestureDetector;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

			var _cardView = FindViewById<Android.Support.V7.Widget.CardView> (Resource.Id.card1);
			//_gestureDetector = new GestureDetector (_cardView);

            // Get our button from the layout resource,
            // and attach an event to it
//            Button button = FindViewById<Button>(Resource.Id.MyButton);
//
//            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };        
		}
    }
}

