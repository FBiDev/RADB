using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;

namespace RADB
{
    public class MainLogic
    {
        #region Properties
        readonly Main f;

        event EventHandler RALoggedChanged = delegate { };
        bool _RALogged;
        bool RALogged
        {
            get { return _RALogged; }
            set
            {
                _RALogged = value;
                RALoggedChanged(this, EventArgs.Empty);
            }
        }

        RA RA = new RA();
        public Game GameBind;

        event EventHandler ConsoleBindChanged = delegate { };
        Console _ConsoleBind;
        public Console ConsoleBind
        {
            get { return _ConsoleBind; }
            set
            {
                _ConsoleBind = value;
                ConsoleBindChanged(this, EventArgs.Empty);
            }
        }

        event EventHandler ConsoleGridChanged = delegate { };
        #endregion

        public MainLogic(Main form)
        {
            f = form;

            Form_Init();
            Consoles_Init();
            Games_Init();
            GameInfo_Init();
            About_Init();
        }

        #region Form
        void Form_Init()
        {
            f.Shown += Form_Shown;
            f.Resize += Form_Resize;
            f.KeyDown += Form_KeyDown;
        }

        async void Form_Shown(object sender, EventArgs e)
        {
            await Consoles_Shown();
            await Games_Shown();
        }

        void Form_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Modifiers == Keys.Alt;
        }

        FormWindowState? LastWindowState;
        void Form_Resize(object sender, EventArgs e)
        {
            if (f.WindowState != LastWindowState)
            {
                LastWindowState = f.WindowState;
                if (f.WindowState == FormWindowState.Maximized)
                {
                    //await LoadGamesIcon();
                }
                else if (f.WindowState == FormWindowState.Normal) { }
            }
        }
        #endregion

        #region Consoles
        void Consoles_Init()
        {
            f.btnUpdateConsoles_.Click += btnUpdateConsoles_Click;

            f.lblUpdateConsoles_.Text = string.Empty;
            f.lblProgressConsoles_.Text = string.Empty;

            f.dgvConsoles_.Columns.Format(CellStyle.StringCenter, 0);
            f.dgvConsoles_.Columns.Format(CellStyle.NumberCenter, 3, 4);

            f.dgvConsoles_.AutoGenerateColumns = true;
            f.dgvConsoles_.CellDoubleClick += dgvConsoles_CellDoubleClick;
            //f.dgvConsoles_.KeyPress += dgvConsoles_KeyPress;
            f.dgvConsoles_.KeyDown += dgvConsoles_KeyDown;
        }

        async Task Consoles_Shown()
        {
            await LoadConsoles();
        }

        public void DisablePanelConsoles()
        {
            f.pnlDownloadConsoles_.Enabled = false;
            f.lblNotFoundConsoles_.Visible = false;
            f.picLoaderConsole_.Visible = true;
            f.dgvConsoles_.Enabled = false;
        }

        public void EnablePanelConsoles()
        {
            f.pnlDownloadConsoles_.Enabled = true;
            f.lblNotFoundConsoles_.Visible = (f.dgvConsoles_.RowCount == 0);
            f.picLoaderConsole_.Visible = false;
            f.dgvConsoles_.Enabled = true;
        }

        async Task LoadConsoles()
        {
            DisablePanelConsoles();
            f.dgvConsoles_.DataSource = new ListBind<Console>(await Console.List());
            EnablePanelConsoles();

            f.dgvConsoles_.Focus();
        }

        async void btnUpdateConsoles_Click(object sender, EventArgs e)
        {
            f.dgvConsoles_.DataSource = new List<Console>();
            DisablePanelConsoles();
            await RA.DownloadConsoles();
            await LoadConsoles();

            f.lblOutput_.Text = "[" + DateTime.Now.ToTimeLong() + "] " + "Consoles Updated!" + Environment.NewLine + f.lblOutput_.Text;
        }

