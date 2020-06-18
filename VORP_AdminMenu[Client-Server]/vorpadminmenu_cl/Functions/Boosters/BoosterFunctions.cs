using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions.Utils;

namespace vorpadminmenu_cl.Functions.Boosters
{
    class BoosterFunctions : BaseScript
    {
        static float heading;
        public static bool godmodeON = false;
        public static bool noclip = false;
        public static bool noclip2 = false;
        float speed = 1.28F;

        public static bool thorON = false;
        public BoosterFunctions()
        {
            EventHandlers["vorp:thordone"] += new Action<Vector3>(ThorDone);

            Tick += Noc;
            Tick += Noc2;
            Tick += OnLight;

        }

        public static void SetupBoosters()
        {
            API.RegisterCommand("golden", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Golden(args);
            }), false);

            API.RegisterCommand("gm", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                GodMode(args);
            }), false);

            API.RegisterCommand("n", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Noclip(args);
            }), false);
            API.RegisterCommand("m", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Noclip2(args);
            }), false);

            API.RegisterCommand("thor", new Action<int, List<object>, string>((source, args, raw) =>
            {
                Thor(args);
            }), false);
        }

        public static void Golden(List<object> args)
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


        public static void GodMode(List<object> args)
        {

            if (!Menus.Boosters.Getgmode())
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), true);
                Menus.Boosters.Setgmode(true);
            }
            else
            {
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), false);
                Menus.Boosters.Setgmode(false);
            }
        }

        public static void SetClip(bool active)
        {
            int playerPed = API.PlayerPedId();
            heading = API.GetEntityHeading(playerPed);

            if (active)
            {
                API.FreezeEntityPosition(playerPed, true);
            }
            else
            {
                API.FreezeEntityPosition(playerPed, false);
            }
        }

        public static void Noclip(List<object> args)
        {


            int playerPed = API.PlayerPedId();
            heading = API.GetEntityHeading(playerPed);

            if (!Menus.Boosters.Getnclip())
            {
                API.FreezeEntityPosition(playerPed, true);
                Menus.Boosters.Setnclip(true);
            }
            else
            {
                API.FreezeEntityPosition(playerPed, false);
                Menus.Boosters.Setnclip(false);
            }
        }

        [Tick]
        private async Task Noc()
        {
            if (GetUserInfo.loaded)
            {
                if (Menus.Boosters.Getnclip())
                {
                    int playerPed = API.PlayerPedId();
                    API.SetEntityHeading(playerPed, heading);
                    if (API.IsControlPressed(0, 0x8FD015D8)) //W
                    {
                        Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, speed, -1.0F);
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0xD27782E3)) //S
                    {
                        Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, -speed, -1.0F);
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0x7065027D)) //A
                    {
                        Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, -speed, 0.0F, -1.0F);
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0xB4E465B4)) //D
                    {
                        Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, speed, 0.0F, -1.0F);
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0xD9D0E1C0)) //SPACE
                    {
                        Vector3 c = new Vector3();
                        if (speed > 1.0F)
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -speed * 2);
                        }
                        else
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -speed - 1.0F);
                        }
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0x8FFC75D6)) //SHIFT
                    {
                        Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, speed - 1.0F);
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0x6319DB71)) //UP
                    {
                        if (speed > 0.5F)
                        {
                            speed = speed + 0.5F;
                        }
                    }
                    if (API.IsControlPressed(0, 0x05CA7C52)) //DOWN
                    {
                        if (speed > 0.5)
                        {
                            speed = speed - 0.5F;
                        }
                    }
                    if (API.IsControlPressed(0, 0x9959A6F0)) //C
                    {
                        speed = 1.28F;
                    }
                    heading += API.GetGameplayCamRelativeHeading();
                }
            }
        }

        public static void Noclip2(List<object> args)
        {
            int playerPed = API.PlayerPedId();
            heading = API.GetEntityHeading(playerPed);

            if (!Menus.Boosters.Getmclip())
            {
                API.FreezeEntityPosition(playerPed, true);
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), true);
                Menus.Boosters.Setmclip(true);
            }
            else
            {
                API.FreezeEntityPosition(playerPed, false);
                Function.Call(Hash.SET_PLAYER_INVINCIBLE, API.PlayerId(), false);
                Menus.Boosters.Setmclip(false);
            }
        }

        [Tick]
        private async Task Noc2()
        {
            if (GetUserInfo.loaded)
            {
                int playerPed = API.PlayerPedId();
                if (Menus.Boosters.Getmclip())
                {
                    API.SetEntityHeading(playerPed, heading);
                    if (API.IsControlPressed(0, 0x8FD015D8)) //W
                    {
                        Vector3 a = API.GetGameplayCamRot(0);

                        Vector3 c = new Vector3();
                        if (a.X > 8.0F)
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, speed, -0.5F);
                            API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                        }
                        else if (a.X < -8.0F)
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, speed, -1.5F);
                            API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                        }
                        else
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, speed, -1.0F);
                            API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                        }
                    }

                    if (API.IsControlPressed(0, 0xD9D0E1C0)) //SPACE
                    {
                        Vector3 c = new Vector3();
                        if (speed > 1.0F)
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -speed * 2);
                        }
                        else
                        {
                            c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, -speed - 1.0F);
                        }
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0x8FFC75D6)) //SHIFT
                    {
                        Vector3 c = API.GetOffsetFromEntityInWorldCoords(playerPed, 0.0F, 0.0F, speed - 1.0F);
                        API.SetEntityCoords(playerPed, c.X, c.Y, c.Z, true, true, true, true);
                    }

                    if (API.IsControlPressed(0, 0xCEFD9220)) //E-more speed
                    {
                        if (speed > 0.5F)
                        {
                            speed = speed + 0.5F;
                        }
                    }
                    if (API.IsControlPressed(0, 0xDE794E3E)) //Q-less speed
                    {
                        if (speed > 0.5)
                        {
                            speed = speed - 0.5F;
                        }
                    }
                    if (API.IsControlPressed(0, 0x8CC9CD42)) //X-default speed
                    {
                        speed = 1.28F;
                    }
                    if (API.IsControlPressed(0, 0xB2F377E8)) //F-turn off noclip2
                    {
                        List<object> args = new List<object>();
                        Noclip2(args);
                    }
                    heading += API.GetGameplayCamRelativeHeading();
                }
            }
        }

        [Tick]
        public async Task OnLight()
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
                if (API.IsControlJustPressed(0, 0xCEE12B50) && thorON)
                {
                    TriggerServerEvent("vorp:thor", endCoord);
                }

                if (thorON)
                {
                    Function.Call((Hash)0x2A32FAA57B937173, -1795314153, endCoord.X, endCoord.Y, endCoord.Z, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.0F, 0.5F, 0.5F, 50.0F, 255, 255, 0, 155, false, false, 2, false, 0, 0, false);
                }
            }
        }

        private void ThorDone(Vector3 endCooord)
        {
            API.ForceLightningFlashAtCoords(endCooord.X, endCooord.Y, endCooord.Z);
        }


        public static void Thor(List<object> args)
        {
            if (Menus.Boosters.Gettmode())
            {
                Menus.Boosters.Settmode(false);
            }
            else
            {
                Menus.Boosters.Settmode(true);
            }
        }

    }
}
