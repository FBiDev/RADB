using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using GNX;
using GNX.Desktop;

namespace RADB
{
    public static class Session
    {
        #region Options
        public static Options Options = new Options();

        public static bool LoadOptions()
        { return Options.Loaded = Json.Load(ref Options, Options.FileName); }

        public static bool UpdateOptions()
        { return Json.Save(Options, Options.FileName); }
        #endregion

        #region Main
        public const bool Singleton = true;
        public const string SystemName = "RADatabase";

        public const CultureID Language = CultureID.UnitedStates_English;
        public const CultureID LanguageNumbers = CultureID.Brazil_Portuguese;

        public static void Start()
        {
            LanguageManager.SetLanguage(Language);
            LanguageManager.SetLanguageNumbers(LanguageNumbers);
            //AppManager.Start();

            LoadOptions();

            Banco.Load();

            DebugManager.Enable = Options.DebugMode;
            DebugManager.LogSQLSistema.SyncList(Banco.Log);
            //cDebug.Open();
        }
        #endregion

        #region Forms
        public static Main MainForm;

        public delegate Task AsyncAction();
        public delegate bool ActionWithGame(Game game);

        public static event Action OnRALoggedChanged = delegate { };
        public static event Action OnUserChanged = delegate { };
        public static event Action OnTabMainChanged = delegate { };

        public static event AsyncAction OnGameListChanged = delegate { return Task.Run(() => { }); };
        public static event AsyncAction OnConsoleChanged = delegate { return Task.Run(() => { }); };
        public static event AsyncAction OnGameChanged = delegate { return Task.Run(() => { }); };
        public static event AsyncAction OnGameExtendChanged = delegate { return Task.Run(() => { }); };

        public static event ActionWithGame OnAddGamesToPlay = delegate { return false; };
        public static event ActionWithGame OnAddGamesToHide = delegate { return false; };
        public static event ActionWithGame OnAddGames = delegate { return false; };

        static bool _RALogged;
        public static bool RALogged
        {
            get { return _RALogged; }
            set
            {
                _RALogged = value;
                OnRALoggedChanged();
            }
        }

        public static TabPage SelectedTab;
        public static void TabMainChanged(TabPage tab)
        {
            SelectedTab = tab;
            OnTabMainChanged();
        }

        static User _User;
        public static User User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnUserChanged();
            }
        }

        public static List<DataGridView> lstDgvGames = new List<DataGridView>();

        public static void GameListChanged(bool changed = true)
        {
            if (changed)
                OnGameListChanged();
        }

        public static bool AddGamesToPlay(Game game) { return OnAddGamesToPlay(game); }
        public static bool AddGamesToHide(Game game) { return OnAddGamesToHide(game); }
        public static bool AddGames(Game game) { return OnAddGames(game); }

        public static Console LastConsole { get; set; }
        static Console _Console;
        public static Console Console
        {
            get { return _Console; }
            set
            {
                _Console = value;
                OnConsoleChanged();
            }
        }

        static Game _Game;
        public static Game Game
        {
            get { return _Game; }
            set
            {
                _Game = value;
                OnGameChanged();
            }
        }

        static GameExtend _GameExtend;
        public static GameExtend GameExtend
        {
            get { return _GameExtend; }
            set
            {
                _GameExtend = value;
                OnGameExtendChanged();
            }
        }
        #endregion
    }
}
