using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using App.File.Json;

namespace RADB
{
    public class SpeedRunGame
    {
        private SpeedRunGameNames _names;

        public SpeedRunGame()
        {
            PlatformsIDs = new List<string>();
            Platforms = new List<SpeedRunPlatform>();

            RegionsIDs = new List<string>();
            DevelopersIDs = new List<string>();
        }

        [Style(Width = 70)]
        public string Id { get; set; }

        [Display(AutoGenerateField = false)]
        public SpeedRunGameNames Names
        {
            get
            {
                return _names;
            }

            set
            {
                _names = value;
                Name = _names.International;
            }
        }

        [Style(AutoSizeMode = ColumnAutoSizeMode.Fill)]
        public string Name { get; set; }

        [Display(AutoGenerateField = false)]
        public string Abbreviation { get; set; }

        [Display(AutoGenerateField = false)]
        public int Released { get; set; }

        [Style(Format = ColumnFormat.DateCenter)]
        [JsonConverter(JsonType.Date)]
        [JsonProperty("release-date")]
        public DateTime ReleaseDate { get; set; }

        public bool RomHack { get; set; }

        [Style(Format = ColumnFormat.StringCenter)]
        public SpeedRunPlatform Platform
        {
            get { return Platforms != null ? Platforms.ElementAtOrDefault<SpeedRunPlatform>(0) : null; }
        }

        [JsonProperty("platforms")]
        public List<string> PlatformsIDs { get; set; }

        [JsonIgnore]
        public List<SpeedRunPlatform> Platforms { get; set; }

        [JsonProperty("regions")]
        public List<string> RegionsIDs { get; set; }

        [JsonProperty("developers")]
        public List<string> DevelopersIDs { get; set; }

        [Display(AutoGenerateField = false)]
        public string Developer
        {
            get { return DevelopersIDs != null ? DevelopersIDs.ElementAtOrDefault(0) : string.Empty; }
        }

        public override string ToString()
        {
            if (Names.International.Equals(string.Empty) == false)
            {
                return Names.International;
            }

            return base.ToString();
        }

        public struct SpeedRunGameNames
        {
            public string International { get; set; }

            public string Japanese { get; set; }

            public string Twitch { get; set; }

            public override string ToString()
            {
                return International;
            }
        }
    }
}