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
    }
}
