using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_sv
{
    public class BanManager : BaseScript
    {
        public static List<PlayerBanned> userBanneds = new List<PlayerBanned>();

        public BanManager()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            LoadBannedsFromDB();
        }

        private async void OnPlayerConnecting([FromSource]Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            deferrals.defer();
            await Delay(0);

            string steam = player.Identifiers["steam"];
            string license = player.Identifiers["license"];

            if (userBanneds.Any(x => x.Steam.Contains(steam)))
            {
                PlayerBanned userBan = userBanneds.FirstOrDefault(x=> x.Steam.Contains(steam));
                if (userBan.Permanent)
                {
                    deferrals.done(LoadConfig.Langs["YouArePermanentBanned"]);
                    setKickReason(LoadConfig.Langs["YouArePermanentBanned"]);
                }
                else
                {
                    //Need calculation of DateTime in constructor vamos que lo hago luego
                }
             
            } else if (userBanneds.Any(x => x.License.Contains(license)))
            {
                PlayerBanned userBan = userBanneds.FirstOrDefault(x => x.License.Contains(license));
                if (userBan.Permanent)
                {
                    deferrals.done(LoadConfig.Langs["YouArePermanentBanned"]);
                    setKickReason(LoadConfig.Langs["YouArePermanentBanned"]);
                }
                else
                {
                    //Need calculation of DateTime in constructor vamos que lo hago luego
                }
            }
        }

        public async Task LoadBannedsFromDB()
        {
            Exports["ghmattimysql"].execute("SELECT * FROM banneds", new[] { "" }, new Action<dynamic>((result) =>
            {
                if (result.Count != 0)
                {
                    foreach (var r in result)
                    {
                        int id = r.b_id;
                        string steam = r.b_steam;
                        string license = r.b_license;
                        string discord = r.b_discord;
                        DateTime banned = r.b_banned;
                        DateTime unban = r.b_unban;
                        bool permanent = Convert.ToBoolean(r.b_permanent);
                        userBanneds.Add(new PlayerBanned(id, steam, license, discord, banned, unban, permanent));
                    }
                }

            }));
        }

        public static Player getPlayerFromSource(int handle)
        {
            PlayerList pl = new PlayerList();
            Player p = pl[handle];
            return p;
        }
    }
}
