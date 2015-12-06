
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
	[Activity (Label = "Login")]			
	public class Login : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.Login);

			Button loginBtn = FindViewById<Button> (Resource.Id.login);
			loginBtn.Click += delegate {
				EditText email = FindViewById<EditText>(Resource.Id.emailField);
				if (String.IsNullOrEmpty(email.Text))
				{
					User user = LoginHelper.LoginUser(email.Text);
					if (user != null)
					{
						this.Finish();
					}
				}
				//TEMP
				this.Finish();
			};

			Button dontHaveAccount = FindViewById<Button>(Resource.Id.dontHaveAccount);
			dontHaveAccount.Click += delegate {
				StartActivity(typeof(DontHaveAnAccount));
			};
		}
	}
}

