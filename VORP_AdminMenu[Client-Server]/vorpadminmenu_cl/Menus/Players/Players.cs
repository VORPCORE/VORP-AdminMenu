using CitizenFX.Core.Native;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Menus.Players
{
    class Players
    {
        private static Menu playersListMenu = new Menu(GetConfig.Langs["PlayersListTitle"], GetConfig.Langs["PlayersListDesc"]);
        private static Menu playersOptionsMenu = new Menu("", GetConfig.Langs["PlayersListDesc"]);
        private static List<int> idPlayers = new List<int>();
        private static int indexPlayer;
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(playersListMenu);

            playersListMenu.OnMenuOpen += (_menu) =>
            {
                playersListMenu.ClearMenuItems();
                idPlayers.Clear();
                foreach (var i in API.GetActivePlayers())
                {
                    string name = API.GetPlayerName(i).ToString();
                    string id = API.GetPlayerServerId(i).ToString();
                    idPlayers.Add(i);
                    MenuController.AddSubmenu(playersListMenu, playersOptionsMenu);

                    MenuItem playerNameButton = new MenuItem(name, $"{name},{id}")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    playersListMenu.AddMenuItem(playerNameButton);
                    MenuController.BindMenuItem(playersListMenu, playersOptionsMenu, playerNameButton);
                    
                }
            };

            playersListMenu.OnItemSelect += (_menu, _item, _index) =>
            {
                indexPlayer = _index;
                playersOptionsMenu.MenuTitle = API.GetPlayerName(idPlayers.ElementAt(indexPlayer)) + "," + API.GetPlayerServerId((idPlayers.ElementAt(indexPlayer)));
                
            };

            playersOptionsMenu.AddMenuItem(new MenuItem("Tp to the player", "Press here to tp to the player or: Command:/tptoplayer id.")
            {
                Enabled = true,
            });

            playersOptionsMenu.AddMenuItem(new MenuItem("Bring the player", "Press here to bring the player or: Command:/slap id.")
            {
                Enabled = true,
            });

            playersOptionsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["FreezeTitle"], GetConfig.Langs["FreezeDesc"])
            {
                Enabled = true,
            });

            playersOptionsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["SlapTitle"], GetConfig.Langs["SlapDesc"])
            {
                Enabled = true,
            });
            playersOptionsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["LightningTitle"], GetConfig.Langs["LightningDesc"])
            {
                Enabled = true,
            });
            playersOptionsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["FireTitle"], GetConfig.Langs["FireDesc"])
            {
                Enabled = true,
            });
            playersOptionsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["KickPlayerTitle"], GetConfig.Langs["KickPlayerDesc"])
            {
                Enabled = true,
            });
            playersOptionsMenu.AddMenuItem(new MenuItem("Ban", "Press here to ban a player: Command:/sban id.")
            {
                Enabled = true,
            });



            playersOptionsMenu.OnItemSelect += (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    MainMenu.args.Add(API.GetPlayerServerId(idPlayers.ElementAt(indexPlayer)));
                    //AdminControl.executeAdminCommand("TpToPlayer", MainMenu.args, "MethodsTeleports");
                    MainMenu.args.Clear();
                }
                else if (_index == 1)
                {
                    MainMenu.args.Add(API.GetPlayerServerId(idPlayers.ElementAt(indexPlayer)));
                    //AdminControl.executeAdminCommand("TpBring", MainMenu.args, "MethodsTeleports");
                    MainMenu.args.Clear();
                }
                if (_index == 2)
                {
                    MainMenu.args.Add(API.GetPlayerServerId(idPlayers.ElementAt(indexPlayer)));
                    //AdminControl.executeAdminCommand("StopPlayer", MainMenu.args, "MethodsPlayerAdministration");
                    MainMenu.args.Clear();
                }
                else if (_index == 3)
                {
                    MainMenu.args.Add(API.GetPlayerServerId(idPlayers.ElementAt(indexPlayer)));
                    //AdminControl.executeAdminCommand("Slap", MainMenu.args, "MethodsPlayerAdministration");
                    MainMenu.args.Clear();
                }
                else if (_index == 4)
                {
                    MainMenu.args.Add(API.GetPlayerServerId(idPlayers.ElementAt(indexPlayer)));
                    //AdminControl.executeAdminCommand("ThorToId", MainMenu.args, "MethodsBoosters");
                    MainMenu.args.Clear();
                }
                else if (_index == 5)
                {
                    MainMenu.args.Add(API.GetPlayerServerId(idPlayers.ElementAt(indexPlayer)));
                    //AdminControl.executeAdminCommand("FireToId", MainMenu.args, "MethodsBoosters");
                    MainMenu.args.Clear();
                }
                else if (_index == 6)
                {
                    MainMenu.args.Add(API.GetPlayerServerId(idPlayers.ElementAt(indexPlayer)));
                    //AdminControl.executeAdminCommand("Kick", MainMenu.args, "MethodsPlayerAdministration");
                    MainMenu.args.Clear();
                }
                else if (_index == 7)
                {
                    MainMenu.args.Add(API.GetPlayerServerId(idPlayers.ElementAt(indexPlayer)));
                    string reason = "Banned by Staff.";
                    MainMenu.args.Add(reason);
                    //AdminControl.executeAdminCommand("Sbans", MainMenu.args, "MethodsPlayerAdministration");
                    MainMenu.args.Clear();
                }
            };
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return playersListMenu;
        }
    }
}
