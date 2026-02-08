using MelonLoader;
using UnityEngine;

namespace Schedule1Mod
{
    public class Schedule1Trainer : MelonMod
    {
        public static bool MenuVisible = false;
        public static bool EspEnabled = false;
        public static bool GodMode = false;
        public static bool InfiniteEnergy = false;
        public static bool PoliceIgnore = false;
        public static bool SpeedHack = false;

        private static CursorLockMode previousLockState;
        private static bool previousCursorVisible;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Schedule1Trainer loaded! F1 = Menu, F2 = ESP");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                MenuVisible = !MenuVisible;
                if (MenuVisible)
                {
                    previousLockState = Cursor.lockState;
                    previousCursorVisible = Cursor.visible;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = previousLockState;
                    Cursor.visible = previousCursorVisible;
                }
            }

            if (Input.GetKeyDown(KeyCode.F2))
                EspEnabled = !EspEnabled;

            // Apply continuous cheats
            if (GodMode) Cheats.ApplyGodMode();
            if (InfiniteEnergy) Cheats.ApplyInfiniteEnergy();
            if (SpeedHack) Cheats.ApplySpeedHack();
            if (PoliceIgnore) Cheats.ApplyPoliceIgnore();
        }

        public override void OnGUI()
        {
            if (MenuVisible)
            {
                // Keep cursor unlocked while menu is open
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ModMenu.Draw();
            }

            if (EspEnabled)
                PoliceESP.Draw();
        }
    }
}
