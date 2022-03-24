using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RADB
{
    public class Download
    {
        public string URL { get; set; }
        public string FileName { get; set; }
        public Form Form { get; set; }
        public ProgressBar ProgressBar { get; set; }
        public Label LabelTime { get; set; }
        public Label LabelBytes { get; set; }

        public Download() 
        {
            //URL = GetDaoClassAndMethod(2);
            Form = Form.ActiveForm;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetDaoClassAndMethod(int frameIndex = 0)
        {
            var st = new StackTrace();
            var sf = st.GetFrame(frameIndex);

            return sf.GetMethod().DeclaringType.Name;
        }
    }
}
