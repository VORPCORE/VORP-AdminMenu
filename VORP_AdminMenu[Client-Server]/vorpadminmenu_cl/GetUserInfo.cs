using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace vorpadminmenu_cl
{
    class GetUserInfo : BaseScript
    {
        public static string userGroup = "user";
        public GetUserInfo()
        {
            EventHandlers["vorp_admin:GetPlayerInfo"] += new Action<string>(GetPlayerInfo);
            TriggerServerEvent("vorp_admin:LoadPlayerInfo");
        }

        private void GetPlayerInfo(string group)
        {
            userGroup = group;
            SetupMenu();
        }

        private async Task SetupMenu()
        {
            await Delay(2000);
            if (GetUserInfo.userGroup != "user")
            {
                Menus.MainMenu.GetMenu();
            }
        }
    }
}
