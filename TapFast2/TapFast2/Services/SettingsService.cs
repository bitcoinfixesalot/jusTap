//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TapFast2.Services
//{
//    public class SettingsService : ISettingsService
//    {
//        const string APP_SETTINGS = "APP_SETTINGS";
       

//        const int EASY = 900;
//        const int NORMAL = 800;
//        const int HARD = 700;


//        public int GetIntervalBetweenColors(DifficultyMode mode)
//        {
//            switch (mode)
//            {
//                case DifficultyMode.Easy:
//                    return EASY;
//                case DifficultyMode.Normal:
//                    return NORMAL;
//                case DifficultyMode.Hard:
//                    return HARD;;
//                default:
//                    return EASY;
                    
//            }
//        }


//        public DifficultyMode GetModeByIntervals(int interval)
//        {
//            switch (interval)
//            {
//                case EASY:
//                    return DifficultyMode.Easy;
//                case NORMAL:
//                    return DifficultyMode.Normal;
//                case HARD:
//                    return DifficultyMode.Hard;
//                default:
//                    return DifficultyMode.Easy;
//            }
//        }

//        public bool SettingExists(string key, StorageStrategies location = StorageStrategies.Local)
//        {
//            return false;
//            //switch (location)
//            //{
//            //    case StorageStrategies.Local:
//            //        return Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key);
//            //    case StorageStrategies.Roaming:
//            //        return Windows.Storage.ApplicationData.Current.RoamingSettings.Values.ContainsKey(key);
//            //    default:
//            //        throw new NotSupportedException(location.ToString());
//            //}
//        }

//        public T GetSetting<T>(string key, T otherwise = default(T), StorageStrategies location = StorageStrategies.Local)
//        {
//            return default(T);
//            //try
//            //{
//            //    if (!(SettingExists(key, location)))
//            //        return otherwise;
//            //    switch (location)
//            //    {
//            //        case StorageStrategies.Local:
//            //            return (T)Windows.Storage.ApplicationData.Current.LocalSettings.Values[key.ToString()];
//            //        case StorageStrategies.Roaming:
//            //            return (T)Windows.Storage.ApplicationData.Current.RoamingSettings.Values[key.ToString()];
//            //        default:
//            //            throw new NotSupportedException(location.ToString());
//            //    }
//            //}
//            //catch { /* error casting */ return otherwise; }
//        }

//        public void SetSetting<T>(string key, T value, StorageStrategies location = StorageStrategies.Local)
//        {
//            //switch (location)
//            //{
//            //    case StorageStrategies.Local:
//            //        Windows.Storage.ApplicationData.Current.LocalSettings.Values[key.ToString()] = value;
//            //        break;
//            //    case StorageStrategies.Roaming:
//            //        Windows.Storage.ApplicationData.Current.RoamingSettings.Values[key.ToString()] = value;
//            //        break;
//            //    default:
//            //        throw new NotSupportedException(location.ToString());
//            //}
//        }


//        public void DeleteSetting(string key, StorageStrategies location = StorageStrategies.Local)
//        {
//            //switch (location)
//            //{
//            //    case StorageStrategies.Local:
//            //        Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove(key);
//            //        break;
//            //    case StorageStrategies.Roaming:
//            //        Windows.Storage.ApplicationData.Current.RoamingSettings.Values.Remove(key);
//            //        break;
//            //    default:
//            //        throw new NotSupportedException(location.ToString());
//            //}
//        }
//    }

   
//}
