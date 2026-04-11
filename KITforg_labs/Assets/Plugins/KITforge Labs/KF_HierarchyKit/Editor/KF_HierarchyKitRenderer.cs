using UnityEditor;
using UnityEngine;

namespace KITforgeLabs.Editor.HierarchyKit
{
    static class KF_HierarchyKitRenderer
    {
        private static readonly Color k_HeaderBg = new Color(0.18f, 0.26f, 0.44f, 0.92f);
        private static readonly char[] k_TrimChars = { '-', '=', '[', ']', ' ' };
        private const float k_BorderWidth = 3f;

        private static GUIStyle _headerStyle;

        private static GUIStyle HeaderStyle
        {
            get
            {
                if (_headerStyle != null)
                    return _headerStyle;

                _headerStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    alignment = TextAnchor.MiddleCenter
                };
                _headerStyle.normal.textColor = Color.white;
                return _headerStyle;
            }
        }

        public static void OnHierarchyItemGUI(int instanceId, Rect rowRect)
        {
            var config = KF_HierarchyKitSettings.Current;
            if (!config.enabled)
                return;

            var go = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (go == null)
                return;

            var name = go.name;
            if (IsHeader(name))
                DrawHeaderRow(rowRect, name);
            else
                DrawBorderRow(rowRect, config.defaultBorderColor);
        }

        static bool IsHeader(string name)
        {
            return name.StartsWith("---") || name.StartsWith("===") ||
                   (name.StartsWith("[") && name.EndsWith("]"));
        }

        static void DrawHeaderRow(Rect rowRect, string name)
        {
            var fullRect = new Rect(0f, rowRect.y, Screen.width, rowRect.height);
            EditorGUI.DrawRect(fullRect, k_HeaderBg);
            GUI.Label(fullRect, name.Trim(k_TrimChars), HeaderStyle);
        }

        static void DrawBorderRow(Rect rowRect, Color color)
        {
            var borderRect = new Rect(rowRect.x, rowRect.y, k_BorderWidth, rowRect.height);
            EditorGUI.DrawRect(borderRect, color);
        }
    }
}
