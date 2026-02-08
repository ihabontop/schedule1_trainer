using UnityEngine;

namespace Schedule1Mod
{
    public static class ModMenu
    {
        private static Rect windowRect = new Rect(40, 40, 360, 500);

        // ---- Textures ----
        private static bool init = false;
        private static Texture2D texBg;
        private static Texture2D texHeader;
        private static Texture2D texSection;
        private static Texture2D texAccentLine;
        private static Texture2D texBtnIdle;
        private static Texture2D texBtnHover;
        private static Texture2D texMoneyBtn;
        private static Texture2D texMoneyBtnHover;
        private static Texture2D texToggleOn;
        private static Texture2D texToggleOnHover;
        private static Texture2D texToggleOff;
        private static Texture2D texToggleOffHover;
        private static Texture2D texActionBtn;
        private static Texture2D texActionBtnHover;
        private static Texture2D texBorder;
        private static Texture2D texWhite;

        // ---- Styles ----
        private static GUIStyle sWin;
        private static GUIStyle sTitle;
        private static GUIStyle sSubtitle;
        private static GUIStyle sSectionHead;
        private static GUIStyle sLabel;
        private static GUIStyle sValue;
        private static GUIStyle sBtn;
        private static GUIStyle sMoneyBtn;
        private static GUIStyle sToggleOn;
        private static GUIStyle sToggleOff;
        private static GUIStyle sActionBtn;
        private static GUIStyle sHotkey;
        private static GUIStyle sStatus;

        // ---- Colors ----
        private static readonly Color colBg        = new Color(0.051f, 0.067f, 0.090f, 0.97f);
        private static readonly Color colHeader     = new Color(0.063f, 0.082f, 0.110f, 1f);
        private static readonly Color colSection    = new Color(0.075f, 0.094f, 0.125f, 0.95f);
        private static readonly Color colAccent     = new Color(0f, 0.831f, 1f, 1f);       // cyan
        private static readonly Color colAccentDim  = new Color(0f, 0.5f, 0.65f, 1f);
        private static readonly Color colGreen      = new Color(0f, 1f, 0.533f, 1f);       // #00ff88
        private static readonly Color colGreenDim   = new Color(0f, 0.65f, 0.35f, 1f);
        private static readonly Color colOrange     = new Color(1f, 0.42f, 0.21f, 1f);     // #ff6b35
        private static readonly Color colTextPri    = new Color(0.902f, 0.929f, 0.953f, 1f);
        private static readonly Color colTextSec    = new Color(0.545f, 0.580f, 0.620f, 1f);
        private static readonly Color colBtnIdle    = new Color(0.110f, 0.130f, 0.165f, 1f);
        private static readonly Color colBtnHover   = new Color(0.145f, 0.170f, 0.210f, 1f);
        private static readonly Color colToggleOn   = new Color(0f, 0.55f, 0.45f, 1f);
        private static readonly Color colToggleOnH  = new Color(0f, 0.65f, 0.53f, 1f);
        private static readonly Color colToggleOff  = new Color(0.160f, 0.175f, 0.200f, 1f);
        private static readonly Color colToggleOffH = new Color(0.200f, 0.215f, 0.245f, 1f);
        private static readonly Color colBorder     = new Color(0.188f, 0.212f, 0.239f, 0.5f);

        private static string statusMsg = "";
        private static float statusTimer = 0f;

        private static Texture2D Tex(Color c)
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

            // Textures
            texBg             = Tex(colBg);
            texHeader         = Tex(colHeader);
            texSection        = Tex(colSection);
            texAccentLine     = Tex(colAccent);
            texBtnIdle        = Tex(colBtnIdle);
            texBtnHover       = Tex(colBtnHover);
            texMoneyBtn       = Tex(colGreenDim);
            texMoneyBtnHover  = Tex(new Color(0f, 0.75f, 0.4f, 1f));
            texToggleOn       = Tex(colToggleOn);
            texToggleOnHover  = Tex(colToggleOnH);
            texToggleOff      = Tex(colToggleOff);
            texToggleOffHover = Tex(colToggleOffH);
            texActionBtn      = Tex(new Color(0.55f, 0.25f, 0.08f, 1f));
            texActionBtnHover = Tex(new Color(0.7f, 0.32f, 0.1f, 1f));
            texBorder         = Tex(colBorder);
            texWhite          = Tex(Color.white);

