using System;
using CitizenFX.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_sv
{
    public class PlayerBanned : BaseScript
    {
        int id;
        string steam;
        string license;
        string discord;
        DateTime banned;
        DateTime unban;
        bool permanent;
        string reason;

        public PlayerBanned()
        {

        }

        public PlayerBanned(int id, string steam, string license, string discord, DateTime banned, DateTime unban, bool permanent, string reason = "")
        {
            this.id = id;
            this.steam = steam;
            this.license = license;
            this.discord = discord;
            this.banned = banned;
            this.unban = unban;
            this.permanent = permanent;
            this.reason = reason;
        }

        public int Id { get => id; set => id = value; }
        public string Steam { get => steam; set => steam = value; }
        public string License { get => license; set => license = value; }
        public string Discord { get => discord; set => discord = value; }
        public DateTime Banned { get => banned; set => banned = value; }
        public DateTime Unban { get => unban; set => unban = value; }
        public bool Permanent { get => permanent; set => permanent = value; }
        public string Reason { get => reason; set => reason = value; }

        public void DeleteInDB()
        {
            Exports["ghmattimysql"].execute($"DELETE FROM BANNEDS WHERE b_id=?", new[] { id });
        }

    }
}
