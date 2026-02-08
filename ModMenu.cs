using UnityEngine;
using System.Collections.Generic;

namespace Schedule1Mod
{
    public static class ModMenu
    {
        private static Rect windowRect = new Rect(40, 40, 460, 700);

        // State
        private static bool init = false;
        private static string statusMsg = "";
        private static float statusTimer = 0f;

        // Animation states
        private static Dictionary<string, float> hoverStates = new Dictionary<string, float>();
        private static Dictionary<string, float> toggleAnims = new Dictionary<string, float>();

        // Textures
        private static Texture2D texWhite;

        // Styles
        private static GUIStyle sWin;
        private static GUIStyle sBtnLabel;
        private static GUIStyle sText;

        // ---- Colors ----
        private static readonly Color colBg         = new Color(0.04f, 0.05f, 0.09f, 0.96f);
        private static readonly Color colHeader     = new Color(0.05f, 0.06f, 0.11f, 1f);
        private static readonly Color colCard       = new Color(0.07f, 0.09f, 0.15f, 0.95f);
        private static readonly Color colCardBorder = new Color(0.12f, 0.16f, 0.24f, 0.4f);

        private static readonly Color colAccent     = new Color(0.39f, 0.40f, 0.95f, 1f);
        private static readonly Color colAccentBright= new Color(0.51f, 0.55f, 1f, 1f);

        private static readonly Color colGreen      = new Color(0.06f, 0.73f, 0.51f, 1f);
        private static readonly Color colGreenDim   = new Color(0.04f, 0.45f, 0.32f, 1f);
        private static readonly Color colGreenBg    = new Color(0.04f, 0.35f, 0.25f, 0.4f);

        private static readonly Color colRed        = new Color(0.94f, 0.27f, 0.27f, 1f);
        private static readonly Color colRedDim     = new Color(0.55f, 0.15f, 0.15f, 1f);
        private static readonly Color colRedBg      = new Color(0.45f, 0.12f, 0.12f, 0.4f);

        private static readonly Color colCyan       = new Color(0.02f, 0.71f, 0.83f, 1f);
        private static readonly Color colCyanDim    = new Color(0.02f, 0.45f, 0.53f, 1f);
        private static readonly Color colCyanBg     = new Color(0.02f, 0.35f, 0.42f, 0.4f);

        private static readonly Color colOrange     = new Color(0.96f, 0.62f, 0.04f, 1f);
        private static readonly Color colOrangeDim  = new Color(0.60f, 0.38f, 0.02f, 1f);
        private static readonly Color colOrangeBg   = new Color(0.50f, 0.30f, 0.02f, 0.3f);

        private static readonly Color colTextPri    = new Color(0.95f, 0.96f, 0.97f, 1f);
        private static readonly Color colTextSec    = new Color(0.58f, 0.64f, 0.72f, 1f);
        private static readonly Color colTextMuted  = new Color(0.39f, 0.45f, 0.55f, 1f);

        // ======================== INIT ========================

        private static Texture2D MakeTex(Color c)
        {
            var t = new Texture2D(2, 2);
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    t.SetPixel(i, j, c);
            t.Apply();
            return t;
        }

        private static void Init()
        {
            if (init) return;

            texWhite = MakeTex(Color.white);

            var bgTex = MakeTex(colBg);
            sWin = new GUIStyle(GUI.skin.window);
            sWin.normal.background = bgTex;
            sWin.onNormal.background = bgTex;
            sWin.normal.textColor = Color.clear;
            sWin.padding = new RectOffset(0, 0, 0, 0);
            sWin.border = new RectOffset(0, 0, 0, 0);
            sWin.overflow = new RectOffset(0, 0, 0, 0);

            sBtnLabel = new GUIStyle(GUI.skin.label);
            sBtnLabel.fontSize = 13;
            sBtnLabel.fontStyle = FontStyle.Bold;
            sBtnLabel.alignment = TextAnchor.MiddleCenter;
            sBtnLabel.normal.textColor = colTextPri;

            sText = new GUIStyle(GUI.skin.label);
            sText.fontSize = 14;
            sText.normal.textColor = colTextSec;

            init = true;
        }

        // ======================== DRAW HELPERS ========================

