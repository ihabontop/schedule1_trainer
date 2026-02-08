using UnityEngine;
using Il2CppScheduleOne.Police;

namespace Schedule1Mod
{
    public static class PoliceESP
    {
        private static Texture2D texLine;
        private static Texture2D texFill;
        private static Texture2D texLabelBg;
        private static GUIStyle labelStyle;
        private static int officerCount = 0;
        private static bool init = false;

        // Colors matching the menu theme
        private static readonly Color colCyan    = new Color(0f, 0.831f, 1f, 0.9f);
        private static readonly Color colCyanDim = new Color(0f, 0.831f, 1f, 0.15f);
        private static readonly Color colLabelBg = new Color(0.05f, 0.07f, 0.09f, 0.75f);

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
            texLine = Tex(colCyan);
            texFill = Tex(colCyanDim);
            texLabelBg = Tex(colLabelBg);

            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 11;
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.alignment = TextAnchor.MiddleCenter;
            labelStyle.normal.textColor = colCyan;

            init = true;
        }

        public static string GetStatus()
        {
            return $"\u25B8 Tracking {officerCount} officer{(officerCount != 1 ? "s" : "")}";
        }

        public static void Draw()
        {
            Init();

            Camera cam = Camera.main;
            if (cam == null) return;

            try
            {
                var officers = PoliceOfficer.Officers;
                if (officers == null) { officerCount = 0; return; }

                officerCount = officers.Count;
                for (int i = 0; i < officers.Count; i++)
                {
                    var officer = officers[i];
                    if (officer == null) continue;
                    if (officer.transform == null) continue;
                    DrawOfficerESP(cam, officer.transform, officer.name ?? "Officer");
                }
            }
            catch (System.Exception ex)
            {
                GUI.Label(new Rect(10, Screen.height - 30, 500, 25),
                    $"ESP Error: {ex.Message}");
            }
        }

        private static void DrawOfficerESP(Camera cam, Transform transform, string label)
        {
            Vector3 worldPos = transform.position;
            Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
            if (screenPos.z <= 0) return;

            float screenY = Screen.height - screenPos.y;
            float distance = Vector3.Distance(cam.transform.position, worldPos);

            // Adaptive box size
            float boxH = Mathf.Clamp(900f / distance, 18f, 220f);
            float boxW = boxH * 0.38f;
            float left = screenPos.x - boxW / 2f;
            float top = screenY - boxH * 0.65f;

            // Distance-based color: close = bright, far = dimmer
            float intensity = Mathf.Clamp01(1f - (distance - 10f) / 150f);
            Color lineCol = new Color(colCyan.r, colCyan.g, colCyan.b, 0.4f + intensity * 0.5f);
            Color fillCol = new Color(colCyan.r, colCyan.g, colCyan.b, 0.03f + intensity * 0.1f);

            // Draw filled box
            GUI.color = fillCol;
            GUI.DrawTexture(new Rect(left + 1, top + 1, boxW - 2, boxH - 2), texFill);
            GUI.color = Color.white;

            // Draw border with corner accents
            float t = 1.5f;
            GUI.color = lineCol;

            // Full border
            GUI.DrawTexture(new Rect(left, top, boxW, t), texLine);             // top
            GUI.DrawTexture(new Rect(left, top + boxH, boxW, t), texLine);       // bottom
            GUI.DrawTexture(new Rect(left, top, t, boxH), texLine);             // left
            GUI.DrawTexture(new Rect(left + boxW - t, top, t, boxH + t), texLine); // right

            // Corner accents (thicker, short lines at corners)
            float cornerLen = Mathf.Max(6f, boxW * 0.25f);
            float ct = 3f;
            // Top-left
            GUI.DrawTexture(new Rect(left - 1, top - 1, cornerLen, ct), texLine);
            GUI.DrawTexture(new Rect(left - 1, top - 1, ct, cornerLen), texLine);
            // Top-right
            GUI.DrawTexture(new Rect(left + boxW - cornerLen + 1, top - 1, cornerLen, ct), texLine);
            GUI.DrawTexture(new Rect(left + boxW - ct + 1, top - 1, ct, cornerLen), texLine);
            // Bottom-left
            GUI.DrawTexture(new Rect(left - 1, top + boxH - ct + 1, cornerLen, ct), texLine);
            GUI.DrawTexture(new Rect(left - 1, top + boxH - cornerLen + 1, ct, cornerLen), texLine);
            // Bottom-right
            GUI.DrawTexture(new Rect(left + boxW - cornerLen + 1, top + boxH - ct + 1, cornerLen, ct), texLine);
            GUI.DrawTexture(new Rect(left + boxW - ct + 1, top + boxH - cornerLen + 1, ct, cornerLen), texLine);

            GUI.color = Color.white;

            // Label with background
            string text = $"POLICE  {distance:F0}m";
            float labelW = text.Length * 7.5f + 16f;
            float labelH = 18f;
            float labelX = screenPos.x - labelW / 2f;
            float labelY = top - labelH - 4f;

            GUI.DrawTexture(new Rect(labelX, labelY, labelW, labelH), texLabelBg);
            // Small accent line on top of label
            GUI.DrawTexture(new Rect(labelX, labelY, labelW, 1f), texLine);

            labelStyle.normal.textColor = new Color(colCyan.r, colCyan.g, colCyan.b, 0.6f + intensity * 0.4f);
            GUI.Label(new Rect(labelX, labelY, labelW, labelH), text, labelStyle);
        }
    }
}
