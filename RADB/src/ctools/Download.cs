using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public class Download : DownloadBase
    {
        public Download()
        {
        }

        public Download(DownloadFile file)
        {
            SetFile(file);
        }

        public override async Task<bool> Start()
        {
            await base.Start();

            if (Error) { MessageBox.Show(ErrorMessage); }

            return !Error;
        }

        //==========
        //===Desktop
        //==========
        public void SetControls(Label resultLabel, ProgressBar resultBar, Label resultTime)
        {
            ProgressChanged += () => DownloadChanged(resultLabel, resultBar, resultTime);
        }

        void DownloadChanged(Label resultLabel, ProgressBar resultBar, Label resultTime)
        {
            resultLabel.InvokeIfRequired(() =>
            {
                resultLabel.Text = Result;
            });

            switch (Status)
            {
                case DownloadStatus.Connecting:

                    resultBar.InvokeIfRequired(() =>
                    {
                        BarStart(resultBar, ProgressBarStyle.Marquee);
                    });
                    break;
                case DownloadStatus.ProgressChanged:
                    resultBar.InvokeIfRequired(() =>
                    {
                        resultBar.Value = Percentage;
                        resultBar.Style = (TotalBytesToReceive == -1 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous);
                    });
                    break;
                case DownloadStatus.FileDownloaded:
                    break;
                case DownloadStatus.NextFiles:
                    break;
                case DownloadStatus.Completed:
                    resultTime.InvokeIfRequired(() =>
                    {
                        resultTime.Text = TimeCompleted.ToDMY_TimeShort();
                    });
                    resultBar.InvokeIfRequired(() =>
                    {
                        BarStop(resultBar);
                    });
                    break;
                case DownloadStatus.Stopped:
                    resultBar.InvokeIfRequired(() =>
                    {
                        BarStop(resultBar);
                    });
                    break;
                default:
                    break;
            }
        }

        void BarStart(ProgressBar bar, ProgressBarStyle style = ProgressBarStyle.Continuous, int maximum = 100)
        {
            bar.Visible = true;
            bar.Maximum = maximum;
            bar.Value = 0;
            bar.MarqueeAnimationSpeed = 50;
            bar.Style = style;
        }

        void BarStop(ProgressBar bar)
        {
            if (bar.Style == ProgressBarStyle.Marquee)
            {
                //bar.MarqueeAnimationSpeed = 0;
                bar.Style = ProgressBarStyle.Continuous;
            }

            //Hack for Win7
            bar.Maximum++;
            bar.Value = bar.Maximum;
            bar.Value--;
            bar.Maximum--;
        }
    }
}
