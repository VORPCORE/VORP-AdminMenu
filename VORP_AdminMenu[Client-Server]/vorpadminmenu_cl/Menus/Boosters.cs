using MenuAPI;

using vorpadminmenu_cl.Functions.Boosters;
using vorpadminmenu_cl.Functions.Utils;

namespace vorpadminmenu_cl.Menus
{
    public class Boosters
    {
        private static bool _setupDone = false;
        private readonly static Menu _boostersMenu = new Menu(GetConfig.Langs["MenuBoostersTitle"], GetConfig.Langs["MenuBoostersDesc"]);
        private readonly static MenuCheckboxItem _godModeCheckbox = new MenuCheckboxItem(GetConfig.Langs["GodModeTitle"], GetConfig.Langs["GodModeDesc"], false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private readonly static MenuCheckboxItem _thorModeCheckbox = new MenuCheckboxItem(GetConfig.Langs["ThorTitle"], GetConfig.Langs["ThorDesc"], false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private readonly static MenuCheckboxItem _noClipCheckbox = new MenuCheckboxItem(GetConfig.Langs["NoClip2Title"], GetConfig.Langs["NoClip2Desc"], false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };

        #region Private Method
        private static void SetupMenu()
        {
            if (_setupDone)
            {
                return;
            }

            _setupDone = true;
            MenuController.AddMenu(_boostersMenu);

            _boostersMenu.AddMenuItem(new MenuItem(GetConfig.Langs["GoldenTitle"], GetConfig.Langs["GoldenDesc"])
            {
                Enabled = true,
            });

            _boostersMenu.AddMenuItem(_godModeCheckbox);
            _boostersMenu.AddMenuItem(_thorModeCheckbox);
            _boostersMenu.AddMenuItem(_noClipCheckbox);

            _boostersMenu.AddMenuItem(new MenuItem(GetConfig.Langs["HorseTitle"], GetConfig.Langs["HorseDesc"])
            {
                Enabled = true,
            });

            _boostersMenu.AddMenuItem(new MenuItem(GetConfig.Langs["VehicleTitle"], GetConfig.Langs["VehicleDesc"])
            {
                Enabled = true,
            });
            _boostersMenu.AddMenuItem(new MenuItem(GetConfig.Langs["InfiniteAmmoOnTitle"], GetConfig.Langs["InfiniteAmmoOnDesc"])
            {
                Enabled = true,
            });

            _boostersMenu.AddMenuItem(new MenuItem(GetConfig.Langs["InfiniteAmmoOffTitle"], GetConfig.Langs["InfiniteAmmoOffDesc"])
            {
                Enabled = true,
            });

            _boostersMenu.OnItemSelect += async (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    BoosterFunctions.Golden();
                }
                else if (_index == 4)
                {
                    dynamic ped = await UtilsFunctions.GetInput(GetConfig.Langs["HorseTitle"], GetConfig.Langs["HorseTitle"]);
                    MainMenu.args.Add(ped);
                    await BoosterFunctions.HorseAsync(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 5)
                {
                    dynamic veh = await UtilsFunctions.GetInput(GetConfig.Langs["VehicleTitle"], GetConfig.Langs["VehicleDesc"]);
                    MainMenu.args.Add(veh);
                    await BoosterFunctions.VehicleAsync(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 6)
                {
                    BoosterFunctions.InfiniteAmmo(true);
                }
                else if (_index == 7)
                {
                    BoosterFunctions.InfiniteAmmo(false);
                }
            };

            _boostersMenu.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 1)
                {
                    BoosterFunctions.GodMode(false);
                }
                else if (_index == 3)
                {
                    BoosterFunctions.NoClipMode(false);
                }
            };
        }
        #endregion

        #region Public Methods
        public static Menu GetMenu()
        {
            SetupMenu();
            return _boostersMenu;
        }

        public static bool GetGodModeCheckboxStatus()
        {
            return _godModeCheckbox.Checked;
        }

        public static void SetGodMode(bool gMode)
        {
            _godModeCheckbox.Checked = gMode;
        }

        public static bool GetNoClipCheckboxStatus()
        {
            return _noClipCheckbox.Checked;
        }

        public static void SetNoClipCheckboxStatus(bool noClip)
        {
            _noClipCheckbox.Checked = noClip;
        }

        public static bool GetThorModeCheckboxStatus()
        {
            return _thorModeCheckbox.Checked;
        }

        public static void SetThorModeCheckboxStatus(bool status)
        {
            _thorModeCheckbox.Checked = status;
        }
        #endregion
    }
}
