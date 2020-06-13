using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Menus
{
    class Boosters
    {
        private static Menu boostersMenu = new Menu(GetConfig.Langs["MenuBoostersTitle"], GetConfig.Langs["MenuBoostersDesc"]);
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(boostersMenu);

            
        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return boostersMenu;
        }
    }
}
