using System;
using System.Collections.Generic;
using vorpadminmenu_cl.Functions.Utils;
using vorpadminmenu_cl.Menus.Players.Inventory;

namespace vorpadminmenu_cl.Functions.Database
{
    class DatabaseFunctions : BaseScript
    {
        public DatabaseFunctions()
        {
            EventHandlers["vorp:loadPlayerInventory"] += new Action<dynamic>(LoadPlayerInventory);
        }



        public static void SetupDatabase()
        {
            if (GetUserInfo.userGroup.Contains("admin"))
            {
                API.RegisterCommand(GetConfig.Config["AddMoney"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
                {
                    AddMoney(args);
                }), false);
                API.RegisterCommand(GetConfig.Config["DelMoney"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
                {
                    RemoveMoney(args);
                }), false);
                API.RegisterCommand(GetConfig.Config["AddXp"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
                {
                    AddXp(args);
                }), false);
                API.RegisterCommand(GetConfig.Config["DelXp"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
                {
                    RemoveXp(args);
                }), false);
                API.RegisterCommand(GetConfig.Config["AddItem"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
                {
                    AddItem(args);
                }), false);

                API.RegisterCommand(GetConfig.Config["AddWeapon"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
                {
                    AddWeapon(args);
                }), false);

                API.RegisterCommand(GetConfig.Config["AddAmmo"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
                {
                    AddAmmo(args);
                }), false);
            }
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
            if (args.Count == 3)
            {
                TriggerServerEvent("vorp:adminAddItem", args);
            }
        }

        public static void DelItem(List<object> args)
        {
            if (args.Count == 3)
            {
                TriggerServerEvent("vorp:adminDelItem", args);
            }
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
                    string[] weaponNameSubdivided = weaponName.Split('_');
                    foreach (var a in AmmoList.ammo)
                    {
                        if (a.Contains(weaponNameSubdivided[1]))
                        {
                            int ammoType = API.GetHashKey(a);
                            API.SetPedAmmoByType(API.PlayerPedId(), ammoType, 200);
                        }
                    }
                }
            }
        }


        public static void GetInventoryItems(List<object> args)
        {
            TriggerServerEvent("vorp:getInventory", args);
        }

        private void LoadPlayerInventory(dynamic items)
        {
            Inventory.LoadItems(items);
        }


    }
}