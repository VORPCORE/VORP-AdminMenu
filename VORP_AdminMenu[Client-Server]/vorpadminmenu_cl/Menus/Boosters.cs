using CitizenFX.Core;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vorpadminmenu_cl.Functions.Boosters;

namespace vorpadminmenu_cl.Menus
{
    class Boosters
    {
        private static Menu boostersMenu = new Menu(GetConfig.Langs["MenuBoostersTitle"], GetConfig.Langs["MenuBoostersDesc"]);
        private static MenuCheckboxItem gmode = new MenuCheckboxItem("Godmode", "Press here to be god or Command:/gm", false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private static MenuCheckboxItem tmode = new MenuCheckboxItem("Thor", "Press here to be Thor or: Command:/thor. After activate it use mouse3 to throw lightnings", false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private static MenuCheckboxItem nclip = new MenuCheckboxItem("Noclip", "Press here to use noclip or: Command:/n. Mouse rotation", false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private static MenuCheckboxItem mclip = new MenuCheckboxItem("NoclipV2", "Press here to use the improved noclip or: Mouse direction.", false)
        {
            Style = MenuCheckboxItem.CheckboxStyle.Tick
        };
        private static bool setupDone = false;
        private static void SetupMenu()
        {
            if (setupDone) return;
            setupDone = true;
            MenuController.AddMenu(boostersMenu);

            boostersMenu.AddMenuItem(new MenuItem("Golden", "Press here to be gold or: Command:/golden")
            {
                Enabled = true,
            });

            boostersMenu.AddMenuItem(gmode);

            boostersMenu.AddMenuItem(tmode);

            boostersMenu.AddMenuItem(nclip);

            boostersMenu.AddMenuItem(mclip);

            boostersMenu.OnCheckboxChange += (_menu, _item, _index, _checked) =>
            {
                if (_index == 3)
                {
                    BoosterFunctions.SetClip(_checked);
                    if (_checked) { mclip.Checked = false; };
                }
                else if (_index == 4)
                {
                    BoosterFunctions.SetClip(_checked);
                    if (_checked) { nclip.Checked = false; };
                }
                //poner el checked en true o false
            };

        }
        public static Menu GetMenu()
        {
            SetupMenu();
            return boostersMenu;
        }

        public static bool Getgmode()
        {
            return gmode.Checked;
        }

        public static void Setgmode(bool gMode)
        {
            gmode.Checked = gMode;
        }

        public static bool Gettmode()
        {
            return tmode.Checked;
        }

        public static void Settmode(bool tMode)
        {
            tmode.Checked = tMode;
        }

        public static bool Getnclip()
        {
            return nclip.Checked;
        }

        public static void Setnclip(bool nClip)
        {
            nclip.Checked = nClip;
            mclip.Checked = false;
        }

        public static bool Getmclip()
        {
            return mclip.Checked;
        }

        public static void Setmclip(bool mClip)
        {
            mclip.Checked = mClip;
            nclip.Checked = false;
        }
    }
}
