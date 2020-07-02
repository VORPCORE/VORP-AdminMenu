using CitizenFX.Core.Native;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Menus.Players
{

    class PlayersDatabase
    {
        private static Menu playersListDatabaseMenu = new Menu(GetConfig.Langs["PlayersListTitle"], GetConfig.Langs["PlayersListDesc"]);
        private static Menu playersOptionsDatabaseMenu = new Menu("", GetConfig.Langs["PlayersListDesc"]);
        public static List<int> idPlayers = new List<int>();
        public static int indexPlayer;
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(playersListDatabaseMenu);


            playersListDatabaseMenu.OnMenuOpen += (_menu) =>
            {
                playersListDatabaseMenu.ClearMenuItems();
                idPlayers.Clear();
                foreach (var i in API.GetActivePlayers())
                {
                    string name = API.GetPlayerName(i).ToString();
                    string id = API.GetPlayerServerId(i).ToString();
                    idPlayers.Add(i);
                    MenuController.AddSubmenu(playersListDatabaseMenu, playersOptionsDatabaseMenu);

                    MenuItem playerNameDatabaseButton = new MenuItem(name, $"{name},{id}")
                    {
                        RightIcon = MenuItem.Icon.ARROW_RIGHT
                    };
                    playersListDatabaseMenu.AddMenuItem(playerNameDatabaseButton);
                    MenuController.BindMenuItem(playersListDatabaseMenu, playersOptionsDatabaseMenu, playerNameDatabaseButton);

                    
                }
            };
            playersListDatabaseMenu.OnItemSelect += (_menu, _item, _index) =>
            {
                indexPlayer = _index;
                playersOptionsDatabaseMenu.MenuTitle = API.GetPlayerName(idPlayers.ElementAt(indexPlayer)) + "," + API.GetPlayerServerId((idPlayers.ElementAt(indexPlayer)));

            };

            playersOptionsDatabaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["AddMoneyTitle"], GetConfig.Langs["AddMoneyDesc"])
            {
                Enabled = true,
            });
            playersOptionsDatabaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["DelMoneyTitle"], GetConfig.Langs["DelMoneyDesc"])
            {
                Enabled = true,
            });
            playersOptionsDatabaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["AddXpTitle"], GetConfig.Langs["AddXpDesc"])
            {
                Enabled = true,
            });
            playersOptionsDatabaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["DelXpTitle"], GetConfig.Langs["DelXpDesc"])
            {
                Enabled = true,
            });
            playersOptionsDatabaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["AddItemTitle"], GetConfig.Langs["AddItemDesc"])
            {
                Enabled = true,
            });

            MenuController.AddSubmenu(playersOptionsDatabaseMenu, Inventory.Inventory.GetMenu());

            MenuItem subMenuInventoryBtn = new MenuItem(GetConfig.Langs["InventoryTitle"], " ")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            playersOptionsDatabaseMenu.AddMenuItem(subMenuInventoryBtn);
            MenuController.BindMenuItem(playersOptionsDatabaseMenu, Inventory.Inventory.GetMenu(), subMenuInventoryBtn);

        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return playersListDatabaseMenu;
        }
    }
}
