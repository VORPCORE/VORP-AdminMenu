using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions.Database;
using vorpadminmenu_cl.Functions.Utils;

namespace vorpadminmenu_cl.Menus
{
    class Database
    {
        private static Menu databaseMenu = new Menu(GetConfig.Langs["MenuDatabaseTitle"], GetConfig.Langs["MenuDatabaseDesc"]);
        private static bool setupDone = false;

        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(databaseMenu);

            MenuController.AddSubmenu(databaseMenu, Players.PlayersDatabase.GetMenu());

            MenuItem subMenuPlayersDatabaseBtn = new MenuItem(GetConfig.Langs["PlayersListTitle"], " ")
            {
                RightIcon = MenuItem.Icon.ARROW_RIGHT
            };

            databaseMenu.AddMenuItem(subMenuPlayersDatabaseBtn);
            MenuController.BindMenuItem(databaseMenu, Players.PlayersDatabase.GetMenu(), subMenuPlayersDatabaseBtn);

            databaseMenu.AddMenuItem(new MenuItem("AddMoney", "Press here to")
            {
                Enabled = true,
            });
            databaseMenu.AddMenuItem(new MenuItem("RemoveMoney", "Press here to")
            {
                Enabled = true,
            });
            databaseMenu.AddMenuItem(new MenuItem("AddXp", "Press here to ")
            {
                Enabled = true,
            });
            databaseMenu.AddMenuItem(new MenuItem("RemoveXp", "Press here ")
            {
                Enabled = true,
            });

            databaseMenu.AddMenuItem(new MenuItem("AddItem", "Press here to ")
            {
                Enabled = true,
            });
            databaseMenu.AddMenuItem(new MenuItem("RemoveItem", "Press here to ")
            {
                Enabled = true,
            });
            databaseMenu.AddMenuItem(new MenuItem("AddWeapon", "Press here to ")
            {
                Enabled = true,
            });

            databaseMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 1)
                {
                    MainMenu.args = await UtilsFunctions.GetThreeByNUI(MainMenu.args, "Addmoney", "id", "Type:0-dollar,1-gold,2-rolpoints", "type", "Quantity", "quantity");
                    DatabaseFunctions.AddMoney(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 2)
                {
                    MainMenu.args = await UtilsFunctions.GetThreeByNUI(MainMenu.args, "DelMoney", "id", "Type:0-dollar,1-gold,2-rolpoints", "type", "Quantity", "quantity");
                    DatabaseFunctions.RemoveMoney(MainMenu.args);
                    MainMenu.args.Clear();
                }
                if (_index == 3)
                {
                    MainMenu.args = await UtilsFunctions.GetTwoByNUI(MainMenu.args, "AddXp", "id", "Quantity", "quantity");
                    DatabaseFunctions.AddXp(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 4)
                {
                    MainMenu.args = await UtilsFunctions.GetTwoByNUI(MainMenu.args, "DelXp", "id", "Quantity", "quantity");
                    DatabaseFunctions.RemoveXp(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 5)
                {
                    int idPlayer = await UtilsFunctions.GetInput("Player id", "id");
                    string item = await UtilsFunctions.GetInput("Item Name", "name");
                    string quantity = await UtilsFunctions.GetInput("Quantity", "quantity");
                    MainMenu.args.Add(idPlayer);
                    MainMenu.args.Add(item);
                    MainMenu.args.Add(quantity);
                    DatabaseFunctions.AddItem(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 6)
                {
                    int idPlayer = await UtilsFunctions.GetInput("Player id", "id");
                    string weaponName = await UtilsFunctions.GetInput("Weapon Name", "name");
                    string ammoName = await UtilsFunctions.GetInput("Weapon ammo", "weapon ammo");
                    int ammoQuantity = await UtilsFunctions.GetInput("Ammo quantity", "quantity");
                    MainMenu.args.Add(idPlayer);
                    MainMenu.args.Add(weaponName);
                    MainMenu.args.Add(ammoName);
                    MainMenu.args.Add(ammoQuantity);
                    DatabaseFunctions.AddWeapon(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 7)
                {
                    MainMenu.args = await UtilsFunctions.GetTwoByNUI(MainMenu.args, "AddXp", "id", "Quantity", "quantity");
                    DatabaseFunctions.RemoveXp(MainMenu.args);
                    MainMenu.args.Clear();
                }
            };
        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return databaseMenu;
        }
    }
}
