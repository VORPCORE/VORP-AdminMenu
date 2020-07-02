using CitizenFX.Core.Native;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions;
using vorpadminmenu_cl.Functions.Database;

namespace vorpadminmenu_cl.Menus.Players.Inventory
{
    class Inventory
    {
        private static Menu inventory = new Menu(GetConfig.Langs["InventoryListTitle"], GetConfig.Langs["InventoryListDesc"]);
        private static int indexItem;
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(inventory);

            inventory.OnMenuOpen += (_menu) =>
            {
                inventory.ClearMenuItems();
                int idPlayerInventory = API.GetPlayerServerId(PlayersDatabase.idPlayers.ElementAt(PlayersDatabase.indexPlayer));
                MainMenu.args.Add(idPlayerInventory);
                DatabaseFunctions.GetInventoryItems(MainMenu.args);
                MainMenu.args.Clear();
                inventory.AddMenuItem(new MenuItem(GetConfig.Langs["LoadingTitle"], GetConfig.Langs["LoadingDesc"])
                {
                    Enabled = true,
                });
            };

            inventory.OnItemSelect += (_menu, _item, _index) =>
            {
                indexItem = _index;
                

            };


        }

        public static void LoadItems(dynamic items)
        {
            inventory.ClearMenuItems();
            foreach (var i in items)
            {
                inventory.AddMenuItem(new MenuItem(i.label + " " + i.count.ToString(), i.name)
                {
                    Enabled = true,
                });
            }
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return inventory;
        }

        
    }
}
