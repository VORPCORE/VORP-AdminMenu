using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Menus
{
    public class MainMenu
    {
        private static Menu mainMenu = new Menu(GetConfig.Langs["MenuMainTitle"], GetConfig.Langs["MenuMainDesc"]);
        private static bool setupDone = false;
        public static List<object> args = new List<object>();
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(mainMenu);

            string keyPress = GetConfig.Config["key"].ToString();
            int KeyInt = Convert.ToInt32(keyPress, 16);

            MenuController.EnableMenuToggleKeyOnController = false;
            MenuController.MenuToggleKey = (Control)KeyInt;

            //Administration
            MenuController.AddSubmenu(mainMenu, Administration.GetMenu());

            MenuItem subMenuAdministrationBtn = new MenuItem(GetConfig.Langs["MenuAdministrationTitle"], " ")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(subMenuAdministrationBtn);
            MenuController.BindMenuItem(mainMenu, Administration.GetMenu(), subMenuAdministrationBtn);

            //Boosters
            if (GetUserInfo.userGroup.Contains("admin"))
            {
                MenuController.AddSubmenu(mainMenu, Boosters.GetMenu());

                MenuItem subMenuBoostersBtn = new MenuItem(GetConfig.Langs["MenuBoostersTitle"], " ")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(subMenuBoostersBtn);
                MenuController.BindMenuItem(mainMenu, Boosters.GetMenu(), subMenuBoostersBtn);
            }

            //Notifications
            MenuController.AddSubmenu(mainMenu, Notifications.GetMenu());

            MenuItem subMenuNotificationsBtn = new MenuItem(GetConfig.Langs["MenuNotificationsTitle"], " ")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(subMenuNotificationsBtn);
            MenuController.BindMenuItem(mainMenu, Notifications.GetMenu(), subMenuNotificationsBtn);

            //Teleports
            MenuController.AddSubmenu(mainMenu, Teleports.GetMenu());

            MenuItem subMenuTeleportsBtn = new MenuItem(GetConfig.Langs["MenuTeleportsTitle"], " ")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            mainMenu.AddMenuItem(subMenuTeleportsBtn);
            MenuController.BindMenuItem(mainMenu, Teleports.GetMenu(), subMenuTeleportsBtn);

            //Database
            if (GetUserInfo.userGroup.Contains("admin"))
            {
                MenuController.AddSubmenu(mainMenu, Database.GetMenu());

                MenuItem subMenuDatabaseBtn = new MenuItem(GetConfig.Langs["MenuDatabaseTitle"], " ")
                {
                    RightIcon = MenuItem.Icon.ARROW_RIGHT
                };

                mainMenu.AddMenuItem(subMenuDatabaseBtn);
                MenuController.BindMenuItem(mainMenu, Database.GetMenu(), subMenuDatabaseBtn);
            }


        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return mainMenu;
        }
    }
}
