using CitizenFX.Core;
using CitizenFX.Core.Native;
using MenuAPI;
using System.Collections.Generic;
using System.Linq;
using vorpadminmenu_cl.Functions.Database;

namespace vorpadminmenu_cl.Menus.Players.Inventory
{
    class Inventory
    {
        private static Menu inventory = new Menu(GetConfig.Langs["InventoryListTitle"], GetConfig.Langs["InventoryListDesc"]);
        private static int indexItem;
        private static bool setupDone = false;
        private static dynamic itemInventoryList;
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

            inventory.OnListItemSelect += (_menu, _listItem, _listIndex, _itemIndex) =>
            {
                if (itemInventoryList[_itemIndex].count < _listIndex)
                {
                    int idPlayerInventory = API.GetPlayerServerId(PlayersDatabase.idPlayers.ElementAt(PlayersDatabase.indexPlayer));
                    MainMenu.args.Add(idPlayerInventory);
                    string item = itemInventoryList[_itemIndex].name;
                    MainMenu.args.Add(item);
                    int itemQuantity = _listIndex - itemInventoryList[_itemIndex].count;
                    MainMenu.args.Add(itemQuantity);
                    DatabaseFunctions.AddItem(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else
                {
                    int idPlayerInventory = API.GetPlayerServerId(PlayersDatabase.idPlayers.ElementAt(PlayersDatabase.indexPlayer));
                    MainMenu.args.Add(idPlayerInventory);
                    string item = itemInventoryList[_itemIndex].name;
                    MainMenu.args.Add(item);
                    int itemQuantity = 0;
                    if (_listIndex == 0)
                    {
                        itemQuantity = itemInventoryList[_itemIndex].count;
                    }
                    else
                    {
                        itemQuantity = (itemInventoryList[_itemIndex].count - _listIndex);
                    }

                    MainMenu.args.Add(itemQuantity);
                    DatabaseFunctions.DelItem(MainMenu.args);
                    MainMenu.args.Clear();
                }
            };
        }

        public static void LoadItems(dynamic items)
        {
            inventory.ClearMenuItems();
            itemInventoryList = items;

            foreach (var i in items)
            {
                List<string> itemCount = new List<string>();
                for (var a = 0; a < i.limit + 1; a++)
                {
                    itemCount.Add($"{a}");
                }
                MenuListItem itemList = new MenuListItem(i.label, itemCount, i.count, i.name);
                inventory.AddMenuItem(itemList);
            }
        }

        public static Menu GetMenu()
        {
            SetupMenu();
            return inventory;
        }


    }
}
