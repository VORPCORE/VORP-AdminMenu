using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Menus
{
    class Administration
    {
        private static Menu administrationMenu = new Menu(GetConfig.Langs["MenuAdministrationTitle"], GetConfig.Langs["MenuAdministrationDesc"]);
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(administrationMenu);

            
        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return administrationMenu;
        }
    }
}
