using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions.Notifications;
using vorpadminmenu_cl.Functions.Utils;

namespace vorpadminmenu_cl.Menus
{
    class Notifications
    {
        private static Menu notificationsMenu = new Menu(GetConfig.Langs["MenuNotificationsTitle"], GetConfig.Langs["MenuNotificationsDesc"]);
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(notificationsMenu);


            notificationsMenu.AddMenuItem(new MenuItem("Pm", "Press here to send a private message or Command:/pm id message")
            {
                Enabled = true,
            });
            notificationsMenu.AddMenuItem(new MenuItem("Bc", "Press here to send a broadcast message or Command:/bc id message")
            {
                Enabled = true,
            });

            notificationsMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    MainMenu.args = await UtilsFunctions.GetTwoByNUI(MainMenu.args, "Send to", "id", "Send", "message");
                    NotificationFunctions.PrivateMessage(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 1)
                {
                    MainMenu.args = await UtilsFunctions.GetOneByNUI(MainMenu.args, "Send broadcast", "message");
                    NotificationFunctions.BroadCast(MainMenu.args);
                    MainMenu.args.Clear();
                }
            };

        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return notificationsMenu;
        }
    }
}
