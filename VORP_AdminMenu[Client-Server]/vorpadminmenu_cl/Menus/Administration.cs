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
           // administrationMenu.AddMenuItem(new MenuCheckboxItem("Players in map", "Press here to see all players in map or Command:/cblip", MethodsPlayerAdministration.playersFollow)
           // {
           //     Style = MenuCheckboxItem.CheckboxStyle.Tick
           // });
           //
           // Menu bansList = new Menu("Bans", "");
           // MenuController.AddSubmenu(administration, bansList);
           //
           // MenuItem bansButton = new MenuItem("Bans", "")
           // {
           //     RightIcon = MenuItem.Icon.ARROW_RIGHT
           // };
           // administration.AddMenuItem(bansButton);
           // MenuController.BindMenuItem(administration, bansList, bansButton);
           //
           // bansList.AddMenuItem(new MenuItem("Fast ban", "Press here to do a fast ban or: Command:/ban id reason.")
           // {
           //     Enabled = true,
           // });
           // bansList.AddMenuItem(new MenuCheckboxItem("Delete ban", "Press here to activate the delete mode", MethodsPlayerAdministration.deleteOn)
           // {
           //     Style = MenuCheckboxItem.CheckboxStyle.Tick
           // });
           //
           // TriggerServerEvent("vorp:callbans");
           // await Delay(250);
           // foreach (string s in MethodsPlayerAdministration.savedbans)
           // {
           //     string[] sBan = s.Split(',');
           //     bansList.AddMenuItem(new MenuItem(sBan[0], sBan[1])
           //     {
           //         Enabled = true,
           //     });
           // }


        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return administrationMenu;
        }
    }
}
