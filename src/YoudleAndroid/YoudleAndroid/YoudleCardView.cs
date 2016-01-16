
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace YoudleAndroid
{
	public class YoudleCardView : Android.Support.V7.Widget.CardView
	{
		private GestureDetector _gestureDetector;
		private GestureListener _gestureListner;
		private float _currentX;
		private float _startingX;

		public YoudleCardView (Context context) :
			base (context)
		{
			Initialize (context);
		}

		public YoudleCardView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize (context);
		}

		public YoudleCardView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize (context);
		}

		void Initialize (Context context)
		{
			_gestureListner = new GestureListener (context, this);
			_gestureDetector = new GestureDetector (context, _gestureListner);

			_currentX = GetX ();
			_startingX = GetX ();
		}

		public override bool OnTouchEvent (MotionEvent e)
		{
			_gestureDetector.OnTouchEvent (e);

			var action = e.Action & MotionEventActions.Mask;

			switch (action) {
				case MotionEventActions.Down:
					_currentX = e.RawX;
					Console.WriteLine ("Down: RawX" + e.RawX);
					break;
				case MotionEventActions.Move:
					Console.WriteLine ("Move: RawX" + e.RawX);

					var difference = e.RawX - _currentX;
					Console.WriteLine ("Move: Difference" + difference);
					this.TranslationX = difference;


					//_currentX = _currentX + difference;
					Console.WriteLine ("Move: New RawX" + e.RawX);
					break;
				case MotionEventActions.Up: // this is called after onFling
					if (!_gestureListner.Flinged)
					{
						Animate ()
							.X (_startingX)
							.Start ();
						_gestureListner.Flinged = false;
					}
					else
					{
						((LinearLayout)Parent).RemoveView (this);
					}
					Console.WriteLine ("Up: RawX" + e.RawX);
					break;
			}
			return true;
		}

		private class GestureListener : GestureDetector.SimpleOnGestureListener
		{
			private Context _context;
			private Android.Support.V7.Widget.CardView _cardView;
			public bool Flinged { get; set;}

			public GestureListener (Context context, Android.Support.V7.Widget.CardView cardView)
			{
				_context = context;
				_cardView = cardView;
				Flinged = false;
			}

			public override bool OnFling (MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
			{
				Toast.MakeText (_context, "on fling", ToastLength.Short).Show ();
				Console.WriteLine ("OnFling called");
				_cardView.Animate ()
					.XBy (_cardView.Resources.DisplayMetrics.WidthPixels)
					.Start ();
				Flinged = true;
				return base.OnFling (e1, e2, velocityX, velocityY);
			}
		}
	}
}

