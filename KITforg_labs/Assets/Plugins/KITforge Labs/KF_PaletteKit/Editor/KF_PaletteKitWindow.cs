using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace KITforgeLabs.Editor.PaletteKit
{
    internal sealed class KF_PaletteKitWindow : EditorWindow
    {
        private const string k_UxmlPath =
            "Assets/Plugins/KITforge Labs/KF_PaletteKit/Editor/UXML/KF_PaletteKitWindow.uxml";

        private const string k_DesignTokensPath =
            "Assets/Plugins/KITforge Labs/KF_PaletteKit/Editor/USS/KF_DesignTokens.uss";

        private const string k_WindowUssPath =
            "Assets/Plugins/KITforge Labs/KF_PaletteKit/Editor/USS/KF_Window.uss";

        private const string k_ComponentsUssPath =
            "Assets/Plugins/KITforge Labs/KF_PaletteKit/Editor/USS/KF_Components.uss";

        private const string k_SpecificUssPath =
            "Assets/Plugins/KITforge Labs/KF_PaletteKit/Editor/USS/KF_PaletteKitSpecific.uss";

        private const string k_NewPalettePath = "Assets/Plugins/KITforge Labs/KF_PaletteKit/Settings/KF_PaletteKit_Default.asset";
        private const string k_NewBindingPath = "Assets/Plugins/KITforge Labs/KF_PaletteKit/Settings/KF_PaletteKit_Binding.asset";
        private const int k_SwatchSize = 64;

        private static readonly string[] k_PreferredColorProps =
            { "_BaseColor", "_Color", "_MainColor", "_TintColor", "_Tint" };

        [SerializeField] private KF_PaletteKitSO _palette;
        [SerializeField] private KF_PaletteKitBindingSO _binding;
        [SerializeField] private Scope _scope = Scope.Scene;

        private SerializedObject _serializedPalette;
        private readonly Dictionary<Renderer, MaterialPropertyBlock> _previewBlocks = new();
        private ObjectField _paletteField;
        private ObjectField _bindingField;
        private VisualElement _emptyPanel;
        private VisualElement _loadedPanel;
        private VisualElement _rolesContainer;
        private VisualElement _playModeBanner;
        private Label _statusMessage;
        private VisualElement _statusDot;
        private Button _scopeBtnScene;
        private Button _scopeBtnProject;
        private Button _scopeBtnSelection;

        private enum StatusType { Success, Warning, Error }
        private enum Scope { Scene, Project, Selection }

        public static void Open()
        {
            var window = GetWindow<KF_PaletteKitWindow>();
            window.titleContent = new GUIContent("PaletteKit");
            window.minSize = new Vector2(400f, 350f);
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            RevertAll();
        }

        private void CreateGUI()
        {
            var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(k_UxmlPath);
            if (uxml == null)
            {
                Debug.LogError($"[KF_PaletteKit] UXML not found at: {k_UxmlPath}");
                return;
            }

            AddStyleSheet(k_DesignTokensPath);
            AddStyleSheet(k_WindowUssPath);
            AddStyleSheet(k_ComponentsUssPath);
            AddStyleSheet(k_SpecificUssPath);
            uxml.CloneTree(rootVisualElement);
            SetupHeaderIcon();
            QueryRefs();
            SetScope(_scope);
            WireUpToolbar();
            _paletteField.SetValueWithoutNotify(_palette);
            _bindingField.SetValueWithoutNotify(_binding);
            RefreshPlayModeBanner(EditorApplication.isPlayingOrWillChangePlaymode);
            RefreshView();
        }

        private void QueryRefs()
        {
            _paletteField = rootVisualElement.Q<ObjectField>("kfpk-palette-field");
            _paletteField.objectType = typeof(KF_PaletteKitSO);
            _bindingField = rootVisualElement.Q<ObjectField>("kfpk-binding-field");
            _bindingField.objectType = typeof(KF_PaletteKitBindingSO);
            _emptyPanel = rootVisualElement.Q<VisualElement>("kfpk-empty-panel");
            _loadedPanel = rootVisualElement.Q<VisualElement>("kfpk-loaded-panel");
            _rolesContainer = rootVisualElement.Q<VisualElement>("kfpk-roles-container");
            _statusMessage = rootVisualElement.Q<Label>("kf-statusbar-message");
            _statusDot = rootVisualElement.Q<VisualElement>("kf-statusbar-dot");
            _playModeBanner = rootVisualElement.Q<VisualElement>("kf-playmode-banner");
            _scopeBtnScene = rootVisualElement.Q<Button>("kfpk-scope-btn-scene");
            _scopeBtnProject = rootVisualElement.Q<Button>("kfpk-scope-btn-project");
            _scopeBtnSelection = rootVisualElement.Q<Button>("kfpk-scope-btn-selection");
        }

        private void WireUpToolbar()
        {
            _paletteField.RegisterValueChangedCallback(OnPaletteFieldChanged);
            _bindingField.RegisterValueChangedCallback(OnBindingFieldChanged);
            rootVisualElement.Q<Button>("kfpk-btn-new-palette")
                .RegisterCallback<ClickEvent>(_ => CreateNewPalette());
            rootVisualElement.Q<Button>("kfpk-btn-new-binding")
                .RegisterCallback<ClickEvent>(_ => CreateNewBinding());
            rootVisualElement.Q<Button>("kfpk-btn-add-role")
                .RegisterCallback<ClickEvent>(_ => AddRole());
            rootVisualElement.Q<Button>("kfpk-btn-import-lospec")
                .RegisterCallback<ClickEvent>(_ => ImportLospec());
            rootVisualElement.Q<Button>("kfpk-btn-export")
                .RegisterCallback<ClickEvent>(_ => ExportPalette());
            rootVisualElement.Q<Button>("kfpk-btn-preview")
                .RegisterCallback<ClickEvent>(_ => PreviewAll());
            rootVisualElement.Q<Button>("kfpk-btn-revert")
                .RegisterCallback<ClickEvent>(_ => RevertAll());
            rootVisualElement.Q<Button>("kfpk-btn-apply")
                .RegisterCallback<ClickEvent>(_ => ApplyAll());
            _scopeBtnScene.RegisterCallback<ClickEvent>(_ => SetScope(Scope.Scene));
            _scopeBtnProject.RegisterCallback<ClickEvent>(_ => SetScope(Scope.Project));
            _scopeBtnSelection.RegisterCallback<ClickEvent>(_ => SetScope(Scope.Selection));
        }

        private void AddStyleSheet(string path)
        {
            var sheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
            if (sheet != null)
                rootVisualElement.styleSheets.Add(sheet);
            else
                Debug.LogWarning($"[KF_PaletteKit] StyleSheet not found: {path}");
        }

        private void SetupHeaderIcon()
        {
            var iconPath = "Assets/Plugins/KITforge Labs/KF_PaletteKit/Editor/Icons/KF_BulbIcon.png";
            var headerIcon = rootVisualElement.Q<VisualElement>("kf-header-icon");
            if (headerIcon == null) return;

            var bulbIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);
            if (bulbIcon != null)
            {
                headerIcon.style.backgroundImage = new StyleBackground(bulbIcon);
            }

        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            bool entering = state == PlayModeStateChange.EnteredPlayMode
                         || state == PlayModeStateChange.ExitingEditMode;
            RefreshPlayModeBanner(entering);
        }

        private void RefreshPlayModeBanner(bool show)
        {
            if (_playModeBanner == null) return;
            _playModeBanner.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private void OnPaletteFieldChanged(ChangeEvent<UnityEngine.Object> evt)
        {
            RevertAll();
            _palette = evt.newValue as KF_PaletteKitSO;
            RefreshView();
        }

        private void OnBindingFieldChanged(ChangeEvent<UnityEngine.Object> evt)
        {
            _binding = evt.newValue as KF_PaletteKitBindingSO;
            RebuildRolesList();
        }

        private void CreateNewPalette()
        {
            EnsureSettingsFolder();
            var uniquePath = AssetDatabase.GenerateUniqueAssetPath(k_NewPalettePath);
            var so = CreateInstance<KF_PaletteKitSO>();
            AssetDatabase.CreateAsset(so, uniquePath);
            AssetDatabase.SaveAssets();
            _palette = so;
            _paletteField.SetValueWithoutNotify(so);
            RefreshView();
            SetStatus("Palette created.", StatusType.Success);
        }

        private void CreateNewBinding()
        {
            EnsureSettingsFolder();
            var uniquePath = AssetDatabase.GenerateUniqueAssetPath(k_NewBindingPath);
            var so = CreateInstance<KF_PaletteKitBindingSO>();
            AssetDatabase.CreateAsset(so, uniquePath);
            AssetDatabase.SaveAssets();
            _binding = so;
            _bindingField.SetValueWithoutNotify(so);
            RebuildRolesList();
            SetStatus("Binding created.", StatusType.Success);
        }

        private static void EnsureSettingsFolder()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Plugins"))
                AssetDatabase.CreateFolder("Assets", "Plugins");
            if (!AssetDatabase.IsValidFolder("Assets/Plugins/KITforge Labs"))
                AssetDatabase.CreateFolder("Assets/Plugins", "KITforge Labs");
            if (!AssetDatabase.IsValidFolder("Assets/Plugins/KITforge Labs/KF_PaletteKit"))
                AssetDatabase.CreateFolder("Assets/Plugins/KITforge Labs", "KF_PaletteKit");
            if (!AssetDatabase.IsValidFolder("Assets/Plugins/KITforge Labs/KF_PaletteKit/Settings"))
                AssetDatabase.CreateFolder("Assets/Plugins/KITforge Labs/KF_PaletteKit", "Settings");
        }

        private void RefreshView()
        {
            bool hasPalette = _palette != null;
            _emptyPanel.style.display = hasPalette ? DisplayStyle.None : DisplayStyle.Flex;
            _loadedPanel.style.display = hasPalette ? DisplayStyle.Flex : DisplayStyle.None;
            if (hasPalette)
            {
                MigrateRoleGuids();
                RebuildRolesList();
            }
        }

        private void MigrateRoleGuids()
        {
            bool dirty = false;
            foreach (var role in _palette.Roles)
            {
                if (!string.IsNullOrEmpty(role.RoleGuid)) continue;
                Undo.RecordObject(_palette, "Migrate Role GUIDs");
                role.AssignGuid(System.Guid.NewGuid().ToString());
                dirty = true;
            }
            if (dirty) EditorUtility.SetDirty(_palette);
        }

        private void RebuildRolesList()
        {
            _rolesContainer.Clear();
            if (_palette == null) return;
            _serializedPalette = new SerializedObject(_palette);
            var rolesProp = _serializedPalette.FindProperty("_roles");
            for (int i = 0; i < rolesProp.arraySize; i++)
            {
                var roleProp = rolesProp.GetArrayElementAtIndex(i);
                var roleGuid = _palette.Roles[i].RoleGuid;
                _rolesContainer.Add(BuildRoleRow(i, roleGuid, roleProp));
            }
        }

        private VisualElement BuildRoleRow(int index, string roleId, SerializedProperty roleProp)
        {
            var row = new VisualElement();
            row.AddToClassList("kfpk-role-row");
            row.Add(BuildSwatchPanel(roleProp));
            row.Add(BuildMaterialsPanel(roleId, index));
            return row;
        }

        private VisualElement BuildSwatchPanel(SerializedProperty roleProp)
        {
            var panel = new VisualElement();
            panel.AddToClassList("kfpk-role-swatch-panel");

            var colorField = new ColorField(string.Empty) { showAlpha = false, showEyeDropper = false };
            colorField.AddToClassList("kfpk-role-color-field");
            colorField.BindProperty(roleProp.FindPropertyRelative("RoleColor"));

            var nameField = new TextField(string.Empty);
            nameField.AddToClassList("kfpk-role-name-field");
            nameField.BindProperty(roleProp.FindPropertyRelative("RoleName"));

            panel.Add(colorField);
            panel.Add(nameField);
            return panel;
        }

        private VisualElement BuildMaterialsPanel(string roleId, int roleIndex)
        {
            var panel = new VisualElement();
            panel.AddToClassList("kfpk-role-materials-panel");
            panel.Add(_binding != null ? BuildBindingList(roleId) : BuildNoBindingHint());
            panel.Add(BuildDeleteRoleButton(roleIndex));
            return panel;
        }

        private VisualElement BuildBindingList(string roleId)
        {
            var container = new VisualElement();
            var bindings = _binding.Bindings.FindAll(b => b.RoleId == roleId);
            foreach (var b in bindings)
                container.Add(BuildBindingEntry(b, _binding.Bindings.IndexOf(b)));
            container.Add(BuildAddMaterialField(roleId));
            return container;
        }

        private static VisualElement BuildNoBindingHint()
        {
            var hint = new Label("Assign a Binding SO to manage materials");
            hint.AddToClassList("kfpk-no-binding-hint");
            return hint;
        }

        private VisualElement BuildBindingEntry(KF_PaletteKitMaterialBinding entry, int index)
        {
            var row = new VisualElement();
            row.AddToClassList("kfpk-binding-entry");
            row.Add(BuildValidityDot(entry));
            row.Add(BuildMaterialLabel(entry));
            row.Add(BuildPropertyField(entry, index));
            row.Add(BuildRedetectButton(entry, index));
            row.Add(BuildRemoveBindingButton(index));
            return row;
        }

        private static VisualElement BuildValidityDot(KF_PaletteKitMaterialBinding entry)
        {
            var dot = new VisualElement();
            dot.AddToClassList("kfpk-binding-dot");
            bool valid = entry.Material != null
                      && !string.IsNullOrEmpty(entry.PropertyName)
                      && entry.Material.HasProperty(entry.PropertyName);
            dot.AddToClassList(valid ? "kfpk-binding-dot--valid" : "kfpk-binding-dot--invalid");
            return dot;
        }

        private static Label BuildMaterialLabel(KF_PaletteKitMaterialBinding entry)
        {
            bool hasMat = entry.Material != null;
            var label = new Label(hasMat ? entry.Material.name : "Missing");
            label.AddToClassList(hasMat ? "kfpk-material-name" : "kfpk-material-name--missing");
            return label;
        }

        private VisualElement BuildPropertyField(KF_PaletteKitMaterialBinding entry, int bindingIndex)
        {
            var field = new TextField();
            field.AddToClassList("kfpk-binding-property");
            field.SetValueWithoutNotify(entry.PropertyName);
            field.RegisterCallback<FocusOutEvent>(_ =>
            {
                if (field.value == _binding.Bindings[bindingIndex].PropertyName) return;
                Undo.RecordObject(_binding, "Change Property Name");
                _binding.Bindings[bindingIndex].PropertyName = field.value;
                EditorUtility.SetDirty(_binding);
                RebuildRolesList();
            });
            return field;
        }

        private Button BuildRedetectButton(KF_PaletteKitMaterialBinding entry, int index)
        {
            var btn = new Button { text = "⟳" };
            btn.AddToClassList("kfpk-btn-redetect");
            btn.RegisterCallback<ClickEvent>(_ => RedetectProperty(entry, index));
            return btn;
        }

        private Button BuildRemoveBindingButton(int index)
        {
            var btn = new Button { text = "✕" };
            btn.AddToClassList("kfpk-btn-remove-material");
            btn.RegisterCallback<ClickEvent>(_ => RemoveMaterialBinding(index));
            return btn;
        }

        private VisualElement BuildAddMaterialField(string roleId)
        {
            var field = new ObjectField(string.Empty) { objectType = typeof(Material) };
            field.AddToClassList("kfpk-add-material-field");
            field.RegisterValueChangedCallback(evt =>
            {
                if (evt.newValue is Material mat)
                {
                    AddMaterialToRole(roleId, mat);
                    field.SetValueWithoutNotify(null);
                }
            });
            return field;
        }

        private Button BuildDeleteRoleButton(int roleIndex)
        {
            var btn = new Button { text = "✕ Remove" };
            btn.AddToClassList("kfpk-btn-delete-role");
            btn.RegisterCallback<ClickEvent>(_ => RemoveRole(roleIndex));
            return btn;
        }

        private void AddRole()
        {
            if (_palette == null) return;
            Undo.RecordObject(_palette, "Add Color Role");
            _palette.Roles.Add(new KF_PaletteKitColorRole());
            EditorUtility.SetDirty(_palette);
            RebuildRolesList();
            SetStatus("Role added.", StatusType.Success);
        }

        private void RemoveRole(int roleIndex)
        {
            var roleGuid = _palette.Roles[roleIndex].RoleGuid;
            Undo.RecordObject(_palette, "Remove Color Role");
            _palette.Roles.RemoveAt(roleIndex);
            EditorUtility.SetDirty(_palette);
            if (_binding != null)
            {
                Undo.RecordObject(_binding, "Remove Role Bindings");
                _binding.Bindings.RemoveAll(b => b.RoleId == roleGuid);
                EditorUtility.SetDirty(_binding);
            }
            RebuildRolesList();
        }

        private static string DetectColorProperty(Material mat)
        {
            int count = mat.shader.GetPropertyCount();
            var colorProps = new List<string>(count);
            for (int i = 0; i < count; i++)
            {
                if (mat.shader.GetPropertyType(i) == ShaderPropertyType.Color)
                    colorProps.Add(mat.shader.GetPropertyName(i));
            }
            if (colorProps.Count == 0) return string.Empty;
            foreach (var preferred in k_PreferredColorProps)
            {
                if (colorProps.Contains(preferred))
                    return preferred;
            }
            return colorProps[0];
        }

        private void AddMaterialToRole(string roleId, Material mat)
        {
            if (_binding == null) return;
            if (_binding.Bindings.Exists(b => b.RoleId == roleId && b.Material == mat)) return;
            var entry = new KF_PaletteKitMaterialBinding(roleId) { Material = mat };
            entry.PropertyName = DetectColorProperty(mat);
            Undo.RecordObject(_binding, "Add Material to Role");
            _binding.Bindings.Add(entry);
            EditorUtility.SetDirty(_binding);
            RebuildRolesList();
            SetStatusForBinding(entry, mat.name);
        }

        private void SetStatusForBinding(KF_PaletteKitMaterialBinding entry, string matName)
        {
            bool detected = !string.IsNullOrEmpty(entry.PropertyName);
            string msg = detected
                ? $"'{matName}' assigned with '{entry.PropertyName}'."
                : $"'{matName}' assigned — no color property found. Enter name manually.";
            SetStatus(msg, detected ? StatusType.Success : StatusType.Warning);
        }

        private void RemoveMaterialBinding(int bindingIndex)
        {
            if (_binding == null) return;
            Undo.RecordObject(_binding, "Remove Material Binding");
            _binding.Bindings.RemoveAt(bindingIndex);
            EditorUtility.SetDirty(_binding);
            RebuildRolesList();
        }

        private void RedetectProperty(KF_PaletteKitMaterialBinding entry, int bindingIndex)
        {
            if (entry.Material == null) return;
            var detected = DetectColorProperty(entry.Material);
            if (string.IsNullOrEmpty(detected))
            {
                SetStatus($"'{entry.Material.name}': no color property found. Enter name manually.", StatusType.Warning);
                return;
            }
            Undo.RecordObject(_binding, "Redetect Property");
            _binding.Bindings[bindingIndex].PropertyName = detected;
            EditorUtility.SetDirty(_binding);
            RebuildRolesList();
            SetStatus($"'{entry.Material.name}': detected '{detected}'.", StatusType.Success);
        }

        private void PreviewAll()
        {
            if (_palette == null) return;
            if (_binding == null) { SetStatus("No Binding SO assigned.", StatusType.Warning); return; }
            RevertAll();
            var renderers = GetRenderersByScope();
            var warnings = new StringBuilder();
            foreach (var b in _binding.Bindings)
                ApplyBindingToPreview(b, renderers, warnings);
            SetStatus(warnings.Length > 0 ? warnings.ToString() : $"Previewing {_previewBlocks.Count} renderer(s).",
                      warnings.Length > 0 ? StatusType.Warning : StatusType.Success);
        }

        private void ApplyBindingToPreview(KF_PaletteKitMaterialBinding b, Renderer[] renderers, StringBuilder warnings)
        {
            var role = _palette.Roles.Find(r => r.RoleGuid == b.RoleId);
            if (role == null || b.Material == null) return;
            if (!b.Material.HasProperty(b.PropertyName))
            {
                warnings.Append($"'{b.Material.name}' missing '{b.PropertyName}'. ");
                return;
            }
            ApplyPreviewToRenderers(renderers, b.Material, b.PropertyName, role.RoleColor);
        }

        private void ApplyPreviewToRenderers(Renderer[] renderers, Material mat, string prop, Color color)
        {
            foreach (var r in renderers)
            {
                if (!UsesMaterial(r, mat)) continue;
                if (!_previewBlocks.TryGetValue(r, out var block))
                {
                    block = new MaterialPropertyBlock();
                    _previewBlocks[r] = block;
                }
                r.GetPropertyBlock(block);
                block.SetColor(prop, color);
                r.SetPropertyBlock(block);
            }
        }

        private static bool UsesMaterial(Renderer r, Material mat)
        {
            foreach (var m in r.sharedMaterials)
            {
                if (m == mat) return true;
            }
            return false;
        }

        private void RevertAll()
        {
            foreach (var r in _previewBlocks.Keys)
            {
                if (r != null)
                    r.SetPropertyBlock(null);
            }
            _previewBlocks.Clear();
            SetStatus(string.Empty, StatusType.Success);
        }

        private void ApplyAll()
        {
            if (_palette == null) return;
            if (_binding == null) { SetStatus("No Binding SO assigned.", StatusType.Warning); return; }
            RevertAll();
            int count = 0;
            var warnings = new StringBuilder();
            AssetDatabase.StartAssetEditing();
            try { ApplyRolesToMaterials(ref count, warnings); }
            finally { AssetDatabase.StopAssetEditing(); }
            AssetDatabase.SaveAssets();
            SetStatus(warnings.Length > 0 ? warnings.ToString() : $"Applied to {count} material(s).",
                      warnings.Length > 0 ? StatusType.Warning : StatusType.Success);
        }

        private void ApplyRolesToMaterials(ref int count, StringBuilder warnings)
        {
            var scopeMats = _scope == Scope.Project ? null : GetMaterialsInScope();
            foreach (var b in _binding.Bindings)
            {
                if (scopeMats != null && !scopeMats.Contains(b.Material)) continue;
                var role = _palette.Roles.Find(r => r.RoleGuid == b.RoleId);
                if (role == null || b.Material == null) continue;
                if (!b.Material.HasProperty(b.PropertyName)) { warnings.Append($"'{b.Material.name}' missing '{b.PropertyName}'. "); continue; }
                Undo.RecordObject(b.Material, "PaletteKit Apply");
                b.Material.SetColor(b.PropertyName, role.RoleColor);
                EditorUtility.SetDirty(b.Material);
                count++;
            }
        }

        private void ImportLospec()
        {
            var path = EditorUtility.OpenFilePanel("Import Lospec HEX Palette", "", "hex");
            if (string.IsNullOrEmpty(path)) return;

            string[] lines;
            try { lines = File.ReadAllLines(path); }
            catch (System.Exception e) { SetStatus($"Failed to read file: {e.Message}", StatusType.Error); return; }

            var parsedColors = ParseLospecColors(lines);
            if (parsedColors.Count == 0)
            {
                SetStatus("No valid colors found in HEX file.", StatusType.Error);
                return;
            }

            bool overwrite = false;
            if (_palette.Roles.Count > 0)
            {
                int choice = EditorUtility.DisplayDialogComplex(
                    "Import Lospec Palette",
                    $"Palette has {_palette.Roles.Count} role(s).\nOverwrite clears all roles and bindings.\nAppend adds {parsedColors.Count} new role(s).",
                    "Overwrite", "Cancel", "Append");
                if (choice == 1) return;
                overwrite = choice == 0;
            }

            var paletteName = Path.GetFileNameWithoutExtension(path);
            ApplyLospecImport(paletteName, overwrite, parsedColors);
        }

        private void ApplyLospecImport(string paletteName, bool overwrite, List<Color> colors)
        {
            Undo.RecordObject(_palette, "Import Lospec");
            if (overwrite)
            {
                if (_binding != null)
                {
                    Undo.RecordObject(_binding, "Import Lospec");
                    _binding.Bindings.Clear();
                    EditorUtility.SetDirty(_binding);
                }
                _palette.Roles.Clear();
            }

            string prefix = string.IsNullOrEmpty(paletteName) ? "Color" : CapitalizeFirst(paletteName);
            for (int i = 0; i < colors.Count; i++)
            {
                var role = new KF_PaletteKitColorRole { RoleName = $"{prefix} {i + 1}", RoleColor = colors[i] };
                _palette.Roles.Add(role);
            }

            EditorUtility.SetDirty(_palette);
            RebuildRolesList();
            SetStatus($"Imported {colors.Count} role(s) from '{paletteName}'.", StatusType.Success);
        }

        private static List<Color> ParseLospecColors(string[] lines)
        {
            var result = new List<Color>();
            foreach (var rawLine in lines)
            {
                if (result.Count >= 64) break;
                var line = rawLine.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                string hex = line.StartsWith("#") ? line : "#" + line;
                if (ColorUtility.TryParseHtmlString(hex, out var color))
                    result.Add(color);
            }
            return result;
        }

        private static string CapitalizeFirst(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void ExportPalette()
        {
            if (_palette == null) { SetStatus("No palette loaded.", StatusType.Warning); return; }
            if (_palette.Roles.Count == 0) { SetStatus("Palette has no roles to export.", StatusType.Warning); return; }
            string dir = EnsureExportsDir();
            string baseName = _palette.name;
            ExportSwatchPng(dir, baseName);
            ExportHexFile(dir, baseName);
            AssetDatabase.Refresh();
            SetStatus($"Exported '{baseName}.png' + '{baseName}.hex'  →  Exports/", StatusType.Success);
        }

        private static string EnsureExportsDir()
        {
            string dir = Path.Combine(Application.dataPath,
                "Plugins", "KITforge Labs", "KF_PaletteKit", "Exports");
            Directory.CreateDirectory(dir);
            return dir;
        }

        private void ExportSwatchPng(string dir, string baseName)
        {
            int count = _palette.Roles.Count;
            var tex = new Texture2D(count * k_SwatchSize, k_SwatchSize, TextureFormat.RGBA32, false);
            var block = new Color[k_SwatchSize * k_SwatchSize];
            for (int i = 0; i < count; i++)
            {
                var c = _palette.Roles[i].RoleColor;
                for (int p = 0; p < block.Length; p++) block[p] = c;
                tex.SetPixels(i * k_SwatchSize, 0, k_SwatchSize, k_SwatchSize, block);
            }
            tex.Apply();
            File.WriteAllBytes(Path.Combine(dir, baseName + ".png"), tex.EncodeToPNG());
            Object.DestroyImmediate(tex);
        }

        private void ExportHexFile(string dir, string baseName)
        {
            var sb = new StringBuilder();
            foreach (var role in _palette.Roles)
                sb.AppendLine(ColorUtility.ToHtmlStringRGB(role.RoleColor));
            File.WriteAllText(Path.Combine(dir, baseName + ".hex"), sb.ToString());
        }

        private void SetStatus(string message, StatusType type)
        {
            if (_statusMessage == null || _statusDot == null) return;
            _statusMessage.text = message;
            bool isEmpty = string.IsNullOrEmpty(message);
            _statusDot.style.display = isEmpty ? DisplayStyle.None : DisplayStyle.Flex;
            if (!isEmpty)
                ApplyStatusDotClass(type);
        }

        private void ApplyStatusDotClass(StatusType type)
        {
            _statusDot.RemoveFromClassList("kf-statusbar__dot--success");
            _statusDot.RemoveFromClassList("kf-statusbar__dot--warning");
            _statusDot.RemoveFromClassList("kf-statusbar__dot--error");
            string cssClass = type switch
            {
                StatusType.Warning => "kf-statusbar__dot--warning",
                StatusType.Error   => "kf-statusbar__dot--error",
                _                  => "kf-statusbar__dot--success"
            };
            _statusDot.AddToClassList(cssClass);
        }

        private void SetScope(Scope scope)
        {
            _scope = scope;
            _scopeBtnScene.EnableInClassList("kfpk-scope-btn--active", scope == Scope.Scene);
            _scopeBtnProject.EnableInClassList("kfpk-scope-btn--active", scope == Scope.Project);
            _scopeBtnSelection.EnableInClassList("kfpk-scope-btn--active", scope == Scope.Selection);
        }

        private Renderer[] GetRenderersByScope()
        {
            if (_scope == Scope.Selection)
                return Selection.GetFiltered<Renderer>(SelectionMode.Deep);
            var all = Object.FindObjectsByType<Renderer>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (_scope != Scope.Scene)
                return all;
            var activeScene = SceneManager.GetActiveScene();
            var result = new List<Renderer>();
            foreach (var r in all)
            {
                if (r.gameObject.scene == activeScene)
                    result.Add(r);
            }
            return result.ToArray();
        }

        private HashSet<Material> GetMaterialsInScope()
        {
            var renderers = GetRenderersByScope();
            var set = new HashSet<Material>(renderers.Length);
            foreach (var r in renderers)
                foreach (var m in r.sharedMaterials)
                    if (m != null) set.Add(m);
            return set;
        }
    }
}
