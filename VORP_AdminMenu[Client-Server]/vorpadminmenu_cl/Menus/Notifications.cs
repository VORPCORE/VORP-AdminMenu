using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Menus
{
    class Notifications
    {
        private static Menu notificationsMenu = new Menu(GetConfig.Langs["MenuNotificationsTitle"], GetConfig.Langs["MenuNotificationsDesc"]);
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(notificationsMenu);

            
        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return notificationsMenu;
        }
    }
}