        private static void FillRect(Rect r, Color c)
        {
            if (c.a < 0.001f) return;
            Color prev = GUI.color;
            GUI.color = c;
            GUI.DrawTexture(r, texWhite);
            GUI.color = prev;
        }

        private static void DrawBorder(Rect r, Color c, float t = 1f)
        {
            FillRect(new Rect(r.x, r.y, r.width, t), c);
            FillRect(new Rect(r.x, r.y + r.height - t, r.width, t), c);
            FillRect(new Rect(r.x, r.y + t, t, r.height - t * 2), c);
            FillRect(new Rect(r.x + r.width - t, r.y + t, t, r.height - t * 2), c);
        }

        private static void DrawCard(Rect r)
        {
            FillRect(r, colCard);
            DrawBorder(r, colCardBorder);
        }

        // Label helper to avoid style mutation issues
        private static void Label(Rect r, string text, int size, FontStyle fs, TextAnchor align, Color col)
        {
            sText.fontSize = size;
            sText.fontStyle = fs;
            sText.alignment = align;
            sText.normal.textColor = col;
            GUI.Label(r, text, sText);
        }

        // ======================== ANIMATION ========================

        private static float SmoothHover(string id, bool hovered)
        {
            if (!hoverStates.ContainsKey(id)) hoverStates[id] = 0f;
            if (Event.current.type == EventType.Repaint)
            {
                float target = hovered ? 1f : 0f;
                hoverStates[id] = Mathf.Lerp(hoverStates[id], target, Time.deltaTime * 12f);
                if (Mathf.Abs(hoverStates[id] - target) < 0.01f)
                    hoverStates[id] = target;
            }
            return hoverStates[id];
        }

        private static float SmoothToggle(string id, bool on)
        {
            if (!toggleAnims.ContainsKey(id)) toggleAnims[id] = on ? 1f : 0f;
            if (Event.current.type == EventType.Repaint)
            {
                float target = on ? 1f : 0f;
                toggleAnims[id] = Mathf.Lerp(toggleAnims[id], target, Time.deltaTime * 10f);
                if (Mathf.Abs(toggleAnims[id] - target) < 0.01f)
                    toggleAnims[id] = target;
            }
            return toggleAnims[id];
        }

        // ======================== INTERACTIVE ELEMENTS ========================

        private static bool Btn(Rect r, string text, string id,
            Color bgIdle, Color bgHover, Color textIdle)
        {
            bool hovered = r.Contains(Event.current.mousePosition);
            float h = SmoothHover(id, hovered);

            // Outer glow on hover
            if (h > 0.01f)
            {
                Color glow = new Color(bgHover.r, bgHover.g, bgHover.b, 0.12f * h);
                FillRect(new Rect(r.x - 1, r.y - 1, r.width + 2, r.height + 2), glow);
            }

            // Background
            FillRect(r, Color.Lerp(bgIdle, bgHover, h));

            // Border
            Color border = new Color(bgHover.r, bgHover.g, bgHover.b,
                Mathf.Lerp(0.15f, 0.5f, h));
            DrawBorder(r, border);

            // Text
            sBtnLabel.normal.textColor = Color.Lerp(textIdle, Color.white, h * 0.5f);
            GUI.Label(r, text, sBtnLabel);

            // Click
            if (hovered && Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                Event.current.Use();
                return true;
            }
            return false;
        }

