// Helpers/Settings.cs
using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SmokeT.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

	//	private const string SettingsKey = "settings_key";
	//	private static readonly string SettingsDefault = string.Empty;

		#endregion

        public static bool firstTime
        {

            get
            {
                return AppSettings.GetValueOrDefault("firstTime", true);
            }
            set
            {
                AppSettings.AddOrUpdateValue("firstTime", value);
            }
        }

        public static string ciggarsToday
        {

            get
            {
                return AppSettings.GetValueOrDefault("ciggarsToday", "0");
            }
            set
            {
                AppSettings.AddOrUpdateValue("ciggarsToday", value);
            }
        }


        public static string ciggarsPerDay
		{
            
			get
			{
                return AppSettings.GetValueOrDefault("ciggarsPerDay", "");
			}
			set
			{
                AppSettings.AddOrUpdateValue("ciggarsPerDay", value);
			}
		}

        public static string costByPocket
        {

            get
            {
                return AppSettings.GetValueOrDefault("costByPocket", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("costByPocket", value);
            }
        }


        public static string ciggarsPerPocket
        {

            get
            {
                return AppSettings.GetValueOrDefault("ciggarsPerPocket", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("ciggarsPerPocket", value);
            }
        }

        public static string spent
        {

            get
            {
                return AppSettings.GetValueOrDefault("spent", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("spent", value);
            }
        }


        public static string dateToday
        {

            get
            {
                return AppSettings.GetValueOrDefault("dateToday", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("dateToday", value);
            }
        }

        public static string hours
        {

            get
            {
                return AppSettings.GetValueOrDefault("hours", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("hours", value);
            }
        }

        public static string minutes
        {

            get
            {
                return AppSettings.GetValueOrDefault("minutes", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("minutes", value);
            }
        }

        public static string timerStop
        {

            get
            {
                return AppSettings.GetValueOrDefault("timerStop", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("timerStop", value);
            }
        }

        public static void ClearAll(){
            spent = "";
            ciggarsPerDay = "";
            ciggarsPerPocket = "";
            ciggarsToday = "";
            costByPocket = "";
            dateToday = "";

        }

        public static void ChangeToday()
        {
            ciggarsToday = "0";
            dateToday = DateTime.Now.ToString() ;
        }

        internal static void Start(string _costByPocket, string _ciggarsPerDay, string _ciggarsPerPocket)
        {
            Settings.costByPocket = _costByPocket;
            Settings.ciggarsPerDay = _ciggarsPerDay;
            Settings.ciggarsPerPocket = _ciggarsPerPocket;
            Settings.spent = "0";
            Settings.ciggarsToday = "0";
            Settings.dateToday = DateTime.Now.ToString();
            Settings.firstTime = false;
        }

        internal static void ChangeSettings(string _costByPocket, string _ciggarsPerDay, string _ciggarsPerPocket)
        {
            Settings.costByPocket = _costByPocket;
            Settings.ciggarsPerDay = _ciggarsPerDay;
            Settings.ciggarsPerPocket = _ciggarsPerPocket;
        }
	}
}