            // Window
            sWin = new GUIStyle(GUI.skin.window);
            sWin.normal.background = texBg;
            sWin.onNormal.background = texBg;
            sWin.normal.textColor = Color.clear; // we draw our own title
            sWin.padding = new RectOffset(0, 0, 0, 0);
            sWin.border = new RectOffset(0, 0, 0, 0);
            sWin.overflow = new RectOffset(0, 0, 0, 0);

            // Title
            sTitle = new GUIStyle(GUI.skin.label);
            sTitle.fontSize = 16;
            sTitle.fontStyle = FontStyle.Bold;
            sTitle.alignment = TextAnchor.MiddleCenter;
            sTitle.normal.textColor = colAccent;

            // Subtitle
            sSubtitle = new GUIStyle(GUI.skin.label);
            sSubtitle.fontSize = 10;
            sSubtitle.alignment = TextAnchor.MiddleCenter;
            sSubtitle.normal.textColor = colTextSec;

            // Section header
            sSectionHead = new GUIStyle(GUI.skin.label);
            sSectionHead.fontSize = 11;
            sSectionHead.fontStyle = FontStyle.Bold;
            sSectionHead.alignment = TextAnchor.MiddleLeft;
            sSectionHead.normal.textColor = colAccent;

            // Label
            sLabel = new GUIStyle(GUI.skin.label);
            sLabel.fontSize = 12;
            sLabel.normal.textColor = colTextSec;

            // Value
            sValue = new GUIStyle(GUI.skin.label);
            sValue.fontSize = 13;
            sValue.fontStyle = FontStyle.Bold;
            sValue.alignment = TextAnchor.MiddleRight;
            sValue.normal.textColor = colGreen;

            // Button base
            sBtn = new GUIStyle(GUI.skin.button);
            sBtn.normal.background = texBtnIdle;
            sBtn.hover.background = texBtnHover;
            sBtn.active.background = texBtnHover;
            sBtn.focused.background = texBtnIdle;
            sBtn.normal.textColor = colTextPri;
            sBtn.hover.textColor = Color.white;
            sBtn.active.textColor = Color.white;
            sBtn.fontSize = 11;
            sBtn.fontStyle = FontStyle.Bold;
            sBtn.alignment = TextAnchor.MiddleCenter;
            sBtn.border = new RectOffset(1, 1, 1, 1);

            // Money button
            sMoneyBtn = new GUIStyle(sBtn);
            sMoneyBtn.normal.background = texMoneyBtn;
            sMoneyBtn.hover.background = texMoneyBtnHover;
            sMoneyBtn.active.background = texMoneyBtnHover;
            sMoneyBtn.normal.textColor = new Color(0.85f, 1f, 0.9f, 1f);
            sMoneyBtn.hover.textColor = Color.white;

            // Toggle ON
            sToggleOn = new GUIStyle(sBtn);
            sToggleOn.normal.background = texToggleOn;
            sToggleOn.hover.background = texToggleOnHover;
            sToggleOn.active.background = texToggleOnHover;
            sToggleOn.normal.textColor = Color.white;

            // Toggle OFF
            sToggleOff = new GUIStyle(sBtn);
            sToggleOff.normal.background = texToggleOff;
            sToggleOff.hover.background = texToggleOffHover;
            sToggleOff.active.background = texToggleOffHover;
            sToggleOff.normal.textColor = colTextSec;
            sToggleOff.hover.textColor = colTextPri;

            // Action button
            sActionBtn = new GUIStyle(sBtn);
            sActionBtn.normal.background = texActionBtn;
            sActionBtn.hover.background = texActionBtnHover;
            sActionBtn.active.background = texActionBtnHover;
            sActionBtn.normal.textColor = new Color(1f, 0.9f, 0.8f, 1f);

