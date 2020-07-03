using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions.Teleports;
using vorpadminmenu_cl.Functions.Utils;

namespace vorpadminmenu_cl.Menus
{
    class Teleports
    {
        private static Menu teleportsMenu = new Menu(GetConfig.Langs["MenuTeleportsTitle"], GetConfig.Langs["MenuTeleportsDesc"]);
        private static bool setupDone = false;
        private static MenuCheckboxItem tpview = new MenuCheckboxItem(GetConfig.Langs["TpToCursorTitle"], GetConfig.Langs["TpToCursorDesc"], false) {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(teleportsMenu);
            
            

            teleportsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["TpToWaypointTitle"], GetConfig.Langs["TpToWaypointDesc"])
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["TpToCoordsTitle"], GetConfig.Langs["TpToCoordsDesc"])
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["TpToPlayerTitle"], GetConfig.Langs["TpToPlayerDesc"])
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["BringPlayerTitle"], GetConfig.Langs["BringPlayerDesc"])
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["TpBackTitle"], GetConfig.Langs["TpBackDesc"])
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["DelBackTitle"], GetConfig.Langs["DelBackDesc"])
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["GuarmaTitle"], GetConfig.Langs["GuarmaDesc"])
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(tpview);

            teleportsMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    TeleportsFunctions.TpToWaypoint(MainMenu.args);
                }
                else if (_index == 1)
                {
                    dynamic X = await UtilsFunctions.GetInput(GetConfig.Langs["XCoord"], "0.0");
                    MainMenu.args.Add(X);
                    dynamic Y = await UtilsFunctions.GetInput(GetConfig.Langs["YCoord"], "0.0");
                    MainMenu.args.Add(Y);
                    TeleportsFunctions.TpToCoords(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 2)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["ID"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    TeleportsFunctions.TpToPlayer(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 3)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["ID"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    TeleportsFunctions.TpBring(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 4)
                {
                    TeleportsFunctions.TpBack(MainMenu.args);
                }
                else if (_index == 5)
                {
                    TeleportsFunctions.DelBack(MainMenu.args);
                }
                else if (_index == 6)
                {
                    TeleportsFunctions.Guarma(MainMenu.args);
                }
            };
        }


        public static Menu GetMenu()
        {
            SetupMenu();
            return teleportsMenu;
        }

        public static bool GetTpView()
        {
            return tpview.Checked;
        }

        public static void SetTpView(bool tpView)
        {
            tpview.Checked = tpView;
        }
    }
}
