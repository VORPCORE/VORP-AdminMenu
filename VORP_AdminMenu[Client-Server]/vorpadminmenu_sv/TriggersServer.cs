using CitizenFX.Core;
using System;
using vorpadminmenu_sv.Diagnostics;

namespace vorpadminmenu_sv
{
    class TriggersServer : BaseScript
    {

        PlayerList PlayersList;

        public TriggersServer()
        {
            PlayersList = Players;

            EventHandlers["vorp:ownerCoordsToBring"] += new Action<Vector3, int>(CoordsToBringPlayer);
            EventHandlers["vorp:askCoordsToTPPlayerDestiny"] += new Action<Player, int>(CoordsToPlayerDestiny);
            EventHandlers["vorp:callbackCoords"] += new Action<string, Vector3>(CoordsToStart);

            EventHandlers["vorp:privateMessage"] += new Action<Player, int, string>(PrivateMessage);
            EventHandlers["vorp:broadCastMessage"] += new Action<Player, string>(BroadCastMessage);

            EventHandlers["vorp:thor"] += new Action<Vector3>(ThorServer);


            EventHandlers["vorp:kick"] += new Action<Player, int>(Kick);
            EventHandlers["vorp:slap"] += new Action<Player, int>(Slap);
            EventHandlers["vorp:stopplayer"] += new Action<Player, int>(StopP);

            EventHandlers["vorp:thorIDserver"] += new Action<Player, int>(ThorToId);
            EventHandlers["vorp:fireIDserver"] += new Action<Player, int>(FireToId);

            EventHandlers["vorp:revivePlayer"] += new Action<Player, int>(RevivePlayer);
            EventHandlers["vorp:healPlayer"] += new Action<Player, int>(HealPlayer);

        }

        private void CoordsToBringPlayer(Vector3 coordToSend, int destinataryID)
        {
            try
            {
                Player p = PlayersList[destinataryID];
                TriggerClientEvent(p, "vorp:sendCoordsToDestinyBring", coordToSend);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"CoordsToBringPlayer");
            }
        }

        private void CoordsToPlayerDestiny([FromSource] Player ply, int destinataryID)
        {
            try
            {
                Player p = PlayersList[destinataryID];
                TriggerClientEvent(p, "vorp:askForCoords", ply.Handle);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"CoordsToPlayerDestiny");
            }
        }

        private void CoordsToStart(string sourceID, Vector3 coordsDestiny)
        {
            try
            {
                Player p = PlayersList[int.Parse(sourceID)];
                TriggerClientEvent(p, "vorp:coordsToStart", coordsDestiny);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"CoordsToStart");
            }
        }

        private void PrivateMessage([FromSource] Player player, int id, string message)
        {
            try
            {
                Player p = PlayersList[id];
                TriggerClientEvent(p, "vorp:Tip", message, 8000);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"PrivateMessage");
            }
        }

        private void BroadCastMessage([FromSource] Player player, string message)
        {
            TriggerClientEvent("vorp:NotifyLeft", player.Name, message, "generic_textures", "tick", 12000);
        }


        private void ThorServer(Vector3 thorCoords)
        {
            TriggerClientEvent("vorp:thordone", thorCoords);
        }

        private void StopP([FromSource] Player player, int id)
        {
            try
            {
                Player p = PlayersList[id];
                TriggerClientEvent(p, "vorp:stopit");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"StopP");
            }
        }

        private void Slap([FromSource] Player player, int idDestinatary)
        {
            try
            {
                Player p = PlayersList[idDestinatary];
                p.TriggerEvent("vorp:slapback");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Slap");
            }
        }

        private void Kick([FromSource] Player player, int id)
        {
            try
            {
                Player p = PlayersList[id];
                p.Drop("Kicked by Staff");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Kick");
            }
        }

        private void ThorToId([FromSource] Player player, int idDestinatary)
        {
            try
            {
                Player p = PlayersList[idDestinatary];
                TriggerClientEvent(p, "vorp:thorIDdone");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"ThorToId");
            }
        }

        private void FireToId([FromSource] Player player, int idDestinatary)
        {
            try
            {
                Player p = PlayersList[idDestinatary];
                TriggerClientEvent(p, "vorp:fireIDdone");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"FireToId");
            }
        }

        private void RevivePlayer([FromSource] Player player, int idDestinatary)
        {
            try
            {
                if (idDestinatary != -1)
                {
                    Player p = PlayersList[idDestinatary];
                    TriggerClientEvent(p, "vorp:resurrectPlayer");
                }
                else
                {
                    player.TriggerEvent("vorp:resurrectPlayer");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"RevivePlayer");
            }
        }

        private void HealPlayer([FromSource] Player player, int idDestinatary)
        {
            try
            {
                if (idDestinatary != -1)
                {
                    Player p = PlayersList[idDestinatary];
                    p.TriggerEvent("vorpmetabolism:setValue", "Thirst", 1000);
                    p.TriggerEvent("vorpmetabolism:setValue", "Hunger", 1000);
                    p.TriggerEvent("vorp:healDone");
                }
                else
                {
                    player.TriggerEvent("vorpmetabolism:setValue", "Thirst", 1000);
                    player.TriggerEvent("vorpmetabolism:setValue", "Hunger", 1000);
                    player.TriggerEvent("vorp:healDone");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"HealPlayer");
            }
        }
    }
}
