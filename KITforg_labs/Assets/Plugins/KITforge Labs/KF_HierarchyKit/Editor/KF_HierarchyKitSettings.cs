using System;
using UnityEditor;
using UnityEngine;

namespace KITforgeLabs.Editor.HierarchyKit
{
    [Serializable]
    class KF_HierarchyKitConfig
    {
        public bool enabled = true;
        public bool showTypeIcons = true;
    }

    static class KF_HierarchyKitSettings
    {
        private const string k_PrefsKey = "KITforgeLabs.HierarchyKit.Config";

        private static KF_HierarchyKitConfig _current;
        public static KF_HierarchyKitConfig Current => _current ??= Load();

        static KF_HierarchyKitConfig Load()
        {
            var json = EditorPrefs.GetString(k_PrefsKey, string.Empty);
            if (string.IsNullOrEmpty(json))
                return new KF_HierarchyKitConfig();

            return JsonUtility.FromJson<KF_HierarchyKitConfig>(json) ?? new KF_HierarchyKitConfig();
        }

        public static void Save()
        {
            EditorPrefs.SetString(k_PrefsKey, JsonUtility.ToJson(_current));
        }

        public static void Invalidate()
        {
            _current = null;
        }
    }
}
