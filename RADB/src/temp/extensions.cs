using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace RADB
{
    public static class extensions
    {
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body)
        {
            return Task.WhenAll(
                from item in source
                select Task.Run(() => body(item)));
        }

        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            while (!control.Visible)
            {
                System.Threading.Thread.Sleep(50);
            }

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
