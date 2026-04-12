using System.Collections.Generic;
using UnityEngine;

namespace KITforgeLabs.Editor.PaletteKit
{
    [CreateAssetMenu(
        menuName = "KITforge Labs/PaletteKit Palette",
        fileName = "KF_PaletteKit_Default")]
    public class KF_PaletteKitSO : ScriptableObject
    {
        [SerializeField]
        private List<KF_PaletteKitColorRole> _roles = new List<KF_PaletteKitColorRole>();

        public List<KF_PaletteKitColorRole> Roles => _roles;
    }
}
