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
        private static MenuCheckboxItem pfollow = new MenuCheckboxItem(GetConfig.Langs["PlayersBlipsTitle"], GetConfig.Langs["PlayersBlipsDesc"], false) {
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
            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["BanPlayerTitle"], GetConfig.Langs["BanPlayerDesc"])
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
            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["SpectateTitle"], GetConfig.Langs["SpectateDesc"])
            {
                Enabled = true,
            });
            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["SpectateTitleOff"], GetConfig.Langs["SpectateDescOff"])
            {
                Enabled = true,
            });
            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["ReviveTitle"], GetConfig.Langs["ReviveDesc"])
            {
                Enabled = true,
            });
            administrationMenu.AddMenuItem(new MenuItem(GetConfig.Langs["HealTitle"], GetConfig.Langs["HealDesc"])
            {
                Enabled = true,
            });
            administrationMenu.AddMenuItem(pfollow);

            
            

            administrationMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 1)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["KickPlayerTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    AdministrationFunctions.Kick(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 2)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["BanPlayerTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    dynamic time = await UtilsFunctions.GetInput(GetConfig.Langs["BanPlayerTitle"], GetConfig.Langs["BanPlayerTime"]);
                    MainMenu.args.Add(time);
                    dynamic reason = await UtilsFunctions.GetInput(GetConfig.Langs["BanPlayerTitle"], GetConfig.Langs["BanPlayerReason"]);
                    MainMenu.args.Add(reason);
                    AdministrationFunctions.Ban(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 3)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["FreezeTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    AdministrationFunctions.StopPlayer(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 4)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["SlapTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    AdministrationFunctions.Slap(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 5)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["LightningTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    AdministrationFunctions.ThorToId(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 6)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["FireTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    AdministrationFunctions.FireToId(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 7)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["SpectateTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    AdministrationFunctions.Spectate(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 8)
                {
                    AdministrationFunctions.SpectateOff(MainMenu.args);
                }
                else if (_index == 9)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["ReviveTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    AdministrationFunctions.Revive(MainMenu.args);
                }
                else if (_index == 10)
                {

                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["HealTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    AdministrationFunctions.Heal(MainMenu.args);
                }
            };
            administrationMenu.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 11)
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
