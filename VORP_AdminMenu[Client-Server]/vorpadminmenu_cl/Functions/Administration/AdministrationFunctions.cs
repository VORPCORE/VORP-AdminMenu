using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions.Utils;
using Hash = CitizenFX.Core.Native.Hash;

namespace vorpadminmenu_cl.Functions.Administration
{
    class AdministrationFunctions : BaseScript
    {
        static bool handcuffed = false;
        static List<int> blipsList = new List<int>();
        public static bool playersFollow = false;
        static bool fireguy = false;
        public static bool spectating;
        public static int camera;
        float speed = 1.28F;
        public AdministrationFunctions()
        {
            Tick += freezeAnim;
            Tick += CreateBlips;
            

            EventHandlers["vorp:slapback"] += new Action(SlapDone);
            EventHandlers["vorp:stopit"] += new Action(StopIt);

            EventHandlers["vorp:thorIDdone"] += new Action(ThorIDdone);
            EventHandlers["vorp:fireIDdone"] += new Action(FireIDDone);
        }

        

        public static void SetupAdministration()
        {
            API.RegisterCommand("stop", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                StopPlayer(args);
            }), false);

            API.RegisterCommand("slap", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Slap(args);
            }), false);

            API.RegisterCommand("k", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Kick(args);
            }), false);

            API.RegisterCommand("thorp", new Action<int, List<object>, string>((source, args, raw) =>
            {
                ThorToId(args);
            }), false);

            API.RegisterCommand("firep", new Action<int, List<object>, string>((source, args, raw) =>
            {
                FireToId(args);
            }), false);

            API.RegisterCommand("cblip", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                PlayerBlips(args);
            }), false);

            API.RegisterCommand("son", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Spectate(args);
            }), false);

            API.RegisterCommand("soff", new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                SpectateOff(args);
            }), false);

        }

        

        public static void StopPlayer(List<object> args)
        {
            int idPlayer = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:stopplayer", idPlayer);
        }

        public static void StopIt()
        {
            if (!handcuffed)
            {
                API.FreezeEntityPosition(API.PlayerPedId(), true);
                API.DisableAllControlActions(-1);
                Function.Call((Hash)0xDF1AF8B5D56542FA, API.PlayerPedId(), true);
                handcuffed = true;
            }
            else
            {
                API.FreezeEntityPosition(API.PlayerPedId(), false);
                Function.Call((Hash)0xDF1AF8B5D56542FA, API.PlayerPedId(), false);
                handcuffed = false;
            }
        }

        [Tick]
        private async Task freezeAnim()
        {
            if (handcuffed)
            {
                await Delay(0);
                API.ClearPedTasksImmediately(API.PlayerPedId(), 1, 1);
            }
        }

        public static void Slap(List<object> args)
        {
            int destinataryID = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:slap", destinataryID);
        }

        private void SlapDone()
        {
            Vector3 idCoords = API.GetEntityCoords(API.PlayerPedId(), true, true);
            UtilsFunctions.TeleportToCoords(idCoords.X, idCoords.Y, idCoords.Z + 1000.0F);
        }

        public static void Kick(List<object> args)
        {
            int id = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:kick", id);
        }

        public static void PlayerBlips(List<object> args)
        {
            if (Menus.Administration.GetPFollow())
            {
                Menus.Administration.SetPFollow(false);
                ClearBlips();
            }
            else
            {
                Menus.Administration.SetPFollow(true);
            }
        }
        [Tick]
        private async Task CreateBlips()
        {
            if (GetUserInfo.loaded)
            {
                if (Menus.Administration.GetPFollow())
                {
                    foreach (var i in API.GetActivePlayers())
                    {
                        int blip = API.GetBlipFromEntity(API.GetPlayerPed(i));
                        if (!API.DoesBlipExist(blip))
                        {
                            await Delay(10);
                            Vector3 coords = API.GetEntityCoords(API.GetPlayerPed(i), true, true);
                            int _blip = Function.Call<int>((Hash)0x23F74C2FDA6E7C61, 1664425300, API.GetPlayerPed(i));
                            Function.Call((Hash)0x74F74D3207ED525C, _blip, -1580514024, 1);
                            Function.Call((Hash)0xD38744167B2FA257, _blip, 0.2F);
                            Function.Call((Hash)0x9CB1A1623062F402, _blip, $"{API.GetPlayerName(i)} id: {API.GetPlayerServerId(i)}");
                            blipsList.Add(_blip);
                        }
                    }
                    await Delay(10000);
                }
            }
        }

        public static async Task ClearBlips()
        {
            foreach (int b in blipsList)
            {
                int actualBlip = b;
                API.RemoveBlip(ref actualBlip);
            }
            blipsList.Clear();
            await Delay(1);
        }

        public static void ThorToId(List<object> args)
        {
            int id = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:thorIDserver", id);
        }
        private void ThorIDdone()
        {
            Vector3 endCoord = API.GetEntityCoords(API.PlayerPedId(), true, true);
            API.ForceLightningFlashAtCoords(endCoord.X, endCoord.Y, endCoord.Z);
        }

        public static void FireToId(List<object> args)
        {
            int id = int.Parse(args[0].ToString());
            TriggerServerEvent("vorp:fireIDserver", id);
        }

        private void FireIDDone()
        {
            if (!fireguy)
            {
                fireguy = true;
            }
            else
            {
                fireguy = false;
            }

        }

        [Tick]
        private async Task fireON()
        {

            if (!API.IsEntityDead(API.PlayerPedId()) && fireguy)
            {
                API.StartEntityFire(API.PlayerPedId(), 0, 0, 100000);
            }
            else
            {
                fireguy = false;
            }
            await Delay(2000);
        }

        private static void Spectate(List<object> args)
        {
            int playerId = int.Parse(args[0].ToString());
            int player = API.GetPlayerFromServerId(playerId);
            int playerPed = API.GetPlayerPed(player);
            API.NetworkSetInSpectatorMode(true, playerPed);
        }

        private static void SpectateOff(List<object> args)
        {
            API.NetworkSetInSpectatorMode(false, API.PlayerPedId());
            //isInSpectatorMode
        }
    }
}
