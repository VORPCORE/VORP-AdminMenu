using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                
            };

            inventory.OnItemSelect += (_menu, _item, _index) =>
            {
                indexItem = _index;
                

            };


        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return inventory;
        }
    }
}
