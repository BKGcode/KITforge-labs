using System.Collections.Generic;
using UnityEngine;

namespace KITforgeLabs.Editor.PaletteKit
{
    [CreateAssetMenu(
        menuName = "KITforge Labs/PaletteKit Binding",
        fileName = "KF_PaletteKit_Binding")]
    public class KF_PaletteKitBindingSO : ScriptableObject
    {
        [SerializeField]
        private List<KF_PaletteKitMaterialBinding> _bindings = new List<KF_PaletteKitMaterialBinding>();

        public List<KF_PaletteKitMaterialBinding> Bindings => _bindings;
    }
}
