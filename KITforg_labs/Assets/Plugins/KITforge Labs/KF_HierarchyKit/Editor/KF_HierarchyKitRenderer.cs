using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KITforgeLabs.Editor.HierarchyKit
{
    static class KF_HierarchyKitRenderer
    {
        private static readonly Color k_HeaderBg = new Color(0.18f, 0.26f, 0.44f, 0.92f);
        private static readonly Color k_Accent = new Color(254f / 255f, 193f / 255f, 0f, 1f);
        private static readonly char[] k_TrimChars = { '-', '=', '[', ']', ' ' };
        private const float k_ChipWidth = 4f;
        private const float k_ChipInset = 3f;

        private static readonly Dictionary<int, string> s_NameCache = new Dictionary<int, string>();

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

        public static void ClearNameCache() => s_NameCache.Clear();

        public static void OnHierarchyItemGUI(int instanceId, Rect rowRect)
        {
            var config = KF_HierarchyKitSettings.Current;
            if (!config.enabled)
                return;

            if (!s_NameCache.TryGetValue(instanceId, out var name))
            {
                var go = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
                if (go == null)
                    return;
                name = go.name;
                s_NameCache[instanceId] = name;
            }

            if (IsHeader(name))
                DrawHeaderRow(rowRect, name);
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

            var chipRect = new Rect(
                fullRect.xMax - k_ChipWidth,
                fullRect.y + k_ChipInset,
                k_ChipWidth,
                fullRect.height - k_ChipInset * 2f);
            EditorGUI.DrawRect(chipRect, k_Accent);

            GUI.Label(fullRect, name.Trim(k_TrimChars), HeaderStyle);
        }
    }
}
