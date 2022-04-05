using MenuAPI;

using vorpadminmenu_cl.Functions.Boosters;
using vorpadminmenu_cl.Functions.Utils;

namespace vorpadminmenu_cl.Menus
{
    public class Boosters
    {
        private static bool _setupDone = false;
        private readonly static Menu _boostersMenu = new Menu(GetConfig.Langs["MenuBoostersTitle"], GetConfig.Langs["MenuBoostersDesc"]);
        private readonly static MenuCheckboxItem _gmode = new MenuCheckboxItem(GetConfig.Langs["GodModeTitle"], GetConfig.Langs["GodModeDesc"], false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private readonly static MenuCheckboxItem _tmode = new MenuCheckboxItem(GetConfig.Langs["ThorTitle"], GetConfig.Langs["ThorDesc"], false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private readonly static MenuCheckboxItem _nclip = new MenuCheckboxItem(GetConfig.Langs["NoClipTitle"], GetConfig.Langs["NoClipDesc"], false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private readonly static MenuCheckboxItem _mclip = new MenuCheckboxItem(GetConfig.Langs["NoClip2Title"], GetConfig.Langs["NoClip2Desc"], false)
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

            _boostersMenu.AddMenuItem(_gmode);
            _boostersMenu.AddMenuItem(_tmode);
            _boostersMenu.AddMenuItem(_nclip);
            _boostersMenu.AddMenuItem(_mclip);

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
                else if (_index == 5)
                {
                    dynamic ped = await UtilsFunctions.GetInput(GetConfig.Langs["HorseTitle"], GetConfig.Langs["HorseTitle"]);
                    MainMenu.args.Add(ped);
                    BoosterFunctions.Horse(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 6)
                {
                    dynamic veh = await UtilsFunctions.GetInput(GetConfig.Langs["VehicleTitle"], GetConfig.Langs["VehicleDesc"]);
                    MainMenu.args.Add(veh);
                    BoosterFunctions.Vehicle(MainMenu.args);
                    MainMenu.args.Clear();
                }
                else if (_index == 7)
                {
                    BoosterFunctions.InfiniteAmmo();
                }
                else if (_index == 8)
                {
                    BoosterFunctions.InfiniteAmmoOff();
                }
            };

            _boostersMenu.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 3)
                {
                    BoosterFunctions.NoClipMode("v1", false);
                }
                else if (_index == 4)
                {
                    BoosterFunctions.NoClipMode("v2", false);
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

        public static bool Getgmode()
        {
            return _gmode.Checked;
        }

        public static void Setgmode(bool gMode)
        {
            _gmode.Checked = gMode;
        }

        public static bool Gettmode()
        {
            return _tmode.Checked;
        }

        public static void Settmode(bool tMode)
        {
            _tmode.Checked = tMode;
        }

        public static bool Getnclip()
        {
            return _nclip.Checked;
        }

        public static void Setnclip(bool nClip)
        {
            _nclip.Checked = nClip;
            _mclip.Checked = false;
        }

        public static bool Getmclip()
        {
            return _mclip.Checked;
        }

        public static void Setmclip(bool mClip)
        {
            _mclip.Checked = mClip;
            _nclip.Checked = false;
        }
        #endregion
    }
}
