
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

using Android.Util;

namespace OpenHack
{
	[Activity (Label = "DontHaveAccountNextPage")]			
	public class DontHaveAccountNextPage : Activity
	{

		private string email;
		private string name;
		private string phone;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.Account_Creation_Page_2);

			email = Intent.GetStringExtra ("Email");
			name = Intent.GetStringExtra ("Name");
			phone = Intent.GetStringExtra ("Phone");

			ListView skillsListView = FindViewById<ListView> (Resource.Id.skillsListUser);
			List<string> skills = new List<string> ();
			ArrayAdapter<string> skillsArrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, skills);
			skillsListView.Adapter = skillsArrayAdapter;

			Button addSkill = FindViewById<Button> (Resource.Id.addSkillUser);
			addSkill.Click += delegate {

				Spinner spinner = FindViewById<Spinner>(Resource.Id.skillSpinnerUser);
				string selectedItem = (string) spinner.SelectedItem;

				for (int i = 0; i < skillsArrayAdapter.Count; ++i)
				{
					if (String.Equals(skillsArrayAdapter.GetItem(i), selectedItem))
					{
						return;
					}
				}

				ListView listView = FindViewById<ListView>(Resource.Id.skillsListUser);
				((ArrayAdapter) listView.Adapter).Add(selectedItem);
				((ArrayAdapter) listView.Adapter).NotifyDataSetChanged();
			};

			Button confirm = FindViewById<Button> (Resource.Id.confirmCreateUser);
			confirm.Click += delegate {
				int[] skillsArray = new int[skillsArrayAdapter.Count];
				for (int ind = 0; ind < skillsArrayAdapter.Count; ++ind)
				{
					skillsArray[ind] = CreateJob.SKILL_MAPPINGS[(string) skillsArrayAdapter.GetItem(ind)];
				}
				EditText descriptionField = FindViewById<EditText>(Resource.Id.bioCreateUser);

				if (LoginHelper.CreateUser(name, email, skillsArray, descriptionField.Text, phone))
				{
					Finish();
				}
			};
		}

		protected async void OnResume()
		{
			base.OnResume ();

			Spinner spinner = FindViewById<Spinner> (Resource.Id.skillSpinnerUser);
			List<string> skills = new List<string>(await NetworkHelper.GetAvailableSkills());
			ArrayAdapter<string> listViewAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, skills);
			spinner.Adapter = listViewAdapter;
		}
	}
}

