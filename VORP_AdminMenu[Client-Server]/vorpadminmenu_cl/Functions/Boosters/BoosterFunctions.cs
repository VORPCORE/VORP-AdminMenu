using CitizenFX.Core;
using CitizenFX.Core.Native;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using vorpadminmenu_cl.Functions.Utils;

namespace vorpadminmenu_cl.Functions.Boosters
{
    public class BoosterFunctions : BaseScript
    {
        private float _speed = 1.28F;
        private static float _heading;

        public BoosterFunctions()
        {
            EventHandlers["vorp:thordone"] += new Action<Vector3>(ThorDone);

            Tick += Noc2;
            Tick += OnLight;
        }

        #region Public Methods
        public static void SetupBoosters()
        {
            // Command line for booster commands
            // Note: Methods registered into commands cannot have optional parameters
            API.RegisterCommand(GetConfig.Config["Horse"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Horse(args);
            }), false);
            
            API.RegisterCommand(GetConfig.Config["Veh"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Vehicle(args);
            }), false);
        }

        public static void Golden()
        {
            int pPedId = API.PlayerPedId();

            //Jugador cores
            Function.Call((Hash)0xC6258F41D86676E0, pPedId, 0, 100);
            Function.Call((Hash)0xC6258F41D86676E0, pPedId, 1, 100);
            Function.Call((Hash)0xC6258F41D86676E0, pPedId, 2, 100);

            //Jugador circles                   
            Function.Call((Hash)0x4AF5A4C7B9157D14, pPedId, 0, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, pPedId, 1, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, pPedId, 2, 5000.0);

            Function.Call((Hash)0xF6A7C08DF2E28B28, pPedId, 1, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, pPedId, 2, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, pPedId, 0, 5000.0);

            int entity = Function.Call<int>(Hash.GET_ENTITY_ATTACHED_TO, pPedId);

            Function.Call((Hash)0x09A59688C26D88DF, entity, 0, 1100);
            Function.Call((Hash)0x09A59688C26D88DF, entity, 1, 1100);
            Function.Call((Hash)0x09A59688C26D88DF, entity, 2, 1100);

            Function.Call((Hash)0x75415EE0CB583760, entity, 0, 1100);
            Function.Call((Hash)0x75415EE0CB583760, entity, 1, 1100);
            Function.Call((Hash)0x75415EE0CB583760, entity, 2, 1100);

            Function.Call((Hash)0x5DA12E025D47D4E5, entity, 0, 10);
            Function.Call((Hash)0x5DA12E025D47D4E5, entity, 1, 10);
            Function.Call((Hash)0x5DA12E025D47D4E5, entity, 2, 10);

            Function.Call((Hash)0x920F9488BD115EFB, entity, 0, 10);
            Function.Call((Hash)0x920F9488BD115EFB, entity, 1, 10);
            Function.Call((Hash)0x920F9488BD115EFB, entity, 2, 10);

            Function.Call((Hash)0xF6A7C08DF2E28B28, entity, 0, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, entity, 1, 5000.0);
            Function.Call((Hash)0xF6A7C08DF2E28B28, entity, 2, 5000.0);

            Function.Call((Hash)0x4AF5A4C7B9157D14, entity, 0, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, entity, 1, 5000.0);
            Function.Call((Hash)0x4AF5A4C7B9157D14, entity, 2, 5000.0);
        }

        public static void GodMode(bool isChecked)
        {
            Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), isChecked);
        }

        public static void NoClipMode(bool isChecked)
        {
            int playerPed = API.PlayerPedId();
            _heading = API.GetEntityHeading(playerPed);

            SetNoClipEntity(playerPed, isChecked);
        }

        public static async Task Horse(List<object> args)
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

        public static async Task Vehicle(List<object> args)
        {
            string veh = args[0].ToString();
            int HashVeh = API.GetHashKey(veh);
            Vector3 coords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            await UtilsFunctions.LoadModel(HashVeh);
            int vehCreated = API.CreateVehicle((uint)HashVeh, coords.X + 1, coords.Y, coords.Z, 0, true, true, false, false);
            SetPedDefaultOutfit(vehCreated);
            
            //Spawn
            Function.Call((Hash)0x283978A15512B2FE, vehCreated, true);
            
            //TaskWanderStandard
            Function.Call((Hash)0xBB9CE077274F6A1B, vehCreated, 10, 10);
            
            //SetPedIntoVehicle
            Function.Call((Hash)0xF75B0D629E1C063D, API.PlayerPedId(), vehCreated, -1, false);
            API.SetModelAsNoLongerNeeded((uint)HashVeh);
        }

        public static void InfiniteAmmo()
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

        public static void InfiniteAmmoOff()
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
        #endregion

        #region Private Methods
        private static void SetNoClipEntity(int playerPed, bool isNoClipMode)
        {
            if (isNoClipMode)
            {
                API.SetEntityVisible(playerPed, false);
                API.FreezeEntityPosition(playerPed, true);
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), true);
            }
            else
            {
                API.SetEntityVisible(playerPed, true);
                API.FreezeEntityPosition(playerPed, false);
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), false);
            }
        }

        [Tick]
        private async Task Noc2()
        {
            if (GetUserInfo.loaded)
            {
                int playerPed = API.PlayerPedId();
                if (Menus.Boosters.GetNoClip())
                {
                    API.SetEntityHeading(playerPed, _heading);
                    if (API.IsControlPressed(0, 0x8FD015D8)) //W
                    {
                        Vector3 a = API.GetGameplayCamRot(0);

                        Vector3 c = new Vector3();
                        if (a.X > 8.0F)
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, _speed, -0.5F);
                            API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                        }
                        else if (a.X < -8.0F)
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, _speed, -1.5F);
                            API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                        }
                        else
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, _speed, -1.0F);
                            API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                        }
                    }

                    if (API.IsControlPressed(0, 0xD9D0E1C0)) //SPACE
                    {
                        Vector3 c = new Vector3();
                        if (_speed > 1.0F)
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -_speed * 2);
                        }
                        else
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -_speed - 1.0F);
                        }
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0x8FFC75D6)) //SHIFT
                    {
                        Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, _speed - 1.0F);
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0xCEFD9220)) //E-more speed
                    {
                        if (_speed > 0.5F)
                        {
                            _speed += 0.5F;
                        }
                    }
                    
                    if (API.IsControlPressed(0, 0xDE794E3E)) //Q-less speed
                    {
                        if (_speed > 0.5)
                        {
                            _speed -= 0.5F;
                        }
                    }
                    
                    if (API.IsControlPressed(0, 0x8CC9CD42)) //X-default speed
                    {
                        _speed = 1.28F;
                    }
                    
                    _heading += API.GetGameplayCamRelativeHeading();
                }
            }
        }

        [Tick]
        private async Task OnLight()
        {
            if (GetUserInfo.loaded)
            {
                int entity = 0;
                bool hit = false;
                Vector3 endCoord = new Vector3();
                Vector3 surfaceNormal = new Vector3();
                Vector3 camCoords = API.GetGameplayCamCoord();
                Vector3 sourceCoords = UtilsFunctions.GetCoordsFromCam(1000.0F);
                int rayHandle = API.StartShapeTestRay(camCoords.X, camCoords.Y, camCoords.Z, sourceCoords.X, sourceCoords.Y, sourceCoords.Z, -1, API.PlayerPedId(), 0);
                API.GetShapeTestResult(rayHandle, ref hit, ref endCoord, ref surfaceNormal, ref entity);
                if (API.IsControlJustPressed(0, 0xCEE12B50) && Menus.Boosters.Gettmode())
                {
                    TriggerServerEvent("vorp:thor", endCoord);
                }

                if (Menus.Boosters.Gettmode())
                {
                    Function.Call((Hash)0x2A32FAA57B937173, -1795314153, endCoord.X, endCoord.Y, endCoord.Z, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.5F, 0.5F, 50.0F, 255, 255, 0, 155, false, false, 2, false, 0, 0, false);
                }
            }
        }

        private void ThorDone(Vector3 endCooord)
        {
            API.ForceLightningFlashAtCoords(endCooord.X, endCooord.Y, endCooord.Z);
        }

        private static int SetPedDefaultOutfit(int coach)
        {
            return Function.Call<int>((Hash)0xAF35D0D2583051B0, coach, true);
        }
        #endregion
    }
}
