using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Menus
{
    class Database
    {
        private static Menu databaseMenu = new Menu(GetConfig.Langs["MenuDatabaseTitle"], GetConfig.Langs["MenuDatabaseDesc"]);
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(databaseMenu);
        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return databaseMenu;
        }
    }
}
