using System;
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;

using Android.Util;

struct Job
{
	public string name;
	public string description;
}

namespace OpenHack
{
	public static class NetworkHelper
	{
		public static bool Login(string username, string password)
		{
			return true;
		}

		public static bool Register(string username, string password)
		{
			return true;
		}

		public static async Task<string[]> GetAvailableJobsNearby()
		{
			JsonValue jsonDocument = await GetAsyncJsonFromPage ("http://10.0.201.163:3000/jobs");
			Log.Info ("OpenHackNetwork", jsonDocument ["data"] [0] ["attributes"].ToString ());
			Job[] jobsget = new Job[jsonDocument["data"].Count];

			for (int i = 0; i < jobsget.Length; ++i) 
			{
				jobsget [i].name = jsonDocument ["data"] [i] ["attributes"] ["name"];
				jobsget [i].description = jsonDocument ["data"] [i] ["attributes"] ["description"];
			}

			string[] result = new string[jobsget.Length];
			for (int i = 0; i < jobsget.Length; ++i) 
			{
				result [i] = jobsget [i].name + ": " + jobsget[i].description.Substring(0, 10) + "...";
			}
			//return result;
			return new string[] {"Task 1: Do this", "Task2: Do that", "Task3: Do this aswelL"};
		}

		public static async Task<String[]> GetAvailableSkills()
		{
			//JsonValue jsonDocument = await GetAsyncJsonFromPage("http://10.0.201.163:3000/skills");
			//string[] result = new string[jsonDocument["data"].Count];

			//for (int i = 0; i < result.Length; ++i) 
			//{
			//	result [i] = json ["data"] ["attributes"] ["name"];
			//}

			string[] result = new string[]{ "Hardcore Business", "Monkey Business", "Private Business"};
			CreateJob.SKILL_MAPPINGS [result [0]] = 1;
			CreateJob.SKILL_MAPPINGS [result [1]] = 2;
			CreateJob.SKILL_MAPPINGS [result [2]] = 3;
			return result;
		}

		public struct Job
		{
			public string name;
			public int[] skills;
			public int max_rate;
			public string description;
			public string job_date;
			public bool performed;
			public int owner_user_id;
		}

		public static bool AddJob(string title, int[] skills, string description)
		{
			if (LoginHelper.LocalUser != null) 
			{
				Job jobToAdd = new Job ();
				jobToAdd.name = title;
				jobToAdd.skills = skills;
				//jobToAdd.max_rate = ???;
				jobToAdd.description = description;
				//jobToAdd.job_date = ;
				jobToAdd.performed = false;
				jobToAdd.owner_user_id = int.Parse(LoginHelper.LocalUser.userid);
				//PostJsonToPage ();
				return true;
			} 
			else
			{
				return false;
			}
		}

		public static bool UpdateInformation(string jsonInput)
		{
			return true;
		}

		public static JsonValue GetJsonFromPage(string url)
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (url);
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = request.GetResponse()) {
				using (Stream responseStream = response.GetResponseStream ()) {
					JsonValue jsonFile = JsonObject.Load (responseStream);

					return jsonFile;
				}
			}
		}

		public static JsonValue PostJsonToPage(string url, string data)
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (url);
			request.ContentType = "application/json";
			request.Method = "POST";

			using (var reqStream = new StreamWriter(request.GetRequestStream()))
				reqStream.Write(data);

			using (var response = (HttpWebResponse)request.GetResponse ()) 
			{
				using (var responseStream = response.GetResponseStream())
				{
					JsonValue jsonResponse = JsonObject.Load (responseStream);

					return jsonResponse;
				}
			}
		}

		public static async Task<JsonValue> GetAsyncJsonFromPage(string url)
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (url);
			request.ContentType = "application/json";
			request.Method = "GET";
			
			using (WebResponse response = await request.GetResponseAsync ()) {
				using (Stream responseStream = response.GetResponseStream ()) {
					JsonValue jsonFile = await Task.Run (() => JsonObject.Load (responseStream));
					
					return jsonFile;
				}
			}
		}
	}
}

