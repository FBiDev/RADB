using System;
using System.Collections.Generic;
using System.Xml;
//
using GNX;

namespace RADB
{
    public static class Config
    {
        public static CultureID idioma = CultureID.Brazil_Portuguese;
        public static bool umaExecucao = true;
        public const string sistema = "RADB";
        public static bool Loaded { get; set; }

        public static void Start()
        {
            cApp.SetLanguage(idioma);
            cApp.Start();

            //Carregar Config
            Banco.ConfigLoaded = Loaded = CarregarXML();

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