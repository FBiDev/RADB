﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using RADB.Properties;
using App.Core;
using App.Core.Desktop;

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

            //Internet
            Browser.Load();
            RASite.Load();

            //Folders
            Folder.CreateFolders();
        }

        public static void SetFormIcon()
        {
            //MainBaseForm.Ico = Properties.Resources.ico_app;
        }

        #region Actions
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
