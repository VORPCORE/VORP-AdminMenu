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
        private static List<int> idPlayers = new List<int>();
        private static int indexPlayer;
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
        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return playersListDatabaseMenu;
        }
    }
}