            // Hotkey
            sHotkey = new GUIStyle(GUI.skin.label);
            sHotkey.fontSize = 10;
            sHotkey.alignment = TextAnchor.MiddleCenter;
            sHotkey.normal.textColor = colTextSec;

            // Status
            sStatus = new GUIStyle(GUI.skin.label);
            sStatus.fontSize = 11;
            sStatus.fontStyle = FontStyle.Bold;
            sStatus.alignment = TextAnchor.MiddleCenter;
            sStatus.normal.textColor = colGreen;

            init = true;
        }

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

        private static bool Toggle(Rect r, string label, bool on)
        {
            string icon = on ? "\u25CF " : "\u25CB ";
            string text = icon + label;
            if (GUI.Button(r, text, on ? sToggleOn : sToggleOff))
                return !on;
            return on;
        }

        private static void HLine(float x, float y, float w, Texture2D tex, float thickness = 1f)
        {
            GUI.DrawTexture(new Rect(x, y, w, thickness), tex);
        }

        private static void DrawWindow(int id)
        {
            float W = 360f;
            float M = 12f;        // margin
            float IW = W - M * 2; // inner width
            float y = 0f;

            // ============ HEADER ============
            GUI.DrawTexture(new Rect(0, 0, W, 52), texHeader);
            // Accent line under header
            GUI.DrawTexture(new Rect(0, 52, W, 2), texAccentLine);

            // Title
            GUI.Label(new Rect(0, 8, W, 24), "SCHEDULE I TRAINER", sTitle);
            GUI.Label(new Rect(0, 30, W, 16), "v1.0  \u2022  F1 Menu  \u2022  F2 ESP", sSubtitle);

            y = 62f;

            // ============ MONEY SECTION ============
            DrawSectionBg(M - 4, y, IW + 8, 118);
            y += 6f;

            // Section icon + header
            GUI.Label(new Rect(M + 4, y, IW, 16), "\u25B8  MONEY", sSectionHead);
            y += 20f;

            // Thin separator
            HLine(M + 4, y, IW - 8, texBorder);
            y += 6f;

            // Cash display
            float cash = MoneyManager.GetCash();
            float online = MoneyManager.GetOnline();
            string cashStr = cash >= 0 ? $"${cash:N0}" : "N/A";
            string onlineStr = online >= 0 ? $"${online:N0}" : "N/A";

            GUI.Label(new Rect(M + 8, y, 80, 20), "Cash", sLabel);
            GUI.Label(new Rect(M + 80, y, IW - 88, 20), cashStr, sValue);
            y += 22f;

            GUI.Label(new Rect(M + 8, y, 80, 20), "Online", sLabel);
            sValue.normal.textColor = colAccent;
            GUI.Label(new Rect(M + 80, y, IW - 88, 20), onlineStr, sValue);
            sValue.normal.textColor = colGreen;
            y += 26f;

            // Money buttons row
            float gap = 6f;
            float bw = (IW - gap * 3 - 8) / 4f;
            float bh = 26f;
            float bx = M + 4;

            if (GUI.Button(new Rect(bx, y, bw, bh), "+$1K", sMoneyBtn))
                SetStatus(MoneyManager.AddCash(1000f));
            bx += bw + gap;
            if (GUI.Button(new Rect(bx, y, bw, bh), "+$10K", sMoneyBtn))
                SetStatus(MoneyManager.AddCash(10000f));
            bx += bw + gap;
            if (GUI.Button(new Rect(bx, y, bw, bh), "+$100K", sMoneyBtn))
                SetStatus(MoneyManager.AddCash(100000f));
            bx += bw + gap;
            if (GUI.Button(new Rect(bx, y, bw, bh), "+$1M", sMoneyBtn))
                SetStatus(MoneyManager.AddCash(1000000f));

            y += bh + 14f;

            // ============ CHEATS SECTION ============
            DrawSectionBg(M - 4, y, IW + 8, 148);
            y += 6f;

            GUI.Label(new Rect(M + 4, y, IW, 16), "\u25B8  CHEATS", sSectionHead);
            y += 20f;
            HLine(M + 4, y, IW - 8, texBorder);
            y += 8f;

            float hw = (IW - gap - 8) / 2f;
            float th = 28f;

            // Row 1: God Mode | Inf. Energy
            Schedule1Trainer.GodMode = Toggle(
                new Rect(M + 4, y, hw, th), "God Mode", Schedule1Trainer.GodMode);
            Schedule1Trainer.InfiniteEnergy = Toggle(
                new Rect(M + 4 + hw + gap, y, hw, th), "Inf. Energy", Schedule1Trainer.InfiniteEnergy);
            y += th + 5f;

            // Row 2: Speed x3 | Police Ignore
            bool newSpeed = Toggle(
                new Rect(M + 4, y, hw, th), "Speed x3", Schedule1Trainer.SpeedHack);
            if (newSpeed != Schedule1Trainer.SpeedHack)
            {
                Schedule1Trainer.SpeedHack = newSpeed;
                if (!newSpeed) Cheats.ResetSpeed();
            }

            bool newIgnore = Toggle(
                new Rect(M + 4 + hw + gap, y, hw, th), "Police Ignore", Schedule1Trainer.PoliceIgnore);
            if (newIgnore != Schedule1Trainer.PoliceIgnore)
            {
                Schedule1Trainer.PoliceIgnore = newIgnore;
                if (!newIgnore) Cheats.DisablePoliceIgnore();
            }
            y += th + 5f;

            // Row 3: Police ESP | Clear Wanted
            Schedule1Trainer.EspEnabled = Toggle(
                new Rect(M + 4, y, hw, th), "Police ESP", Schedule1Trainer.EspEnabled);

            if (GUI.Button(new Rect(M + 4 + hw + gap, y, hw, th), "\u26A0 Clear Wanted", sActionBtn))
            {
                Cheats.ClearWantedLevel();
                SetStatus("Wanted level cleared!");
            }
            y += th + 14f;

            // ============ INFO BAR ============
            DrawSectionBg(M - 4, y, IW + 8, 42);
            y += 4f;

            if (Schedule1Trainer.EspEnabled)
            {
                string espInfo = PoliceESP.GetStatus();
                sLabel.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(M, y, IW, 16), espInfo, sLabel);
                sLabel.alignment = TextAnchor.MiddleLeft;
                y += 18f;
            }
            else
            {
                y += 2f;
            }

