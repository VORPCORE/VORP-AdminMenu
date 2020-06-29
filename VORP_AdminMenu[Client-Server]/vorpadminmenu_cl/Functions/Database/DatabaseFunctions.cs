using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_cl.Functions.Database
{
    class DatabaseFunctions : BaseScript
    {
        public static void SetupDatabase()
        {
            API.RegisterCommand("money", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AddMoney(args);
            }), false);

            API.RegisterCommand("delmoney", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                RemoveMoney(args);
            }), false);
            API.RegisterCommand("xp", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AddXp(args);
            }), false);
            API.RegisterCommand("delxp", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                RemoveXp(args);
            }), false);
        }

        public static void AddMoney(List<object> args)
        {
            TriggerServerEvent("vorp:adminAddMoney", args);
        }

        public static void RemoveMoney(List<object> args)
        {
            TriggerServerEvent("vorp:adminRemoveMoney", args);
        }

        public static void AddXp(List<object> args)
        {
            TriggerServerEvent("vorp:adminAddXp", args);
        }

        public static void RemoveXp(List<object> args)
        {
            TriggerServerEvent("vorp:adminRemoveXp", args);
        }
    }
}