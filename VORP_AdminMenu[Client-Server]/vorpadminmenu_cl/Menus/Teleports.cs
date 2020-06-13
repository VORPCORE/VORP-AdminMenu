using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Menus
{
    class Teleports
    {
        private static Menu teleportsMenu = new Menu(GetConfig.Langs["MenuTeleportsTitle"], GetConfig.Langs["MenuTeleportsDesc"]);
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(teleportsMenu);

            
        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return teleportsMenu;
        }
    }
}
