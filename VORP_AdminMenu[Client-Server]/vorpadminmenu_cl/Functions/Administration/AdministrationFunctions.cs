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
        public AdministrationFunctions()
        {
            Tick += freezeAnim;
            Tick += CreateBlips;
            

            EventHandlers["vorp:slapback"] += new Action(SlapDone);
            EventHandlers["vorp:stopit"] += new Action(StopIt);

            EventHandlers["vorp:thorIDdone"] += new Action(ThorIDdone);
            EventHandlers["vorp:fireIDdone"] += new Action(FireIDDone);

            EventHandlers["vorp:healDone"] += new Action(healDone);
        }

       

        public static void SetupAdministration()
        {
            API.RegisterCommand(GetConfig.Config["Freeze"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                StopPlayer(args);
            }), false);
            

            API.RegisterCommand(GetConfig.Config["Kick"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Kick(args);
            }), false);

            API.RegisterCommand(GetConfig.Config["Ban"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Ban(args);
            }), false);

            

            API.RegisterCommand(GetConfig.Config["Cblip"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                PlayerBlips(args);
            }), false);

            API.RegisterCommand(GetConfig.Config["SpectateOn"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Spectate(args);
            }), false);

            API.RegisterCommand(GetConfig.Config["SpectateOff"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                SpectateOff(args);
            }), false);
            API.RegisterCommand(GetConfig.Config["Revive"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Revive(args);
            }), false);
            API.RegisterCommand(GetConfig.Config["Heal"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
            {
                Heal(args);
            }), false);

            if (GetUserInfo.userGroup.Contains("admin"))
            {
                API.RegisterCommand(GetConfig.Config["Slap"].ToString(), new Action<int, List<object>, string, string>((source, args, cl, raw) =>
                {
                    Slap(args);
                }), false);
                API.RegisterCommand(GetConfig.Config["ThorP"].ToString(), new Action<int, List<object>, string>((source, args, raw) =>
                {
                    ThorToId(args);
                }), false);

                API.RegisterCommand(GetConfig.Config["FireP"].ToString(), new Action<int, List<object>, string>((source, args, raw) =>
                {
                    FireToId(args);
                }), false);
            }

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

        public static void Ban(List<object> args)
        {
            int target = int.Parse(args[0].ToString());
            string temp = args[1].ToString().Trim();

            string reason = "";

            for(int i = 2; i < args.Count(); i++)
                reason = args[i].ToString() + " ";

            TriggerServerEvent("vorp_adminmenu:addNewBan", target, temp, reason);
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

        public static void Spectate(List<object> args)
        {
            int playerId = int.Parse(args[0].ToString());
            int player = API.GetPlayerFromServerId(playerId);
            int playerPed = API.GetPlayerPed(player);
            API.NetworkSetInSpectatorMode(true, playerPed);
        }

        public static void SpectateOff(List<object> args)
        {
            API.NetworkSetInSpectatorMode(false, API.PlayerPedId());
        }

        public static void Revive(List<object> args)
        {
            int idDestinatary = -1;

            if (args.Count != 0)
                idDestinatary = int.Parse(args[0].ToString());
            
            TriggerServerEvent("vorp:revivePlayer", idDestinatary);
        }

        public static void Heal(List<object> args)
        {
            int idDestinatary = -1;

            if (args.Count != 0)
                idDestinatary = int.Parse(args[0].ToString());

            TriggerServerEvent("vorp:healPlayer", idDestinatary);
        }

        private void healDone()
        {
            Function.Call((Hash)0xC6258F41D86676E0, API.PlayerPedId(), 1, 100);
            Function.Call((Hash)0xC6258F41D86676E0, API.PlayerPedId(), 1, 100);
            Function.Call((Hash)0xAC2767ED8BDFAB15, API.PlayerPedId(), 100, 0);
        }
    }
}
