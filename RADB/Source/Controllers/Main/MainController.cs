namespace RADB
{
    public class MainController
    {
        #region Entrada
        public MainController(MainForm pageForm)
        {
            Session.SetFormIcon();
            Session.MainForm = pageForm;
            Session.MainForm.StatusBarEnable = false;

            Session.MainForm.Shown += ShownForm;

            var contentForm = new MainContentForm();
            Session.MainForm.SetMainFormContent(contentForm);
        }

        private void ShownForm(object sender, System.EventArgs e)
        {
            Theme.SetTheme(Session.MainForm.IsDesignMode);
            Session.MainForm.CenterWindow();
        }
        #endregion
    }
}