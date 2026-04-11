using UnityEditor;

namespace KITforgeLabs.Editor.HierarchyKit
{
    [InitializeOnLoad]
    static class KF_HierarchyKitInitializer
    {
        static KF_HierarchyKitInitializer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += KF_HierarchyKitRenderer.OnHierarchyItemGUI;
            EditorApplication.hierarchyChanged += KF_HierarchyKitRenderer.ClearNameCache;
        }
    }
}
