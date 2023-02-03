using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
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

            if (Error)
            {
                MessageBox.Show(ErrorMessage);
            }

            return !Error;
        }

        //==========
        //===Desktop
        //==========
        public void SetControls(Label resultLabel, ProgressBar resultBar, Label resultTime)
        {
            this.ProgressChanged += () => DownloadChanged(resultLabel, resultBar, resultTime);
        }

        private void DownloadChanged(Label resultLabel, ProgressBar resultBar, Label resultTime)
        {
            resultLabel.InvokeIfRequired(() =>
            {
                resultLabel.Text = this.Result;
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
                        resultBar.Value = this.Percentage;
                        resultBar.Style = (this.TotalBytesToReceive == -1 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous);
                    });
                    break;
                case DownloadStatus.FileDownloaded:
                    break;
                case DownloadStatus.NextFiles:
                    break;
                case DownloadStatus.Completed:
                    resultTime.InvokeIfRequired(() =>
                    {
                        resultTime.Text = this.TimeCompleted.ToDMY_TimeShort();
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

        private void BarStart(ProgressBar bar, ProgressBarStyle style = ProgressBarStyle.Continuous, int maximum = 100)
        {
            bar.Maximum = maximum;
            bar.Value = 0;
            bar.MarqueeAnimationSpeed = 50;
            bar.Style = style;
        }

        private void BarStop(ProgressBar bar)
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
