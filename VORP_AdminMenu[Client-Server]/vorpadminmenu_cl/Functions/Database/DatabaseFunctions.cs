using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions.Utils;

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
            API.RegisterCommand("addxp", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AddXp(args);
            }), false);
            API.RegisterCommand("delxp", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                RemoveXp(args);
            }), false);
            API.RegisterCommand("additem", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AddItem(args);
            }), false);
            
            API.RegisterCommand("addweapon", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AddWeapon(args);
            }), false);

            API.RegisterCommand("addammo", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                AddAmmo(args);
            }), false);

            API.RegisterCommand("infammon", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                InfiniteAmmo(args);
            }), false);
            API.RegisterCommand("infammoff", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                InfiniteAmmoOff(args);
            }), false);

            API.RegisterCommand("horse", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Horse(args);
            }), false);
            API.RegisterCommand("veh", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Vehicle(args);
            }), false);
            
            
            //API.RegisterCommand("delitem", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            //{
            //    RemoveXp(args);
            //}), false);
            //API.RegisterCommand("delweapon", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            //{
            //    RemoveXp(args);
            //}), false);
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

        public static void AddItem(List<object> args)
        {
            TriggerServerEvent("vorp:adminAddItem", args);
        }

        public static void AddWeapon(List<object> args)
        {
            TriggerServerEvent("vorp:adminAddWeapon", args);
        }

        public static void AddAmmo(List<object> args)
        {
            uint weaponHash = 0;
            if (API.GetCurrentPedWeapon(API.PlayerPedId(), ref weaponHash, false, 0, false))
            {
                string weaponName = Function.Call<string>((Hash)0x89CF5FF3D363311E, weaponHash);
                if (weaponName.Contains("UNARMED"))
                {
                    TriggerEvent("vorp:Tip", GetConfig.Langs["NeedWeaponOnHand"], 3000);
                }
                else
                {
                    Debug.Write("entro");
                    string[] weaponNameSubdivided = weaponName.Split('_');
                    foreach (var a in AmmoList.ammo)
                    {
                        if (a.Contains(weaponNameSubdivided[1])){
                            int ammoType = API.GetHashKey(a);
                            API.SetPedAmmoByType(API.PlayerPedId(), ammoType, 200);
                        }
                    }
                    
                }
            } 
        }

        public static void InfiniteAmmo(List<object> args)
        {
            uint weaponHash = 0;
            if (API.GetCurrentPedWeapon(API.PlayerPedId(), ref weaponHash, false, 0, false))
            {
                string weaponName = Function.Call<string>((Hash)0x89CF5FF3D363311E, weaponHash);
                if (weaponName.Contains("UNARMED"))
                {
                    TriggerEvent("vorp:Tip", GetConfig.Langs["NeedWeaponOnHand"], 3000);
                }
                else
                {

                    API.SetPedInfiniteAmmo(API.PlayerPedId(), true, weaponHash);
                }
            }
        }

        public static void InfiniteAmmoOff(List<object> args)
        {
            uint weaponHash = 0;
            if (API.GetCurrentPedWeapon(API.PlayerPedId(), ref weaponHash, false, 0, false))
            {
                string weaponName = Function.Call<string>((Hash)0x89CF5FF3D363311E, weaponHash);
                if (weaponName.Contains("UNARMED"))
                {
                    TriggerEvent("vorp:Tip", GetConfig.Langs["NeedWeaponOnHand"], 3000);
                }
                else
                {

                    API.SetPedInfiniteAmmo(API.PlayerPedId(), false, weaponHash);
                }
            }
        }

        private static async Task Horse(List<object> args)
        {
            string ped = args[0].ToString();
            int HashPed = API.GetHashKey(ped);
            Vector3 coords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            await UtilsFunctions.LoadModel(HashPed);
            int pedCreated = API.CreatePed((uint)HashPed, coords.X + 1, coords.Y, coords.Z - 1, 0, true, true, true, true);
            Function.Call((Hash)0x283978A15512B2FE, pedCreated, true);
            Function.Call((Hash)0x028F76B6E78246EB, API.PlayerPedId(), pedCreated, -1, false);
            API.SetModelAsNoLongerNeeded((uint)HashPed);
        }

        private static void Vehicle(List<object> args)
        {
            
        }
    }
}