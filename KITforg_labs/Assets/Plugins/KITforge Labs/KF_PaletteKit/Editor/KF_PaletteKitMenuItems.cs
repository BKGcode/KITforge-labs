using UnityEditor;

namespace KITforgeLabs.Editor.PaletteKit
{
    internal static class KF_PaletteKitMenuItems
    {
        [MenuItem("KITforge/PaletteKit/Open")]
        private static void OpenWindow()
        {
            KF_PaletteKitWindow.Open();
        }
    }
}
