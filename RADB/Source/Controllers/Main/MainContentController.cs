using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;

namespace RADB
{
    public partial class MainContentController
    {
        #region Entrada
        public MainContentController(MainContentForm pageForm)
        {
            Page = pageForm;
            Page.Shown += ShownForm;
            Page.KeyDown += KeyDownForm;
            Page.KeyPreview = true;
        }

        private FlatButton SelectedTab { get; set; }

        private void ShownForm(object sender, EventArgs ev)
        {
            SpeedRunTabButton.Click += (s, e) => SetContent(s, Session.SpeedRunForm);

            ConfigTabButton.Click += (s, e) => SetContent(s, Session.ConfigForm);

            SpeedRunTabButton.PerformClick();

            if (SelectedTab != null)
            {
                SelectedTab.Focus();
            }
        }

        private void KeyDownForm(object sender, KeyEventArgs e)
        {
            if (Page.ActiveControl is FlatButton)
            {
                var ctl = Page.ActiveControl as FlatButton;

                if (e.KeyData == Keys.Space)
                {
                    Window.SendKey(Keys.Enter);
                }

                if (e.KeyData == Keys.Enter)
                {
                    ctl.PerformClick();
                }

                return;
            }

            if (e.KeyData == Keys.Escape)
            {
                SelectedTab.Focus();
            }
        }

        private void SetSelectedTab(FlatButton clickedButton)
        {
            if (SelectedTab != null && SelectedTab != clickedButton)
            {
                SelectedTab.Selected = false;
            }

            clickedButton.Selected = true;
            SelectedTab = clickedButton;
        }

        private void ResizeContent(Size contentSize)
        {
            if (MainBaseForm.AutoResizeWindow == false)
            {
                return;
            }

            var newW = contentSize.Width + ContentLPanel.Width + 30;
            var newH = contentSize.Height + 26;

            if (Session.MainForm.StatusBarEnable)
            {
                newH += 24;
            }

            Session.MainForm.ClientSize = new Size(newW, newH);
        }
        #endregion

        #region Common
        private void SetContent<T>(object sender, T contentForm) where T : ContentBaseForm, new()
        {
            if (contentForm == null || contentForm.IsDisposed)
            {
                try
                {
                    contentForm = new T();
                }
                catch (Exception ex)
                {
                    ExceptionManager.Resolve(ex, Environment.NewLine + "ShownForm : " + typeof(T).ToString());
                    return;
                }
            }

            if (ContentRInsidePanel.Controls.Contains(contentForm))
            {
                contentForm.Focus();
                return;
            }

            contentForm.AutoScroll = false;
            SetSelectedTab(sender as FlatButton);

            ContentRInsidePanel.Controls.Clear();
            ContentRInsidePanel.Controls.Add(contentForm);
            ThemeBase.CheckTheme(contentForm);

            contentForm.Show();
            contentForm.Dock = DockStyle.Fill;
            contentForm.Focus();

            ResizeContent(contentForm.SizeOriginal);
            contentForm.AutoScroll = true;

            // Fix Resize Selection End
            foreach (var item in contentForm.GetControls<FlatMaskedTextBox>())
            {
                item.SelectionStart = 0;
            }

            foreach (var item in contentForm.GetControls<FlatTextBox>())
            {
                item.SelectionStart = 0;
            }

            CenterMainWindow(contentForm).TryAwait();
        }

        private async Task CenterMainWindow<T>(T contentForm) where T : ContentBaseForm, new()
        {
            await Session.MainForm.CenterWindow();
            await Task.Delay(50);

            contentForm.FinalLoadOnShow();
        }
        #endregion
    }
}