        void dgvConsoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowHeader()) return;

            Console ConsoleSelected = dgv_SelectionChanged<Console>(sender);
            if (ConsoleBind.IsNull() || ConsoleSelected.ID != ConsoleBind.ID)
                ConsoleBind = ConsoleSelected;

            f.tabMain_.SelectedTab = f.tabGames_;
        }

        void dgvConsoles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                dgvConsoles_CellDoubleClick(sender, new DataGridViewCellEventArgs(0, ((DataGridView)sender).CurrentRow.Index));
            }
        }
        #endregion

        #region Games
        void Games_Init()
        {
            ConsoleBindChanged += LoadGames;
        }

        async Task Games_Shown()
        {
            await f.LoadGames();
        }

        async void LoadGames(object sender, EventArgs e)
        {
            Main.ConsoleBind = ConsoleBind;

            //f.lblUpdateGameList.Text = string.Empty;
            //f.lblProgressGameList.Text = string.Empty;
            //f.pgbGameList.Value = 0;
            //f.txtSearchGames.Text = string.Empty;

            await f.LoadGames();

            //Update GameList
            //if (lstGames.Count == 0 || ConsoleBind.ID == 0 && !File.Exists(Folder.GameData + ConsoleBind.Name + ".json"))
            //{
            //btnUpdateGameList_Click(null, null);
            //}

            //UpdateConsoleLabels();
        }
        #endregion

        #region GameInfo
        void GameInfo_Init()
        {
            f.Shown += GameInfo_Shown;
            RALoggedChanged += GameInfo_Login;
        }

        void GameInfo_Shown(object sender, EventArgs e)
        {
            f.btnHashes_.Enabled = false;
        }

        void GameInfo_Login(object sender, EventArgs e)
        {
            f.btnHashes_.Enabled = RALogged;
        }
        #endregion

        #region About
        void About_Init()
        {
            f.Shown += About_Shown;
            f.btnRALogin_.Click += btnRALogin_Click;
            f.btnRAProfileAbout_.Click += btnRAProfileAbout_Click;

            f.btnUserCheevos_.Click += btnUserCheevos_Click;
        }

        void About_Shown(object sender, EventArgs e)
        {
            btnRALogin_Click(null, null);
        }

        async void btnRALogin_Click(object sender, EventArgs e)
        {
            f.lblRALogin_.ForeColor = Color.Coral;
            f.lblRALogin_.Text = "logging in...";

            f.btnRALogin_.Enabled = false;
            RALogged = false;
            RALogged = await Browser.SystemLogin();
            f.btnRALogin_.Enabled = true;

            if (Browser.RALogged)
            {
                f.lblRALogin_.ForeColor = Color.Green;
                f.lblRALogin_.Text = "logged in!";
            }
            else
            {
                f.lblRALogin_.ForeColor = Color.Firebrick;
                f.lblRALogin_.Text = "not logged in";
            }
        }

        void btnRAProfileAbout_Click(object sender, EventArgs e)
        {
            Process.Start(RA.User_URL("FBiDev"));
        }

        bool UserCheevosIsRunning;
        static UserProgress LastUser = new UserProgress();
        async void btnUserCheevos_Click(object sender, EventArgs e)
        {
            if (GameBind.IsNull())
            {
                MessageBox.Show("Select a Game in Games Tab First");
                return;
            }
            if (UserCheevosIsRunning) { return; }

            UserCheevosIsRunning = true;
            f.btnUserCheevos_.Enabled = false;
            f.lblUserCheevos_.Text = string.Empty;

            do
            {
                UserProgress user = await RA.GetUserProgress(f.txtUsername_.Text, GameBind.ID);
                f.picUserCheevos_.Image = GameBind.ImageIconBitmap;
                f.lblUserCheevos_.Text = user.NumAchieved + " / " + GameBind.NumAchievements;

                if (user.SameProgress(LastUser))
                {
                    f.lblCheevoLoopUpdate_.BackColor = Color.Orange;
                }
                else
                {
                    f.lblCheevoLoopUpdate_.BackColor = Color.LightGreen;
                    LastUser = user;
                }

                await Task.Run(() => { Thread.Sleep(500); });

                f.lblCheevoLoopUpdate_.BackColor = Color.Transparent;

                await Task.Run(() => { Thread.Sleep(2500); });

            } while (f.chkUserCheevos_.Checked);

            UserCheevosIsRunning = false;
            f.btnUserCheevos_.Enabled = true;
        }
        #endregion

        T dgv_SelectionChanged<T>(object sender) where T : class
        {
            var dgv = sender as DataGridView;
            if (dgv.CurrentRow.NotNull())
                return dgv.CurrentRow.DataBoundItem as T;
            return null;
        }
    }
}
