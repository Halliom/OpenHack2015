
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

namespace OpenHack
{
	[Activity (Label = "DontHaveAnAccount")]			
	public class DontHaveAnAccount : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.Account_Creation);

			Button nextPageBtn = FindViewById<Button> (Resource.Id.nextPage);
			nextPageBtn.Click += delegate {
				EditText emailField = FindViewById<EditText>(Resource.Id.emailFieldCreate);
				EditText nameField = FindViewById<EditText>(Resource.Id.nameFieldCreate);
				EditText phoneField = FindViewById<EditText>(Resource.Id.phoneFieldCreate);

				if (String.IsNullOrEmpty(emailField.Text) || String.IsNullOrEmpty(nameField.Text) || String.IsNullOrEmpty(phoneField.Text))
				{
					// Give feedback to the user
				}
				else
				{
					Intent nextPagePayload = new Intent(this, typeof(DontHaveAccountNextPage));
					nextPagePayload.PutExtra("Email", emailField.Text);
					nextPagePayload.PutExtra("Name", nameField.Text);
					nextPagePayload.PutExtra("Phone", phoneField.Text);

					StartActivity(nextPagePayload);
				}
			};
		}
	}
}

