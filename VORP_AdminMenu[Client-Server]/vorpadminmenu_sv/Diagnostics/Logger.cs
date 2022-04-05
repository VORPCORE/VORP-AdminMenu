using System;
using static CitizenFX.Core.Native.API;

namespace vorpadminmenu_sv.Diagnostics
{
    public static class Logger
    {
        #region Public Methods
        public static void Info(string msg)
        {
            if (GetConvarInt("vorp_info_enable", 0) == 1)
                WriteLine("INFO", msg);
        }

        public static void Success(string msg)
        {
            if (GetConvarInt("vorp_success_enable", 0) == 1)
                WriteLine("SUCCESS", msg);
        }

        public static void Warn(string msg)
        {
            if (GetConvarInt("vorp_warning_enable", 0) == 1)
                WriteLine("WARN", msg);
        }

        public static void Error(string msg)
        {
            if (GetConvarInt("vorp_error_enable", 0) == 1)
                WriteLine("ERROR", msg);
        }

        public static void Error(Exception ex, string msg = "")
        {
            if (GetConvarInt("vorp_error_enable", 0) == 1)
                WriteLine("ERROR", $"{msg}\r\n{ex}");
        }

        public static void Debug(string msg)
        {
            if (GetConvarInt("vorp_debug_enable", 0) == 1)
                WriteLine("DEBUG", msg);
        }
        #endregion

        #region Private Method
        private static void WriteLine(string title, string msg)
        {
            try
            {
                string output = $"[{title}] {msg}";
                CitizenFX.Core.Debug.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
