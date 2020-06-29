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
            TriggerEvent("vorp:removeMoney", id, quantity);
        }
    }
}
