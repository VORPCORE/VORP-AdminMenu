using CitizenFX.Core;

using System;
using System.Collections.Generic;

using vorpadminmenu_sv.Diagnostics;

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
            EventHandlers["vorp:adminDelItem"] += new Action<Player, List<object>>(AdminDelItem);
            EventHandlers["vorp:adminAddWeapon"] += new Action<Player, List<object>>(AdminAddWeapon);

            EventHandlers["vorp:getInventory"] += new Action<Player, List<object>>(GetInventory);
        }

        private void AdminAddMoney([FromSource] Player source, List<object> args)
        {
            bool idC = int.TryParse(args[0].ToString(), out int id);
            bool typeC = int.TryParse(args[1].ToString(), out int type);

            dynamic UserCharacter = LoadConfig.VORPCORE.getUser(id).getUsedCharacter;

            if (idC && typeC)
            {
                if (type == 2)
                {
                    bool quantityC = double.TryParse(args[2].ToString(), out double quantity);
                    if (quantityC)
                    {
                        int intQuantity = (int)Math.Ceiling(quantity);
                        UserCharacter.addCurrency(type, intQuantity);
                    }
                    else
                    {
                        bool quantityCInt = int.TryParse(args[2].ToString(), out int quantityInt);
                        if (quantityCInt)
                        {
                            UserCharacter.addCurrency(type, quantityInt);
                        }
                        else
                        {
                            Logger.Error("Bad syntax");
                        }
                    }
                }
                else if (type == 0 || type == 1)
                {
                    bool quantityC = double.TryParse(args[2].ToString(), out double quantity);
                    if (quantityC)
                    {
                        UserCharacter.addCurrency(type, quantity);
                    }
                }
                else
                {
                    Logger.Error("Bad syntax");
                }
            }
            else
            {
                Logger.Error("Bad syntax");
            }
        }

        private void AdminRemoveMoney([FromSource] Player source, List<object> args)
        {
            bool idC = int.TryParse(args[0].ToString(), out int id);
            bool typeC = int.TryParse(args[1].ToString(), out int type);

            dynamic UserCharacter = LoadConfig.VORPCORE.getUser(id).getUsedCharacter;

            if (idC && typeC)
            {
                if (type == 2)
                {
                    bool quantityC = double.TryParse(args[2].ToString(), out double quantity);
                    if (quantityC)
                    {
                        int intQuantity = (int)Math.Ceiling(quantity);
                        UserCharacter.removeCurrency(type, intQuantity);
                    }
                    else
                    {
                        bool quantityCInt = int.TryParse(args[2].ToString(), out int quantityInt);
                        if (quantityCInt)
                        {
                            UserCharacter.removeCurrency(type, quantityInt);
                        }
                        else
                        {
                            Logger.Error("Bad syntax");
                        }
                    }
                }
                else if (type == 0 || type == 1)
                {
                    bool quantityC = double.TryParse(args[2].ToString(), out double quantity);
                    if (quantityC)
                    {
                        UserCharacter.removeCurrency(type, quantity);
                    }
                }
                else
                {
                    Logger.Error("Bad syntax");
                }
            }
            else
            {
                Logger.Error("Bad syntax");
            }
        }

        private void AdminAddXp([FromSource] Player source, List<object> args)
        {
            bool idC = int.TryParse(args[0].ToString(), out int id);
            bool quantityC = int.TryParse(args[1].ToString(), out int quantity);
            dynamic UserCharacter = LoadConfig.VORPCORE.getUser(id).getUsedCharacter;
            if (idC && quantityC)
            {
                UserCharacter.addXp(quantity);
            }
            else
            {
                Logger.Error("Bad syntax");
            }
        }

        private void AdminRemoveXp([FromSource] Player source, List<object> args)
        {
            bool idC = int.TryParse(args[0].ToString(), out int id);
            bool quantityC = int.TryParse(args[1].ToString(), out int quantity);
            dynamic UserCharacter = LoadConfig.VORPCORE.getUser(id).getUsedCharacter;
            if (idC && quantityC)
            {
                UserCharacter.removeXp(quantity);
            }
            else
            {
                Logger.Error("Bad syntax");
            }
        }

        private void AdminAddItem([FromSource] Player source, List<object> args)
        {
            bool idC = int.TryParse(args[0].ToString(), out int id);
            string item = args[1].ToString();
            bool quantityC = int.TryParse(args[2].ToString(), out int quantity);


            if (idC && quantityC)
            {
                Exports["ghmattimysql"].execute("SELECT * FROM items WHERE item=(?)", new[] { item }, new Action<dynamic>((result) =>
                {
                    if (result.Count != 0)
                    {
                        TriggerEvent("vorpCore:addItem", id, item, quantity);
                    }
                    else
                    {
                        Logger.Error(item + " doesn't exist in db");
                    }

                }));

            }
            else
            {
                Logger.Error("Bad syntax");
            }
        }

        private void AdminDelItem([FromSource] Player source, List<object> args)
        {
            bool idC = int.TryParse(args[0].ToString(), out int id);
            string item = args[1].ToString();
            bool quantityC = int.TryParse(args[2].ToString(), out int quantity);
            if (idC && quantityC)
            {
                TriggerEvent("vorpCore:subItem", id, item, quantity);
            }
            else
            {
                Logger.Error("Bad syntax");
            }
        }

        private void AdminAddWeapon([FromSource] Player source, List<object> args)
        {
            if (args.Count != 4)
            {
                Logger.Error("There are 4 arguments in /addweapon");
                return;
            }

            if (!int.TryParse(args[0].ToString(), out int playerId))
            {
                Logger.Error($"{args[0]} is not a valid player ID");
                return;
            }

            string weaponHash = args[1].ToString();
            string ammo = args[2].ToString();

            if (!int.TryParse(args[3].ToString(), out int quantity))
            {
                Logger.Error($"{args[3]} is not a proper ammo amount");
            }

            Dictionary<string, int> ammoAux = new Dictionary<string, int>
            {
                { ammo, quantity }
            };

            TriggerEvent("vorpCore:registerWeapon", playerId, weaponHash, ammoAux, new System.Dynamic.ExpandoObject());
        }

        private void GetInventory([FromSource] Player source, List<object> args)
        {
            int idPlayer = int.Parse(args[0].ToString());
            TriggerEvent("vorpCore:getUserInventory", idPlayer, new Action<dynamic>((items) =>
            {
                source.TriggerEvent("vorp:loadPlayerInventory", items);
            }));
        }
    }
}
