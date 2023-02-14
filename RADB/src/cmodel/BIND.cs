using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RADB
{
    public static class BIND
    {
        public static Main f;

        public delegate Task AsyncAction();

        public static event Action OnRALoggedChanged = delegate { };
        public static event Action OnTabMainChanged = delegate { };
        public static event AsyncAction OnGameListChanged = delegate { return Task.Run(() => { }); };
        public static event AsyncAction OnConsoleChanged = delegate { return Task.Run(() => { }); };
        public static event AsyncAction OnGameChanged = delegate { return Task.Run(() => { }); };

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

        public static void GameListChanged(bool changed = true)
        {
            if (changed)
                OnGameListChanged();
        }

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

        public static List<DataGridView> lstDgvGames = new List<DataGridView>();
    }
}
