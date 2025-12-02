using App.Core.Desktop;
using App.File.Json;

namespace RADB
{
    public class Options
    {
        #region Fields
        [JsonIgnore]
        private const string FileName = "Options.json";
        [JsonIgnore]
        public readonly string SystemDatabaseFile = @"Data\database.db";
        #endregion

        public Options()
        {
            IsDarkMode = true;
            IsDebugMode = true;
        }

        #region Properties
        [JsonIgnore]
        public static bool IsLoaded { get; private set; }

        [JsonConverter(JsonType.Boolean)]
        public bool IsDarkMode { get; set; }

        [JsonConverter(JsonType.Boolean)]
        public bool IsDebugMode { get; set; }
        #endregion

        public bool Load()
        {
            IsLoaded = Json.Load(this, FileName);

            MainBaseForm.DebugMode = IsDebugMode;
            DebugManager.Enable = IsDebugMode;

            return IsLoaded;
        }

        public bool Update()
        {
            return Json.Save(this, FileName);
        }

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
    }
}