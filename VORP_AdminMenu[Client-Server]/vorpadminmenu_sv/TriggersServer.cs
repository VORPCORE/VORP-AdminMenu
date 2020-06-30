using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_sv
{
    class TriggersServer : BaseScript
    {
        public TriggersServer()
        {
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
            PlayerList pl = new PlayerList();
            Player p = pl[destinataryID];
            TriggerClientEvent(p, "vorp:sendCoordsToDestinyBring", coordToSend);
        }


        private void CoordsToPlayerDestiny([FromSource]Player ply, int destinataryID)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[destinataryID];
            TriggerClientEvent(p, "vorp:askForCoords", ply.Handle);
        }


        private void CoordsToStart(string sourceID, Vector3 coordsDestiny)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[int.Parse(sourceID)];
            TriggerClientEvent(p, "vorp:coordsToStart", coordsDestiny);
        }

        private void PrivateMessage([FromSource]Player player, int id, string message)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[id];
            TriggerClientEvent(p,"vorp:Tip", message, 8000);
        }

        private void BroadCastMessage([FromSource]Player player, string message)
        {
            TriggerClientEvent("vorp:NotifyLeft", player.Name, message, "generic_textures", "tick", 12000);
        }


        private void ThorServer(Vector3 thorCoords)
        {
            TriggerClientEvent("vorp:thordone", thorCoords);
        }


        private void StopP([FromSource]Player player, int id)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[id];
            TriggerClientEvent(p, "vorp:stopit");
        }

        private void Slap([FromSource]Player player, int idDestinatary)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[idDestinatary];
            p.TriggerEvent("vorp:slapback");
        }

        private void Kick([FromSource]Player player, int id)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[id];
            p.Drop("Kicked by Staff");
        }

        private void ThorToId([FromSource]Player player, int idDestinatary)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[idDestinatary];
            TriggerClientEvent(p, "vorp:thorIDdone");
        }
        private void FireToId([FromSource]Player player, int idDestinatary)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[idDestinatary];
            TriggerClientEvent(p, "vorp:fireIDdone");
        }

        private void RevivePlayer([FromSource]Player player, int idDestinatary)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[idDestinatary];
            TriggerClientEvent(p, "vorp:resurrectPlayer");
        }

        private void HealPlayer([FromSource]Player player, int idDestinatary)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[idDestinatary];
            p.TriggerEvent("vorpmetabolism:setValue", "Thirst", 1000);
            p.TriggerEvent("vorpmetabolism:setValue", "Hunger", 1000);
            p.TriggerEvent("vorp:healDone");
        }
    }
}
