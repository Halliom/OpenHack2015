using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.Net;
using System.IO;
using System.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

namespace OpenHack
{
	[Activity (Label = "OpenHack", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.button2);

			button.Click += delegate {
				StartActivity(typeof(CreateJob));
			};
		}

		protected override async void OnResume()
		{
			base.OnResume ();

//			if (LoginHelper.LocalUser == null) 
	//		{
		//		StartActivity (typeof(Login));
			//}

			ListView listView = FindViewById<ListView> (Resource.Id.listView1);
			List<string> jobItems = new List<string>(await NetworkHelper.GetAvailableJobsNearby());
			ArrayAdapter<string> listViewAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, jobItems);
			listView.Adapter = listViewAdapter;

			//string jsonString = "{{ \"user\": {{ \"name\": \"{0}\", \"description\": \"{1}\", \"email\": \"{2}\", \"phone\": \"{3}\" }} }}";
			//Log.Info("OpenHack", JsonObject.Parse(jsonString).ToString());
			//NetworkHelper.PostJsonToPage();
		}
	}
}


