using System;
using UnityEngine;

namespace KITforgeLabs.Editor.PaletteKit
{
    [Serializable]
    public class KF_PaletteKitColorRole
    {
        [SerializeField] private string _roleGuid;
        public string RoleName;
        public Color RoleColor;

        public string RoleGuid => _roleGuid;

        public KF_PaletteKitColorRole()
        {
            _roleGuid = System.Guid.NewGuid().ToString();
            RoleName = "New Role";
            RoleColor = Color.white;
        }

        public void AssignGuid(string guid)
        {
            _roleGuid = guid;
        }
    }
}
