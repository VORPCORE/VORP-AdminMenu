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
            EventHandlers["vorp_adminmenu:addNewBan"] += new Action<Player, int, DateTime, int>(AddNewBan);
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
                    TimeSpan diff = (userBan.Unban - DateTime.Now);
                    deferrals.done(string.Format(LoadConfig.Langs["YouAreTempBanned"], diff.TotalDays.ToString(), diff.TotalHours.ToString(), diff.TotalMinutes.ToString()));
                    setKickReason(string.Format(LoadConfig.Langs["YouAreTempBanned"], diff.TotalDays.ToString(), diff.TotalHours.ToString(), diff.TotalMinutes.ToString()));
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
                    TimeSpan diff = (userBan.Unban - DateTime.Now);
                    deferrals.done(string.Format(LoadConfig.Langs["YouAreTempBanned"], diff.TotalDays.ToString(), diff.TotalHours.ToString(), diff.TotalMinutes.ToString()));
                    setKickReason(string.Format(LoadConfig.Langs["YouAreTempBanned"], diff.TotalDays.ToString(), diff.TotalHours.ToString(), diff.TotalMinutes.ToString()));
                }
            }

            deferrals.done();

        }

        public async void AddNewBan([FromSource]Player player, int targetId, DateTime unban, int permanent = 0)
        {
            DateTime banned = DateTime.Now;

            Player target = getPlayerFromSource(targetId);
            string steam = "none";
            string license = "none";
            string discord = "none";

            foreach (var identifier in target.Identifiers)
            {
                if (identifier.Contains("steam:"))
                {
                    steam = identifier;
                }else if (identifier.Contains("license:"))
                {
                    license = identifier;
                }
                else if (identifier.Contains("discord:"))
                {
                    discord = identifier;
                }
            }

            Exports["ghmattimysql"].execute("INSERT INTO banneds (b_steam,b_license,b_discord,b_banned,b_unban,b_permanent) VALUES (?,?,?,?,?,?)", new[] { steam, license, discord, banned.ToString(), unban.ToString(), permanent.ToString()}, new Action<dynamic>((result) =>
            {
                int newId = result.insertId;
                userBanneds.Add(new PlayerBanned(newId, steam, license, discord, banned, unban, Convert.ToBoolean(permanent)));
            }));
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
                        DateTime banned = DateTime.Parse(r.b_banned);
                        DateTime unban = DateTime.Parse(r.b_unban);
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