            // Hotkeys
            GUI.Label(new Rect(M, y, IW, 18), "[F1] Menu    [F2] ESP    [ESC] Close", sHotkey);
            y += 26f;

            // ============ STATUS MESSAGE ============
            if (!string.IsNullOrEmpty(statusMsg) && Time.time - statusTimer < 2.5f)
            {
                float alpha = Mathf.Clamp01(2.5f - (Time.time - statusTimer));
                sStatus.normal.textColor = new Color(colGreen.r, colGreen.g, colGreen.b, alpha);
                GUI.Label(new Rect(M, y, IW, 18), statusMsg, sStatus);
                sStatus.normal.textColor = colGreen;
            }

            // ============ BOTTOM ACCENT LINE ============
            float totalH = y + 24f;
            GUI.DrawTexture(new Rect(0, totalH - 2, W, 2), texAccentLine);

            // Resize window to content
            windowRect.height = totalH;

            // Draggable from header area
            GUI.DragWindow(new Rect(0, 0, W, 54));
        }

        private static void DrawSectionBg(float x, float y, float w, float h)
        {
            // Border
            GUI.DrawTexture(new Rect(x, y, w, 1), texBorder);           // top
            GUI.DrawTexture(new Rect(x, y + h, w, 1), texBorder);       // bottom
            GUI.DrawTexture(new Rect(x, y, 1, h), texBorder);           // left
            GUI.DrawTexture(new Rect(x + w - 1, y, 1, h), texBorder);   // right
            // Fill
            GUI.DrawTexture(new Rect(x + 1, y + 1, w - 2, h - 2), texSection);
        }
    }
}