        private static bool Toggle(Rect r, string label, string id, bool isOn)
        {
            bool hovered = r.Contains(Event.current.mousePosition);
            float h = SmoothHover("th_" + id, hovered);
            float t = SmoothToggle(id, isOn);

            // Background
            Color bgOff = new Color(0.07f, 0.09f, 0.14f, 0.95f);
            Color bgHov = new Color(0.10f, 0.13f, 0.20f, 0.95f);
            FillRect(r, Color.Lerp(bgOff, bgHov, h));

            // Left accent bar
            if (t > 0.01f)
                FillRect(new Rect(r.x, r.y, 3f, r.height),
                    new Color(colAccent.r, colAccent.g, colAccent.b, t));

            // Border with accent glow
            float ba = Mathf.Lerp(0.08f, 0.3f, Mathf.Max(h * 0.5f, t * 0.4f));
            Color bc = isOn
                ? new Color(colAccent.r, colAccent.g, colAccent.b, ba)
                : new Color(1f, 1f, 1f, ba);
            DrawBorder(r, bc);

            // Label
            Label(new Rect(r.x + 14, r.y, r.width - 66, r.height),
                label, 13, FontStyle.Bold, TextAnchor.MiddleLeft,
                Color.Lerp(colTextMuted, colTextPri, Mathf.Max(h, t)));

            // Toggle track
            float tw = 38f, tth = 20f;
            float tx = r.x + r.width - tw - 12f;
            float ty = r.y + (r.height - tth) / 2f;

            Color trackOff = new Color(0.15f, 0.18f, 0.25f, 1f);
            Color trackOn = new Color(colAccent.r * 0.4f, colAccent.g * 0.4f, colAccent.b * 0.4f, 1f);
            FillRect(new Rect(tx, ty, tw, tth), Color.Lerp(trackOff, trackOn, t));

            // Toggle dot
            float ds = 14f, dp = 3f;
            float dx = Mathf.Lerp(tx + dp, tx + tw - ds - dp, t);
            float dy = ty + (tth - ds) / 2f;

            Color dotOff = new Color(0.45f, 0.50f, 0.60f, 1f);
            Color dotOn = colAccent;
            FillRect(new Rect(dx, dy, ds, ds), Color.Lerp(dotOff, dotOn, t));

            // Dot glow when on
            if (t > 0.1f)
                FillRect(new Rect(dx - 2, dy - 2, ds + 4, ds + 4),
                    new Color(colAccent.r, colAccent.g, colAccent.b, 0.25f * t));

            // Click
            if (hovered && Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                Event.current.Use();
                return !isOn;
            }
            return isOn;
        }

        // ======================== MAIN ========================

        public static void Draw()
        {
            Init();
            windowRect = GUI.Window(9999, windowRect, (GUI.WindowFunction)DrawWindow, "", sWin);
        }

        private static void SetStatus(string msg)
        {
            statusMsg = msg;
            statusTimer = Time.time;
        }

