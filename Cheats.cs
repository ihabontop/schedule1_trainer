using Il2CppScheduleOne.PlayerScripts;
using Il2CppScheduleOne.PlayerScripts.Health;

namespace Schedule1Mod
{
    public static class Cheats
    {
        private static float origWalkSpeed = -1f;
        private static float origSprintMult = -1f;

        public static void ApplyGodMode()
        {
            try
            {
                var player = Player.Local;
                if (player == null) return;
                var health = player.Health;
                if (health == null) return;
                if (health.CurrentHealth < PlayerHealth.MAX_HEALTH)
                    health.SetHealth(PlayerHealth.MAX_HEALTH);
            }
            catch { }
        }

        public static void ApplyInfiniteEnergy()
        {
            try
            {
                var player = Player.Local;
                if (player == null) return;
                var energy = player.Energy;
                if (energy == null) return;
                if (energy.CurrentEnergy < PlayerEnergy.MAX_ENERGY)
                    energy.SetEnergy(PlayerEnergy.MAX_ENERGY);
            }
            catch { }
        }

        public static void ApplyPoliceIgnore()
        {
            try
            {
                var officers = Il2CppScheduleOne.Police.PoliceOfficer.Officers;
                if (officers == null) return;
                for (int i = 0; i < officers.Count; i++)
                {
                    var officer = officers[i];
                    if (officer == null) continue;
                    if (!officer.IgnorePlayers)
                        officer.SetIgnorePlayers(true);
                }
            }
            catch { }
        }

        public static void DisablePoliceIgnore()
        {
            try
            {
                var officers = Il2CppScheduleOne.Police.PoliceOfficer.Officers;
                if (officers == null) return;
                for (int i = 0; i < officers.Count; i++)
                {
                    var officer = officers[i];
                    if (officer == null) continue;
                    if (officer.IgnorePlayers)
                        officer.SetIgnorePlayers(false);
                }
            }
            catch { }
        }

        public static void ApplySpeedHack()
        {
            try
            {
                // Save originals on first call
                if (origWalkSpeed < 0)
                {
                    origWalkSpeed = PlayerMovement.WalkSpeed;
                    origSprintMult = PlayerMovement.SprintMultiplier;
                }
                PlayerMovement.SprintMultiplier = 3f;
                PlayerMovement.WalkSpeed = 10f;
            }
            catch { }
        }

        public static void ResetSpeed()
        {
            try
            {
                if (origWalkSpeed > 0)
                {
                    PlayerMovement.WalkSpeed = origWalkSpeed;
                    PlayerMovement.SprintMultiplier = origSprintMult;
                }
                else
                {
                    PlayerMovement.WalkSpeed = 5f;
                    PlayerMovement.SprintMultiplier = 1.6f;
                }
            }
            catch { }
        }

        public static void ClearWantedLevel()
        {
            try
            {
                var player = Player.Local;
                if (player == null) return;
                var crimeData = player.CrimeData;
                if (crimeData == null) return;
                crimeData.ClearCrimes();
                crimeData.SetPursuitLevel(PlayerCrimeData.EPursuitLevel.None);
            }
            catch { }
        }
    }
}
