using Newtonsoft.Json;
using GNX.Desktop;

namespace RADB
{
    public class Options
    {
        [JsonIgnore]
        public const string FileName = "Options.json";
        [JsonIgnore]
        public static bool Loaded;

        bool _DarkMode = true;
        bool _DebugMode = true;

        [JsonConverter(typeof(BoolConverter))]
        public bool DarkMode { get { return _DarkMode; } set { _DarkMode = value; } }

        [JsonConverter(typeof(BoolConverter))]
        public bool DebugMode { get { return _DebugMode; } set { _DebugMode = value; } }

        public bool ToggleDarkMode()
        {
            DarkMode = Theme.ToggleDarkTheme();
            return Session.UpdateOptions();
        }

        public bool ToggleDebugMode()
        {
            DebugMode = !DebugMode;
            DebugManager.Enable = DebugMode;
            MainBaseForm.DebugMode = DebugMode;

            return Session.UpdateOptions();
        }

        #region System
        [JsonIgnore]
        public string SystemDatabaseFile = @"Data\database.db";
        #endregion
    }
}