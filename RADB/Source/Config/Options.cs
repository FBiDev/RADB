using App.Core.Desktop;
using App.File.Json;

namespace RADB
{
    public class Options
    {
        #region Options
        [JsonIgnore]
        public const string FileName = "Options.json";
        [JsonIgnore]
        public static bool IsLoaded;

        public Options()
        {
            IsDarkMode = true;
            IsDebugMode = true;
        }

        [JsonConverter(JsonType.Boolean)]
        public bool IsDarkMode { get; set; }

        [JsonConverter(JsonType.Boolean)]
        public bool IsDebugMode { get; set; }

        public bool ToggleDarkMode()
        {
            IsDarkMode = Theme.ToggleDarkTheme();
            return Update();
        }

        public bool ToggleDebugMode()
        {
            IsDebugMode = !IsDebugMode;
            DebugManager.Enable = IsDebugMode;
            MainBaseForm.DebugMode = IsDebugMode;

            return Update();
        }
        #endregion

        public bool Load()
        {
            return IsLoaded = Json.Load(this, FileName);
        }

        public bool Update()
        {
            return Json.Save(this, FileName);
        }

        #region System
        [JsonIgnore]
        public string SystemDatabaseFile = @"Data\database.db";
        #endregion
    }
}