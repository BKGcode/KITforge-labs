using UnityEditor;
using UnityEngine;

namespace KITforgeLabs.Editor.HierarchyKit
{
    static class KF_HierarchyKitMenuItems
    {
        private const string k_TogglePath = "KITforge/HierarchyKit/Toggle Enable";

        [MenuItem("KITforge/HierarchyKit/Settings...", false, 10)]
        static void OpenSettings()
        {
            KF_HierarchyKitWindow.Open();
        }

        [MenuItem(k_TogglePath, false, 20)]
        static void ToggleEnabled()
        {
            var config = KF_HierarchyKitSettings.Current;
            config.enabled = !config.enabled;
            KF_HierarchyKitSettings.Save();
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem(k_TogglePath, true)]
        static bool ToggleEnabledValidate()
        {
            Menu.SetChecked(k_TogglePath, KF_HierarchyKitSettings.Current.enabled);
            return true;
        }

        [MenuItem("GameObject/KITforge/Add Header Above", false, 49)]
        static void AddHeaderAbove()
        {
            var selected = Selection.activeGameObject;
            var parent = selected.transform.parent;

            var header = new GameObject("--- New Header ---");
            Undo.RegisterCreatedObjectUndo(header, "Add Header Above");

            if (parent != null)
                header.transform.SetParent(parent, false);

            header.transform.SetSiblingIndex(selected.transform.GetSiblingIndex());
            Selection.activeGameObject = header;
            EditorApplication.RepaintHierarchyWindow();
        }

        [MenuItem("GameObject/KITforge/Add Header Above", true)]
        static bool AddHeaderAboveValidate()
        {
            return Selection.activeGameObject != null;
        }
    }
}
