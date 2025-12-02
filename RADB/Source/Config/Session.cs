using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Core;
using App.Core.Desktop;
using RADB.Properties;

namespace RADB
{
    public static class Session
    {
        #region Fields
        public const CultureID Language = CultureID.Brazil_Portuguese;
        public const CultureID LanguageNumbers = CultureID.Brazil_Portuguese;

        public const bool SystemLock = true;
        public static readonly string SystemName = AppManager.Name;

        private static Options _options = new Options();

        private static bool _raLogged;
        private static Console _consoleSelected;
        private static Game _gameSelected;
        private static GameExtend _gameExtendSelected;
        private static User _userSelected;
        #endregion

        #region Events
        public delegate Task AsyncAction();

        public delegate bool ActionWithGame(Game game);

        public static event Action OnTabMainChanged = delegate { };

        public static event Action OnRALoggedChanged = delegate { };

        public static event Action OnUserChanged = delegate { };

        public static event AsyncAction OnConsoleChanged = delegate { return Task.Run(() => { }); };

        public static event AsyncAction OnGameChanged = delegate { return Task.Run(() => { }); };

        public static event AsyncAction OnGameExtendChanged = delegate { return Task.Run(() => { }); };

        public static event AsyncAction OnGameListChanged = delegate { return Task.Run(() => { }); };

        public static event ActionWithGame OnAddGames = delegate { return false; };

        public static event ActionWithGame OnAddGamesToPlay = delegate { return false; };

        public static event ActionWithGame OnAddGamesToHide = delegate { return false; };

        #endregion

        #region Properties
        public static Options Options
        {
            get { return _options; }
            private set { _options = value; }
        }

        public static MainForm MainForm { get; set; }

        public static MainContentForm MainContentForm { get; set; }

        public static ConfigForm ConfigForm { get; set; }

        public static Main MainFormRA { get; set; }

        public static SpeedRunForm SpeedRunForm { get; set; }

        public static TabPage SelectedTab { get; set; }

        public static bool RALogged
        {
            get { return _raLogged; }
            set { SetRALogged(value); }
        }

        public static List<DataGridView> MainGameList { get; set; }

        public static Console LastConsole { get; set; }

        public static Console ConsoleSelected
        {
            get { return _consoleSelected; }
            set { SetConsole(value); }
        }

        public static Game GameSelected
        {
            get { return _gameSelected; }
            set { SetGame(value); }
        }

        public static GameExtend GameExtendSelected
        {
            get { return _gameExtendSelected; }
            set { SetGameExtend(value); }
        }

        public static User UserSelected
        {
            get { return _userSelected; }
            set { SetUser(value); }
        }
        #endregion

        public static void Start()
        {
            LanguageManager.SetLanguage(Language);
            LanguageManager.SetLanguageNumbers(LanguageNumbers);

            Options.Load();

            Banco.Load();

            MainBaseForm.DebugMode = Options.IsDebugMode;

            DebugManager.Enable = Options.IsDebugMode;
            DebugManager.LogSQLSistema.SyncList(Banco.Log);

            Picture.PngCompressor = Resources.pngcrush_1_8_13_w64;
            Picture.JpgCompressor = Resources.jpegoptim_1_5_5;

            // Internet
            Browser.Load();
            RASite.Load();

            // Folders
            Folder.CreateFolders();

            // Game List
            MainGameList = new List<DataGridView>();
        }

        public static void SetFormIcon()
        {
            // MainBaseForm.Ico = Properties.Resources.ico_app;
        }

        #region Actions
        private static void SetRALogged(bool value)
        {
            _raLogged = value;
            OnRALoggedChanged();
        }

        private static void SetConsole(Console value)
        {
            _consoleSelected = value;
            OnConsoleChanged();
        }

        private static void SetGame(Game value)
        {
            _gameSelected = value;
            OnGameChanged();
        }

        private static void SetGameExtend(GameExtend value)
        {
            _gameExtendSelected = value;
            OnGameExtendChanged();
        }

        private static void SetUser(User value)
        {
            _userSelected = value;
            OnUserChanged();
        }

        public static void TabMainChanged(TabPage tab)
        {
            SelectedTab = tab;
            OnTabMainChanged();
        }

        public static void GameListChanged()
        {
            OnGameListChanged();
        }

        public static bool AddGames(Game game)
        {
            return OnAddGames(game);
        }

        public static bool AddGamesToPlay(Game game)
        {
            return OnAddGamesToPlay(game);
        }

        public static bool AddGamesToHide(Game game)
        {
            return OnAddGamesToHide(game);
        }
        #endregion
    }
}
