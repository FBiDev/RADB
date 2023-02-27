using System;
using GNX;

namespace RADB
{
    public static class Config
    {
        public static CultureID Language = CultureID.UnitedStates_English;
        public static CultureID LanguageNumbers = CultureID.Brazil_Portuguese;

        public static bool Singleton = true;
        public const string SystemName = "RADatabase";
        public static bool DarkMode { get; set; }

        public static void Start()
        {
            cApp.SetLanguage(Language);
            cApp.SetLanguageNumbers(LanguageNumbers);
            cApp.Start();

            //Carregar Config
            Banco.Loaded = CarregarXML();
            DarkMode = true;

            //Carregar BaseSistema
            Banco.Carregar();

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