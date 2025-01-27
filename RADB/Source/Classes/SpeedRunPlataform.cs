using System.ComponentModel;

namespace RADB
{
    public class SpeedRunPlatform
    {
        public SpeedRunPlatform()
        {
        }

        [Style(Width = 70)]
        public string Id { get; set; }

        [Style(AutoSizeMode = ColumnAutoSizeMode.Fill)]
        public string Name { get; set; }

        [Display(AutoGenerateField = false)]
        public int Released { get; set; }

        public override string ToString()
        {
            if (Name.Equals(string.Empty) == false)
            {
                return Name;
            }

            return base.ToString();
        }
    }
}