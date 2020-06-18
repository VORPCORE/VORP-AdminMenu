using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions.Utils;
using vorpadminmenu_cl.Functions.Administration;

namespace vorpadminmenu_cl.Menus
{
    class Administration
    {
        private static Menu administrationMenu = new Menu(GetConfig.Langs["MenuAdministrationTitle"], GetConfig.Langs["MenuAdministrationDesc"]);
        private static MenuCheckboxItem pfollow = new MenuCheckboxItem("Players in map", "Press here to see all players in map or Command:/cblip", false) {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(administrationMenu);
           
            //Administration
            MenuController.AddSubmenu(administrationMenu, Players.Players.GetMenu());
           
            MenuItem subMenuPlayersBtn = new MenuItem(GetConfig.Langs["PlayersListTitle"], " ")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };
           
            administrationMenu.AddMenuItem(subMenuPlayersBtn);
            MenuController.BindMenuItem(administrationMenu, Players.Players.GetMenu(), subMenuPlayersBtn);

            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["KickPlayerTitle"], GetConfig.Langs["KickPlayerDesc"])
            {
                Enabled = true,
            });
            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["FreezeTitle"], GetConfig.Langs["FreezeDesc"])
            {
                Enabled = true,
            });
            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["SlapTitle"], GetConfig.Langs["SlapDesc"])
            {
                Enabled = true,
            });
            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["LightningTitle"], GetConfig.Langs["LightningDesc"])
            {
                Enabled = true,
            });
            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["FireTitle"], GetConfig.Langs["FireDesc"])
            {
                Enabled = true,
            });
            administrationMenu.AddMenuItem(pfollow);

            administrationMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 1)
                {
                    MainMenu.args = await UtilsFunctions.GetOneByNUI(MainMenu.args, "Kick", "Id player");
                    AdministrationFunctions.Kick(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 2)
                {
                    MainMenu.args = await UtilsFunctions.GetOneByNUI(MainMenu.args, "Freeze", "Id player");
                    AdministrationFunctions.StopPlayer(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 3)
                {
                    MainMenu.args = await UtilsFunctions.GetOneByNUI(MainMenu.args, "Slap", "Id player");
                    AdministrationFunctions.Slap(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 4)
                {
                    MainMenu.args = await UtilsFunctions.GetOneByNUI(MainMenu.args, "Lightning", "Id player");
                    AdministrationFunctions.ThorToId(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 5)
                {
                    MainMenu.args = await UtilsFunctions.GetOneByNUI(MainMenu.args, "Burn", "Id player");
                    AdministrationFunctions.FireToId(MainMenu.args);
                    MainMenu.args.Clear();
                }
            };
            administrationMenu.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 6)
                {
                    if (!_checked) {
                        AdministrationFunctions.ClearBlips();
                    };
                }
               
            };

        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return administrationMenu;
        }

        public static bool GetPFollow()
        {
            return pfollow.Checked;
        }

        public static void SetPFollow(bool pFollow)
        {
            pfollow.Checked = pFollow;
        }
    }
}
