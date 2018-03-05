using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapFast2.Services
{
    public interface ISettingsService
    {
        bool SettingExists(string key, StorageStrategies location = StorageStrategies.Local);

        T GetSetting<T>(string key, T otherwise = default(T), StorageStrategies location = StorageStrategies.Local);

        void SetSetting<T>(string key, T value, StorageStrategies location = StorageStrategies.Local);

        void DeleteSetting(string key, StorageStrategies location = StorageStrategies.Local);


        /// <summary>
        /// Interval in miliseconds
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        int GetIntervalBetweenColors(DifficultyMode mode);

        DifficultyMode GetModeByIntervals(int interval);

        //DifficultyMode GetDifficulty();

        //bool GetSoundEnabled();
    }

    public enum StorageStrategies
    {
        /// <summary>Local, isolated folder</summary>
        Local,
        /// <summary>Cloud, isolated folder. 100k cumulative limit.</summary>
        Roaming,
        /// <summary>Local, temporary folder (not for settings)</summary>
        Temporary
    }


    public enum DifficultyMode
    {
        Easy,
        Normal,
        Hard,
    }
}
