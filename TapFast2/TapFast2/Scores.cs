using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Xamarin.Forms;
using TapFast2.Enums;
using TapFast2.Resx;

namespace TapFast2
{
	public class Scores
	{
		string id;
		string name;
		//bool done;

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value;}
		}

		[JsonProperty(PropertyName = "Name")]
		public string Name
		{
			get { return name; }
			set { name = value;}
		}

        [JsonProperty(PropertyName = "Score")]
        public int Score
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "OSType")]
        public int OSType
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "AverageTime")]
        public int AverageTime
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "GameType")]
        public int GameType
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "GameSubType")]
        public int GameSubType
        {
            get;
            set;
        }

        //[JsonProperty(PropertyName = "complete")]
        //public bool Done
        //{
        //	get { return done; }
        //	set { done = value;}
        //}

        [Version]
        public string Version { get; set; }

        public string GameModeText
        {
            get
            {
                string result = string.Empty;
                switch ((GameMode)GameSubType)
                {
                    case GameMode.Easy:
                        result = AppResources.Easy;
                        break;
                    case GameMode.Normal:
                        result = AppResources.Normal;
                        break;
                    case GameMode.Hard:
                        result = AppResources.Hard;
                        break;
                    case GameMode.Impossible:
                        result = AppResources.Impossible;
                        break;
                    case GameMode.FifteenSeconds:
                        result = AppResources.FifteenSeconds;
                        break;
                    case GameMode.ThirtySeconds:
                        result = AppResources.ThirtySeconds;
                        break;
                    case GameMode.SixtySeconds:
                        result = AppResources.SixtySeconds;
                        break;
                    default:
                        break;
                }

                return result;
            }
        }

        //TODO: optimize this
        public object PlatformIcon
        {
            get
            {
                switch ((TargetPlatform)OSType)
                {
                    case TargetPlatform.iOS:
                        return ImageSource.FromResource("TapFast2.Images.apple48x48.png");
                    case TargetPlatform.Android:
                        return ImageSource.FromResource("TapFast2.Images.android48x48.png");
                    case TargetPlatform.WinPhone:
                    case TargetPlatform.Windows:
                        return ImageSource.FromResource("TapFast2.Images.windows48x48.png");
                    case TargetPlatform.Other:
                    default:
                        return null;
                }
            }
        }

        public int Number { get; set; }
    }
}

