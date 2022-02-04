using MenuAPI;
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


            notificationsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["PrivateMessageTitle"], GetConfig.Langs["PrivateMessageDesc"])
            {
                Enabled = true,
            });
            notificationsMenu.AddMenuItem(new MenuItem(GetConfig.Langs["BroadcastMessageTitle"], GetConfig.Langs["BroadcastMessageDesc"])
            {
                Enabled = true,
            });

            notificationsMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    dynamic idPlayer = await UtilsFunctions.GetInput(GetConfig.Langs["PrivateMessageTitle"], GetConfig.Langs["ID"]);
                    MainMenu.args.Add(idPlayer);
                    dynamic message = await UtilsFunctions.GetInput(GetConfig.Langs["PrivateMessageTitle"], GetConfig.Langs["PMDesc"]);
                    MainMenu.args.Add(message);
                    NotificationFunctions.PrivateMessage(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 1)
                {
                    dynamic message = await UtilsFunctions.GetInput(GetConfig.Langs["BroadcastMessageTitle"], GetConfig.Langs["BMDesc"]);
                    MainMenu.args.Add(message);
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
