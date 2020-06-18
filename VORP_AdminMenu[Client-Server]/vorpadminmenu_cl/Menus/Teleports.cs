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
        private static MenuCheckboxItem tpview = new MenuCheckboxItem("Tp to cursor", "Teleport to cursor.\nPress checkbox to enable or: Command:/tpview \nUse mouse3 to tp", false) {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(teleportsMenu);
            
            

            teleportsMenu.AddMenuItem(new MenuItem("Tp to Waypoint", "Teleport to wayPoint.\nAdd a waypoint on the map and press here or:\n Command: /tpwayp")
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem("Tp to Coords", "Teleport to coords.\nPress here and enter the coords or:\n Command:/tpcoords coordX coordY (coords whit decimals)")
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem("Tp to player", "Teleport to player.\nPress here and enter the id of the player or:\n Command:/tpplayer id")
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem("Bring player", "Bring player.\nPress here and enter the id of the player or:\n Command:/tpbring id")
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem("Go back to first tp", "Teleport back to the first tp and delete the blip in the map.\nPress here or:\n Command:/tpback")
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem("Delete coords of first tp", "Delete the mark and position of the first tp position.\nPress here or:\n Command:/delback")
            {
                Enabled = true,
            });
            teleportsMenu.AddMenuItem(new MenuItem("Guarma", "Teleport to guarma map.\nPress here or: Command:/guarma\n Use again to come back to the normal map")
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
                    MainMenu.args = await UtilsFunctions.GetTwoByNUI(MainMenu.args, "X Coord", "0.0", "Y Coord", "0.0");
                    TeleportsFunctions.TpToCoords(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 2)
                {
                    MainMenu.args = await UtilsFunctions.GetOneByNUI(MainMenu.args, "Id player", "id player");
                    TeleportsFunctions.TpToPlayer(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 3)
                {
                    MainMenu.args = await UtilsFunctions.GetOneByNUI(MainMenu.args, "Id player", "id player");
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
          /*  teleportsMenu.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 7)
                {
                    Teleports. = _checked;
                }
                //poner el checked en true o false
            };*/

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
