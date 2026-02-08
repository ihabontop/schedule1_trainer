using MelonLoader;
using Il2CppScheduleOne.PlayerScripts;
using UnityEngine;

namespace Schedule1Mod
{
    public static class MoneyManager
    {
        private static Il2CppScheduleOne.Money.MoneyManager cachedMM;

        private static Il2CppScheduleOne.Money.MoneyManager GetMoneyManager()
        {
            // Use cache if still valid
            if (cachedMM != null) return cachedMM;

            try
            {
                // Try FindObjectOfType
                cachedMM = Object.FindObjectOfType<Il2CppScheduleOne.Money.MoneyManager>();
                if (cachedMM != null) return cachedMM;

                // Try via Player.Local
                var player = Player.Local;
                if (player == null) return null;
                cachedMM = player.GetComponentInChildren<Il2CppScheduleOne.Money.MoneyManager>();
            }
            catch { }

            return cachedMM;
        }

        public static float GetCash()
        {
            try
            {
                var mm = GetMoneyManager();
                if (mm == null) return -1f;
                return mm.cashBalance;
            }
            catch { return -1f; }
        }

        public static float GetOnline()
        {
            try
            {
                var mm = GetMoneyManager();
                if (mm == null) return -1f;
                return mm.onlineBalance;
            }
            catch { return -1f; }
        }

        public static string AddCash(float amount)
        {
            try
            {
                var mm = GetMoneyManager();
                if (mm == null) return "Not in game!";
                mm.ChangeCashBalance(amount);
                return $"+${amount:N0}";
            }
            catch (System.Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string RemoveCash(float amount)
        {
            try
            {
                var mm = GetMoneyManager();
                if (mm == null) return "Not in game!";
                mm.ChangeCashBalance(-amount);
                return $"-${amount:N0} cash";
            }
            catch (System.Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string AddOnline(float amount)
        {
            try
            {
                var mm = GetMoneyManager();
                if (mm == null) return "Not in game!";
                mm.onlineBalance = mm.onlineBalance + amount;
                return $"+${amount:N0} bank";
            }
            catch (System.Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string RemoveOnline(float amount)
        {
            try
            {
                var mm = GetMoneyManager();
                if (mm == null) return "Not in game!";
                float newBalance = mm.onlineBalance - amount;
                if (newBalance < 0) newBalance = 0;
                mm.onlineBalance = newBalance;
                return $"-${amount:N0} bank";
            }
            catch (System.Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
