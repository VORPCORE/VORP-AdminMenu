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
            EventHandlers["vorp_adminmenu:addNewBan"] += new Action<Player, int, string, string>(AddNewBan);
            LoadBannedsFromDB();
            Tick += CheckBanneds;
        }

        private async Task CheckBanneds()
        {
            await Delay(300000); // 5 minutos
            for (int i=0; i < userBanneds.Count(); i++)
            {
                if (!userBanneds[i].Permanent)
                {
                    TimeSpan diff = (userBanneds[i].Unban - DateTime.Now);
                    if(diff.TotalSeconds < 0)
                    {
                        userBanneds[i].DeleteInDB();
                        await Delay(100);
                        userBanneds.RemoveAt(i);
                    }
                }
            }
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
                    deferrals.done(string.Format(LoadConfig.Langs["YouAreTempBanned"], diff.Days.ToString(), diff.Hours.ToString(), diff.Minutes.ToString()));
                    setKickReason(string.Format(LoadConfig.Langs["YouAreTempBanned"], diff.Days.ToString(), diff.Hours.ToString(), diff.Minutes.ToString()));
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
                    deferrals.done(string.Format(LoadConfig.Langs["YouAreTempBanned"], diff.Days.ToString(), diff.Hours.ToString(), diff.Minutes.ToString()));
                    setKickReason(string.Format(LoadConfig.Langs["YouAreTempBanned"], diff.Days.ToString(), diff.Hours.ToString(), diff.Minutes.ToString()));
                }
            }

            deferrals.done();

        }

        public async void AddNewBan([FromSource]Player player, int targetId, string temp, string reason)
        {

            DateTime banned = DateTime.Now;
            Player target = getPlayerFromSource(targetId);
            string steam = "none";
            string license = "none";
            string discord = "none";
            DateTime unban = new DateTime();
            int permanent = 1;
            

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


            try
            {
                
                if (!temp.StartsWith("0"))
                {
                    permanent = 0;
                    if (temp.EndsWith("Y"))
                    {
                        Debug.WriteLine("Entra en el try");
                        unban = banned.AddYears(int.Parse(temp.Remove(temp.Length - 1)));
                    }
                    else if (temp.EndsWith("M"))
                    {
                        unban = banned.AddMonths(int.Parse(temp.Remove(temp.Length - 1)));
                    }
                    else if (temp.EndsWith("D"))
                    {
                        unban = banned.AddDays(int.Parse(temp.Remove(temp.Length - 1)));
                    }
                    else if (temp.EndsWith("H"))
                    {
                        unban = banned.AddHours(int.Parse(temp.Remove(temp.Length - 1)));
                    }
                    else if (temp.EndsWith("m"))
                    {
                        Debug.WriteLine("Entra en el try");
                        unban = banned.AddMinutes(int.Parse(temp.Remove(temp.Length - 1)));
                    }
                    else
                    {
                        player.TriggerEvent("vorp:Tip", LoadConfig.Langs["SyntaxIncorrect"], 5000);
                        return;
                    }
                }
            }
            catch
            {
                player.TriggerEvent("vorp:Tip", LoadConfig.Langs["SyntaxIncorrect"], 5000);
                return;
            }
            await Delay(2000);
            Exports["ghmattimysql"].execute("INSERT INTO banneds (b_steam,b_license,b_discord,b_reason,b_banned,b_unban,b_permanent) VALUES (?,?,?,?,?,?,?)", new[] { steam, license, discord, reason, banned.ToString() , unban.ToString(), permanent.ToString()}, new Action<dynamic>((result) =>
            {
                int newId = result.insertId;
                userBanneds.Add(new PlayerBanned(newId, steam, license, discord, banned, unban, Convert.ToBoolean(permanent), reason));
                string duration = "";
                if (permanent == 1)
                {
                    duration = LoadConfig.Langs["Permament"];
                }
                else
                {
                    duration = unban.ToString();
                }
                target.Drop(string.Format(LoadConfig.Langs["YouHasBeenBanned"], reason, duration));
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
                        string reason = r.b_reason;
                        DateTime banned = DateTime.Parse(r.b_banned);
                        DateTime unban = DateTime.Parse(r.b_unban);
                        bool permanent = Convert.ToBoolean(r.b_permanent);
                        userBanneds.Add(new PlayerBanned(id, steam, license, discord, banned, unban, permanent, reason));
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
