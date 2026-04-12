using System;
using UnityEngine;

namespace KITforgeLabs.Editor.PaletteKit
{
    [Serializable]
    public class KF_PaletteKitMaterialBinding
    {
        public string RoleId;
        public Material Material;
        public string PropertyName;

        public KF_PaletteKitMaterialBinding(string roleId)
        {
            RoleId = roleId;
            PropertyName = string.Empty;
        }
    }
}
