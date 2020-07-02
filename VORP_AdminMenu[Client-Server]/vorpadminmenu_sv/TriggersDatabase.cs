using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vorpadminmenu_sv
{
    class TriggersDatabase : BaseScript
    {

        public TriggersDatabase()
        {
            EventHandlers["vorp:adminAddMoney"] += new Action<Player, List<object>>(AdminAddMoney);
            EventHandlers["vorp:adminRemoveMoney"] += new Action<Player, List<object>>(AdminRemoveMoney);
            EventHandlers["vorp:adminAddXp"] += new Action<Player, List<object>>(AdminAddXp);
            EventHandlers["vorp:adminRemoveXp"] += new Action<Player, List<object>>(AdminRemoveXp);

            EventHandlers["vorp:adminAddItem"] += new Action<Player, List<object>>(AdminAddItem);
            EventHandlers["vorp:adminAddWeapon"] += new Action<Player, List<object>>(AdminAddWeapon);

            EventHandlers["vorp:getInventory"] += new Action<Player, List<object>>(GetInventory);
        }

        

        private void AdminAddMoney([FromSource]Player source, List<object> args)
        {
            int id = int.Parse(args[0].ToString());
            int type = int.Parse(args[1].ToString());
            double quantity = double.Parse(args[2].ToString());
            if (type == 2)
            {
                Debug.WriteLine("entra");
                int intQuantity = (int)Math.Ceiling(quantity);
                Debug.WriteLine(intQuantity.ToString());

                TriggerEvent("vorp:addMoney", id, type, intQuantity);
            }
            else
            {
                TriggerEvent("vorp:addMoney", id, type, quantity);
            }
        }

        private void AdminRemoveMoney([FromSource]Player source, List<object> args)
        {
            int id = int.Parse(args[0].ToString());
            int type = int.Parse(args[1].ToString());
            double quantity = double.Parse(args[2].ToString());
            if (type == 2)
            {
                TriggerEvent("vorp:removeMoney", id, type, (int)quantity);
            } else
            {
                TriggerEvent("vorp:removeMoney", id, type, quantity);
            }
        }

        private void AdminAddXp([FromSource]Player source, List<object> args)
        {
            int id = int.Parse(args[0].ToString());
            int quantity = int.Parse(args[1].ToString());
            TriggerEvent("vorp:addXp", id, quantity);
        }

        private void AdminRemoveXp([FromSource]Player source, List<object> args)
        {
            int id = int.Parse(args[0].ToString());
            int quantity = int.Parse(args[1].ToString());
            TriggerEvent("vorp:removeXp", id, quantity);
        }

        private void AdminAddItem([FromSource]Player source, List<object> args)
        {
            int idPlayer = int.Parse(args[0].ToString());
            string item = args[1].ToString();
            string quantity = args[2].ToString();
            TriggerEvent("vorpCore:addItem", idPlayer, item, quantity);
        }

        private void AdminAddWeapon([FromSource]Player source, List<object> args)
        {
            int idPlayer = int.Parse(args[0].ToString());
            string item = args[1].ToString();
            string ammo = args[2].ToString();
            int quantity = int.Parse(args[3].ToString());
            Dictionary<string, int> ammoaux = new Dictionary<string, int>();
            ammoaux.Add(ammo, quantity);
            TriggerEvent("vorpCore:registerWeapon", idPlayer, item, ammoaux, ammoaux);
        }

        private void GetInventory([FromSource]Player source, List<object> args)
        {
            int idPlayer = int.Parse(args[0].ToString());
            TriggerEvent("vorpCore:getUserInventory", idPlayer, new Action<dynamic>((items) =>
            {
                source.TriggerEvent("vorp:loadPlayerInventory",items);
            }));
        }
    }
}
