using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Functions.Notifications
{
    class NotificationFunctions : BaseScript
    {

        public static void SetupNotifications()
        {
            API.RegisterCommand(GetConfig.Config["PMessage"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                PrivateMessage(args);
            }), false);

            API.RegisterCommand(GetConfig.Config["BMessage"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                BroadCast(args);
            }), false);
        }

        public static void PrivateMessage(List<object> args)
        {
            string message = "";
            int id = int.Parse(args[0].ToString());
            for (int i = 1; i < args.Count; i++)
            {
                message += args[i].ToString() + " ";

            }

            TriggerServerEvent("vorp:privateMessage", id, message);
        }

        public static void BroadCast(List<object> args)
        {
            string message = "";
            for (int i = 0; i < args.Count; i++)
            {
                message += args[i].ToString() + " ";
            }

            TriggerServerEvent("vorp:broadCastMessage", message);
        }
    }
}
