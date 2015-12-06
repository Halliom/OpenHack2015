using System;
using System.Json;
using Android.App;
using Android.Util;

using Newtonsoft.Json;

namespace OpenHack
{
	public class User
	{
		public string userid;
		public string usermail;
		public string description;
		public string phoneNumber;
	}

	public static class LoginHelper
	{
		public struct CreateUserData
		{
			public int[] skills;
			public string name;
			public string description;
			public string email;
			public string phone;
		}

		public static User LocalUser = null;

		public static bool CreateUser(string name, string email, int[] skillIds, string description, string phone)
		{
			CreateUserData user = new CreateUserData ();
			user.skills = skillIds;
			user.name = name;
			user.description = description;
			user.email = email;
			user.phone = phone;

			string json = JsonConvert.SerializeObject (user);
			string finalString = String.Format ("{{ \"user\": {0} }}", json);
			Log.Info ("OpenHackLogin", finalString);

			return true;
		}

		public static User LoginUser(string email) 
		{
			User user = new User ();
			user.userid = "1";
			user.usermail = "test@test.com";
			user.description = "kadaksdlasd";
			user.phoneNumber = "070123456789";
			LoginHelper.LocalUser = user;
			return user;
			if (!LoginHelper.LoadLocalUserFromFile()) 
			{
				// Do a login
				return LoginHelper.LocalUser;
			} 
			else
			{
				return LoginHelper.LocalUser;
			}
		}

		public static bool LoadLocalUserFromFile()
		{
			
			return true;
			var preferences = Application.Context.GetSharedPreferences ("OpenHack", Android.Content.FileCreationMode.Private);

			if (preferences.Contains ("UserID")) 
			{
				string userid = preferences.GetString ("UserID", null);
				LoginHelper.LocalUser = new User ();
				LoginHelper.LocalUser.userid = userid;
				return true;
			} 
			else 
			{
				return false;
			}

		}

		public static bool SaveLocalUserToFile()
		{
			if (LoginHelper.LocalUser != null && LoginHelper.LocalUser.userid != null) 
			{
				var preferences = Application.Context.GetSharedPreferences ("OpenHack", Android.Content.FileCreationMode.Private);
				var prefEditor = preferences.Edit();

				prefEditor.PutString("UserID", LoginHelper.LocalUser.userid);
				prefEditor.Commit();
				return true;
			} 
			else 
			{
				return false;
			}
		}
	}
}

