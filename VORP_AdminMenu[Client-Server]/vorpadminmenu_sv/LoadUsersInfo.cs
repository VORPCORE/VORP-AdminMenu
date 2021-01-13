using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_sv
{
    class LoadUsersInfo : BaseScript
    {
        public static dynamic VORPCORE;
        public LoadUsersInfo()
        {
            EventHandlers["vorp_admin:LoadPlayerInfo"] += new Action<Player>(LoadPlayerInfo);

            TriggerEvent("getCore", new Action<dynamic>((dic) =>
            {
                VORPCORE = dic;
            }));
        }

        private void LoadPlayerInfo([FromSource]Player source)
        {
            int _source = int.Parse(source.Handle);
            dynamic UserCharacter = VORPCORE.getUser(_source).getUsedCharacter;
            string group = UserCharacter.group;

            source.TriggerEvent("vorp_admin:GetPlayerInfo",group);
        }
    }
}
