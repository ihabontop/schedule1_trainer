using Il2CppScheduleOne.PlayerScripts;
using Il2CppScheduleOne.PlayerScripts.Health;
using UnityEngine;

namespace Schedule1Mod
{
    public static class Cheats
    {
        private static float origWalkSpeed = -1f;
        private static float origSprintMult = -1f;

        private static Il2CppScheduleOne.Equipping.Equippable_RangedWeapon cachedWeapon;
        private static float weaponCacheTime;

        private static Il2CppScheduleOne.Equipping.Equippable_RangedWeapon GetRangedWeapon()
        {
            try
            {
                // Return cached weapon if still valid
                if (cachedWeapon != null && Time.time - weaponCacheTime < 0.5f)
                    return cachedWeapon;

                // Throttle search when no weapon found (avoid FPS drop)
                if (cachedWeapon == null && Time.time - weaponCacheTime < 0.5f)
                    return null;

                weaponCacheTime = Time.time;
                var player = Player.Local;
                if (player == null) { cachedWeapon = null; return null; }
                cachedWeapon = player.GetComponentInChildren<Il2CppScheduleOne.Equipping.Equippable_RangedWeapon>();
                return cachedWeapon;
            }
            catch { cachedWeapon = null; return null; }
        }

        private static int origMagazineSize = -1;

        public static void ApplyInfiniteAmmo()
        {
            try
            {
                var weapon = GetRangedWeapon();
                if (weapon == null) return;
                // Save original magazine size
                if (origMagazineSize < 0)
                    origMagazineSize = weapon.MagazineSize;
                // Set huge magazine
                if (weapon.MagazineSize < 9999)
                    weapon.MagazineSize = 9999;
                // Auto-reload instantly when empty
                if (weapon.Ammo <= 0)
                {
                    weapon.Reload();
                    weapon.IsReloading = false;
                }
            }
            catch { }
        }

        public static void ResetMagazine()
        {
            try
            {
                if (origMagazineSize > 0)
                {
                    var weapon = GetRangedWeapon();
                    if (weapon != null)
                        weapon.MagazineSize = origMagazineSize;
                    origMagazineSize = -1;
                }
            }
            catch { }
        }

        public static void ApplyNoReload()
        {
            try
            {
                var weapon = GetRangedWeapon();
                if (weapon == null) return;
                if (weapon.IsReloading)
                    weapon.IsReloading = false;
            }
            catch { }
        }

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
