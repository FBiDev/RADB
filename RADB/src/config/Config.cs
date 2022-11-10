using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//
using System.Windows.Forms;
using System.Drawing;
using System.Xml;
//
using GNX;

namespace RADB
{
    public static class Config
    {
        public static CultureID Language = CultureID.Brazil_Portuguese;
        public static bool Singleton = true;
        public const string SystemName = "RADatabase";
        public static bool Loaded { get; set; }
        public static bool DarkMode { get; set; }

        public static void Start()
        {
            cApp.SetLanguage(Language);
            cApp.Start();

            //Carregar Config
            Banco.ConfigLoaded = Loaded = CarregarXML();
            DarkMode = true;

            //Carregar BaseSistema
            Banco.Carregar("", "");

            cDebug.LogSQLSistema = Banco.Log;
        }

        public static bool CarregarXML()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                cDebug.AddError(Messages.ConfigReadError());

                return false;
            }
        }
    }
}