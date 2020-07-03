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

            databaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["AddMoneyTitle"], GetConfig.Langs["AddMoneyDesc"])
            {
                Enabled = true,
            });
            databaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["DelMoneyTitle"], GetConfig.Langs["DelMoneyDesc"])
            {
                Enabled = true,
            });
            databaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["AddXpTitle"], GetConfig.Langs["AddXpDesc"])
            {
                Enabled = true,
            });
            databaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["DelXpTitle"], GetConfig.Langs["DelXpDesc"])
            {
                Enabled = true,
            });

            databaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["AddItemTitle"], GetConfig.Langs["AddItemDesc"])
            {
                Enabled = true,
            });
            databaseMenu.AddMenuItem(new MenuItem(GetConfig.Langs["AddWeaponTitle"], GetConfig.Langs["AddWeaponDesc"])
            {
                Enabled = true,
            });

            databaseMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 1)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["DelMoneyTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    dynamic type = await UtilsFunctions.GetInput(GetConfig.Langs["TypeOfMoneyTitle"], GetConfig.Langs["TypeOfMoneyDesc"]);
                    MainMenu.args.Add(type);
                    dynamic quantity = await UtilsFunctions.GetInput(GetConfig.Langs["Quantity"], GetConfig.Langs["Quantity"]);
                    MainMenu.args.Add(quantity);
                    DatabaseFunctions.AddMoney(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 2)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["DelMoneyTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    dynamic type = await UtilsFunctions.GetInput(GetConfig.Langs["TypeOfMoneyTitle"], GetConfig.Langs["TypeOfMoneyDesc"]);
                    MainMenu.args.Add(type);
                    dynamic quantity = await UtilsFunctions.GetInput(GetConfig.Langs["Quantity"], GetConfig.Langs["Quantity"]);
                    MainMenu.args.Add(quantity);
                    DatabaseFunctions.RemoveMoney(MainMenu.args);
                    MainMenu.args.Clear();
                }
                if (_index == 3)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["AddXpTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    dynamic quantity = await UtilsFunctions.GetInput(GetConfig.Langs["Quantity"], GetConfig.Langs["Quantity"]);
                    MainMenu.args.Add(quantity);
                    DatabaseFunctions.AddXp(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 4)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["AddXpTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    dynamic quantity = await UtilsFunctions.GetInput(GetConfig.Langs["Quantity"], GetConfig.Langs["Quantity"]);
                    MainMenu.args.Add(quantity);
                    DatabaseFunctions.RemoveXp(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 5)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["ID"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    dynamic item = await UtilsFunctions.GetInput(GetConfig.Langs["ItemName"], GetConfig.Langs["ItemName"]);
                    MainMenu.args.Add(item);
                    dynamic quantity = await UtilsFunctions.GetInput(GetConfig.Langs["Quantity"], GetConfig.Langs["Quantity"]);
                    MainMenu.args.Add(quantity);
                    DatabaseFunctions.AddItem(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 6)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["ID"], GetConfig.Langs["ID"]);
                    dynamic weaponName = await UtilsFunctions.GetInput(GetConfig.Langs["WeaponName"], GetConfig.Langs["WeaponName"]);
                    dynamic ammoName = await UtilsFunctions.GetInput(GetConfig.Langs["Weaponammo"], GetConfig.Langs["Weaponammo"]);
                    dynamic ammoQuantity = await UtilsFunctions.GetInput(GetConfig.Langs["Quantity"], GetConfig.Langs["Quantity"]);
                    MainMenu.args.Add(idPlayer);
                    MainMenu.args.Add(weaponName);
                    MainMenu.args.Add(ammoName);
                    MainMenu.args.Add(ammoQuantity);
                    DatabaseFunctions.AddWeapon(MainMenu.args);
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
