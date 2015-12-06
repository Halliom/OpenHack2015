
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace OpenHack
{
	[Activity (Label = "CreateJob")]			
	public class CreateJob : Activity
	{
		public static Dictionary<string, int> SKILL_MAPPINGS = new Dictionary<string, int> ();

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Create_Job);

			ListView skillsListView = FindViewById<ListView> (Resource.Id.skillsList);
			List<string> skills = new List<string> ();
			ArrayAdapter<string> skillsArrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, skills);
			skillsListView.Adapter = skillsArrayAdapter;

			Button addSkill = FindViewById<Button> (Resource.Id.addSkillToList);
			addSkill.Click += delegate {

				Spinner spinner = FindViewById<Spinner>(Resource.Id.skillSpinner);
				string selectedItem = (string) spinner.SelectedItem;

				for (int i = 0; i < skillsArrayAdapter.Count; ++i)
				{
					if (String.Equals(skillsArrayAdapter.GetItem(i), selectedItem))
					{
						TextView notEqualWarning = FindViewById<TextView>(Resource.Id.duplicateWarning);
						notEqualWarning.Visibility = ViewStates.Visible;
						return;
					}
				}

				ListView listView = FindViewById<ListView>(Resource.Id.skillsList);
				((ArrayAdapter) listView.Adapter).Add(selectedItem);
				((ArrayAdapter) listView.Adapter).NotifyDataSetChanged();
			};

			Button confirm = FindViewById<Button> (Resource.Id.confirm);
			confirm.Click += delegate {
				int[] skillsArray = new int[skillsArrayAdapter.Count];
				for (int ind = 0; ind < skillsArrayAdapter.Count; ++ind)
				{
					skillsArray[ind] = SKILL_MAPPINGS[(string) skillsArrayAdapter.GetItem(ind)];
				}
				TextView jobTitleTextView = FindViewById<TextView>(Resource.Id.jobTitle);
				TextView jobDescTextView = FindViewById<TextView>(Resource.Id.jobDesc);
				string jobTitle = jobTitleTextView.Text;
				string jobDesc = jobDescTextView.Text;

				if (NetworkHelper.AddJob(jobTitle, skillsArray, jobDesc))
				{
					Finish();
				}
			};
		}

		protected override async void OnResume()
		{
			base.OnResume ();
			
			Spinner spinner = FindViewById<Spinner> (Resource.Id.skillSpinner);
			List<string> skills = new List<string>(await NetworkHelper.GetAvailableSkills());
			ArrayAdapter<string> listViewAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, skills);
			spinner.Adapter = listViewAdapter;
		}
	}
}