        private static void DrawWindow(int id)
        {
            float W = 460f;
            float M = 20f;
            float IW = W - M * 2;
            float y = 0f;

            // ============ HEADER ============
            FillRect(new Rect(0, 0, W, 70), colHeader);

            // Top accent with pulse
            float pulse = (Mathf.Sin(Time.time * 2f) + 1f) / 2f;
            Color ap = Color.Lerp(colAccent, colAccentBright, pulse * 0.3f);
            FillRect(new Rect(0, 0, W, 2), ap);
            FillRect(new Rect(0, 2, W, 3),
                new Color(colAccent.r, colAccent.g, colAccent.b, 0.06f + pulse * 0.04f));

            // Bottom accent
            FillRect(new Rect(0, 68, W, 2), colAccent);

            // Title
            Label(new Rect(0, 12, W, 30), "SCHEDULE I TRAINER",
                22, FontStyle.Bold, TextAnchor.MiddleCenter, colAccent);
            Label(new Rect(0, 42, W, 20), "v1.0  \u2022  F1 Menu  \u2022  F2 ESP",
                13, FontStyle.Normal, TextAnchor.MiddleCenter, colTextMuted);

            y = 80f;

            // ============ MONEY SECTION ============
            float moneyStart = y;
            // Pre-draw card background (content drawn on top)
            float moneyH = 12 + 36 + 28 + 34 + 20 + (32 + 4) + (32 + 12) + 20 + (32 + 4) + (32 + 10);
            DrawCard(new Rect(M - 2, moneyStart, IW + 4, moneyH));
            y += 12f;

            Label(new Rect(M + 10, y, IW, 20), "\u25B8  MONEY",
                14, FontStyle.Bold, TextAnchor.MiddleLeft, colAccent);
            y += 26f;

            FillRect(new Rect(M + 10, y, IW - 20, 1), colCardBorder);
            y += 10f;

            // Cash & Bank display
            float cash = MoneyManager.GetCash();
            float online = MoneyManager.GetOnline();
            string cashStr = cash >= 0 ? $"${cash:N0}" : "N/A";
            string onlineStr = online >= 0 ? $"${online:N0}" : "N/A";

            Label(new Rect(M + 14, y, 100, 26), "Cash",
                14, FontStyle.Normal, TextAnchor.MiddleLeft, colTextSec);
            Label(new Rect(M + 100, y, IW - 120, 26), cashStr,
                16, FontStyle.Bold, TextAnchor.MiddleRight, colGreen);
            y += 28f;

            Label(new Rect(M + 14, y, 100, 26), "Bank",
                14, FontStyle.Normal, TextAnchor.MiddleLeft, colTextSec);
            Label(new Rect(M + 100, y, IW - 120, 26), onlineStr,
                16, FontStyle.Bold, TextAnchor.MiddleRight, colCyan);
            y += 34f;

            float gap = 8f;
            float bw = (IW - gap * 3 - 20) / 4f;
            float bh = 32f;
            float bx;

            // CASH label
            Label(new Rect(M + 10, y, IW, 16), "CASH",
                11, FontStyle.Bold, TextAnchor.MiddleLeft, colTextMuted);
            y += 20f;

            // Add cash
            bx = M + 10;
            if (Btn(new Rect(bx, y, bw, bh), "+$1K", "ca1", colGreenBg, colGreenDim, colGreen))
                SetStatus(MoneyManager.AddCash(1000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "+$10K", "ca2", colGreenBg, colGreenDim, colGreen))
                SetStatus(MoneyManager.AddCash(10000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "+$100K", "ca3", colGreenBg, colGreenDim, colGreen))
                SetStatus(MoneyManager.AddCash(100000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "+$1M", "ca4", colGreenBg, colGreenDim, colGreen))
                SetStatus(MoneyManager.AddCash(1000000f));
            y += bh + 4f;

            // Remove cash
            bx = M + 10;
            if (Btn(new Rect(bx, y, bw, bh), "-$1K", "cr1", colRedBg, colRedDim, colRed))
                SetStatus(MoneyManager.RemoveCash(1000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "-$10K", "cr2", colRedBg, colRedDim, colRed))
                SetStatus(MoneyManager.RemoveCash(10000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "-$100K", "cr3", colRedBg, colRedDim, colRed))
                SetStatus(MoneyManager.RemoveCash(100000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "-$1M", "cr4", colRedBg, colRedDim, colRed))
                SetStatus(MoneyManager.RemoveCash(1000000f));
            y += bh + 12f;

            // BANK label
            Label(new Rect(M + 10, y, IW, 16), "BANK",
                11, FontStyle.Bold, TextAnchor.MiddleLeft, colTextMuted);
            y += 20f;

            // Add bank
            bx = M + 10;
            if (Btn(new Rect(bx, y, bw, bh), "+$1K", "ba1", colCyanBg, colCyanDim, colCyan))
                SetStatus(MoneyManager.AddOnline(1000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "+$10K", "ba2", colCyanBg, colCyanDim, colCyan))
                SetStatus(MoneyManager.AddOnline(10000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "+$100K", "ba3", colCyanBg, colCyanDim, colCyan))
                SetStatus(MoneyManager.AddOnline(100000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "+$1M", "ba4", colCyanBg, colCyanDim, colCyan))
                SetStatus(MoneyManager.AddOnline(1000000f));
            y += bh + 4f;

            // Remove bank
            bx = M + 10;
            if (Btn(new Rect(bx, y, bw, bh), "-$1K", "br1", colRedBg, colRedDim, colRed))
                SetStatus(MoneyManager.RemoveOnline(1000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "-$10K", "br2", colRedBg, colRedDim, colRed))
                SetStatus(MoneyManager.RemoveOnline(10000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "-$100K", "br3", colRedBg, colRedDim, colRed))
                SetStatus(MoneyManager.RemoveOnline(100000f));
            bx += bw + gap;
            if (Btn(new Rect(bx, y, bw, bh), "-$1M", "br4", colRedBg, colRedDim, colRed))
                SetStatus(MoneyManager.RemoveOnline(1000000f));
            y += bh + 10f;
            y += 10f;

            // ============ CHEATS SECTION ============
            float cheatsStart = y;
            float cheatsH = 12 + 38 + (38 + 6) * 3 + (38 + 10);
            DrawCard(new Rect(M - 2, cheatsStart, IW + 4, cheatsH));
            y += 12f;

            Label(new Rect(M + 10, y, IW, 20), "\u25B8  CHEATS",
                14, FontStyle.Bold, TextAnchor.MiddleLeft, colAccent);
            y += 26f;

            FillRect(new Rect(M + 10, y, IW - 20, 1), colCardBorder);
            y += 12f;

            float hw = (IW - gap - 20) / 2f;
            float th = 38f;

            // Row 1
            Schedule1Trainer.GodMode = Toggle(
                new Rect(M + 10, y, hw, th), "God Mode", "godmode", Schedule1Trainer.GodMode);
            Schedule1Trainer.InfiniteEnergy = Toggle(
                new Rect(M + 10 + hw + gap, y, hw, th), "Inf. Energy", "infenergy", Schedule1Trainer.InfiniteEnergy);
            y += th + 6f;

            // Row 2
            bool newSpeed = Toggle(
                new Rect(M + 10, y, hw, th), "Speed x3", "speed", Schedule1Trainer.SpeedHack);
            if (newSpeed != Schedule1Trainer.SpeedHack)
            {
                Schedule1Trainer.SpeedHack = newSpeed;
                if (!newSpeed) Cheats.ResetSpeed();
            }
            bool newIgnore = Toggle(
                new Rect(M + 10 + hw + gap, y, hw, th), "Police Ignore", "polignore", Schedule1Trainer.PoliceIgnore);
            if (newIgnore != Schedule1Trainer.PoliceIgnore)
            {
                Schedule1Trainer.PoliceIgnore = newIgnore;
                if (!newIgnore) Cheats.DisablePoliceIgnore();
            }
            y += th + 6f;

            // Row 3
            bool newAmmo = Toggle(
                new Rect(M + 10, y, hw, th), "Inf. Ammo", "infammo", Schedule1Trainer.InfiniteAmmo);
            if (newAmmo != Schedule1Trainer.InfiniteAmmo)
            {
                Schedule1Trainer.InfiniteAmmo = newAmmo;
                if (!newAmmo) Cheats.ResetMagazine();
            }
            Schedule1Trainer.NoReload = Toggle(
                new Rect(M + 10 + hw + gap, y, hw, th), "No Reload", "noreload", Schedule1Trainer.NoReload);
            y += th + 6f;

            // Row 4
            Schedule1Trainer.EspEnabled = Toggle(
                new Rect(M + 10, y, hw, th), "Police ESP", "esp", Schedule1Trainer.EspEnabled);
            if (Btn(new Rect(M + 10 + hw + gap, y, hw, th), "\u26A0 Clear Wanted", "clearwanted",
                colOrangeBg, colOrangeDim, colOrange))
            {
                Cheats.ClearWantedLevel();
                SetStatus("Wanted level cleared!");
            }
            y += th + 10f;
            y += 10f;

            // ============ INFO BAR ============
            float infoStart = y;
            float infoH = Schedule1Trainer.EspEnabled ? 56f : 38f;
            DrawCard(new Rect(M - 2, infoStart, IW + 4, infoH));
            y += 8f;

            if (Schedule1Trainer.EspEnabled)
            {
                string espInfo = PoliceESP.GetStatus();
                Label(new Rect(M, y, IW, 18), espInfo,
                    12, FontStyle.Normal, TextAnchor.MiddleCenter, colAccent);
                y += 22f;
            }
            else
            {
                y += 4f;
            }

            Label(new Rect(M, y, IW, 18), "[F1] Menu    [F2] ESP    [ESC] Close",
                11, FontStyle.Normal, TextAnchor.MiddleCenter, colTextMuted);
            y += 26f;

            y += 6f;

            // ============ STATUS ============
            if (!string.IsNullOrEmpty(statusMsg) && Time.time - statusTimer < 2.5f)
            {
                float alpha = Mathf.Clamp01(2.5f - (Time.time - statusTimer));
                Label(new Rect(M, y, IW, 22), statusMsg,
                    13, FontStyle.Bold, TextAnchor.MiddleCenter,
                    new Color(colGreen.r, colGreen.g, colGreen.b, alpha));
            }

            // ============ BOTTOM ACCENT ============
            float totalH = y + 28f;
            FillRect(new Rect(0, totalH - 2, W, 2), colAccent);

            windowRect.height = totalH;
            GUI.DragWindow(new Rect(0, 0, W, 70));
        }
    }
}
