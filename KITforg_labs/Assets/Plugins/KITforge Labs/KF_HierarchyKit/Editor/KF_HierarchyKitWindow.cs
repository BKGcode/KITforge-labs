using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace KITforgeLabs.Editor.HierarchyKit
{
    class KF_HierarchyKitWindow : EditorWindow
    {
        public static void Open()
        {
            var window = GetWindow<KF_HierarchyKitWindow>();
            window.titleContent = new GUIContent("HierarchyKit");
            window.minSize = new Vector2(280f, 80f);
            window.maxSize = new Vector2(480f, 80f);
            window.Show();
        }

        private void CreateGUI()
        {
            var config = KF_HierarchyKitSettings.Current;

            var root = rootVisualElement;
            root.style.paddingTop = 10;
            root.style.paddingLeft = 12;
            root.style.paddingRight = 12;
            root.style.paddingBottom = 10;

            var enabled = new Toggle("Enabled") { value = config.enabled };
            enabled.RegisterValueChangedCallback(evt =>
            {
                KF_HierarchyKitSettings.Current.enabled = evt.newValue;
                KF_HierarchyKitSettings.Save();
                EditorApplication.RepaintHierarchyWindow();
            });
            root.Add(enabled);

            var icons = new Toggle("Show Type Icons") { value = config.showTypeIcons };
            icons.style.marginTop = 4;
            icons.RegisterValueChangedCallback(evt =>
            {
                KF_HierarchyKitSettings.Current.showTypeIcons = evt.newValue;
                KF_HierarchyKitSettings.Save();
                EditorApplication.RepaintHierarchyWindow();
            });
            root.Add(icons);
        }
    }
}
