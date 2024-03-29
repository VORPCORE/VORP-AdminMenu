﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions.Teleports;

namespace vorpadminmenu_cl.Functions.Utils
{
    class UtilsFunctions : BaseScript
    {

        public static int blip = -1;

        public UtilsFunctions()
        {
            EventHandlers["onResourceStop"] += new Action<string>(OnResourceStop);
        }
        public static async Task TeleportAndFoundGroundAsync(Vector3 tpCoords)
        {
            float groundZ = 0.0F;
            Vector3 normal = new Vector3(1.0f, 1.0f, 1.0f);
            bool foundGround = false;


            for (int i = 1; i < 1000.0; i++)
            {
                API.SetEntityCoords(API.PlayerPedId(), tpCoords.X, tpCoords.Y, (float)i, true, true, true, false);
                foundGround = API.GetGroundZAndNormalFor_3dCoord(tpCoords.X, tpCoords.Y, (float)i, ref groundZ, ref normal);
                await Delay(1);
                if (foundGround == true)
                {
                    API.SetEntityCoords(API.PlayerPedId(), tpCoords.X, tpCoords.Y, (float)i, true, true, true, false);
                    break;
                }
            }
        }

        public static void TeleportToCoords(float x, float y, float z, float heading = 0.0f)
        {
            int playerPedId = API.PlayerPedId();
            heading = API.GetEntityHeading(playerPedId);
            API.SetEntityCoords(playerPedId, x, y, z, true, true, true, false);
            API.SetEntityHeading(playerPedId, heading);
        }

        public static async Task<bool> LoadModel(int hash)
        {
            if (Function.Call<bool>(Hash.IS_MODEL_VALID, hash))
            {
                Function.Call(Hash.REQUEST_MODEL, hash);
                while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, hash))
                {
                    await BaseScript.Delay(100);
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        public static void CreateBlip()
        {
            blip = Function.Call<int>((Hash)0x554D9D53F696D002, 203020899, TeleportsFunctions.lastTpCoords.X, TeleportsFunctions.lastTpCoords.Y, TeleportsFunctions.lastTpCoords.Z);
            Function.Call((Hash)0x74F74D3207ED525C, blip, -1546805641, 1);
            Function.Call((Hash)0xD38744167B2FA257, blip, 0.2F);
            Function.Call((Hash)0x9CB1A1623062F402, blip, "LastPosition");
        }

        private void OnResourceStop(string resourceName)
        {
            if (API.GetCurrentResourceName() != resourceName) return;

            API.RemoveBlip(ref blip);
        }

        public static Vector3 GetCoordsFromCam(float distance)
        {
            Vector3 rot = API.GetGameplayCamRot(2);
            Vector3 coord = API.GetGameplayCamCoord();

            float tZ = rot.Z * 0.0174532924F;
            float tX = rot.X * 0.0174532924F;




            float num = (float)Math.Abs(Math.Cos(tX));

            float newCoordX = coord.X + (float)(-Math.Sin(tZ)) * (num + distance);
            float newCoordY = coord.Y + (float)(Math.Cos(tZ)) * (num + distance);
            float newCoordZ = coord.Z + (float)(Math.Sin(tX)) * (num + distance);

            return new Vector3(newCoordX, newCoordY, newCoordZ);
        }


        public async static Task<dynamic> GetInput(string title, string hint)
        {
            dynamic postValue = null;
            TriggerEvent("vorpinputs:getInput", title, hint, new Action<dynamic>((value) =>
            {
                postValue = value;
            }));

            while (postValue == null)
            {
                await Delay(1000);
            }
            return postValue;
        }
    }
}
