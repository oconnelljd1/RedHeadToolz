#if UNITY_EDITOR
using RedHeadToolz.Debugging;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace RedHeadToolz.Addressables
{
    public static class AddressableFactory
    {
        // public static void MakeAddressable(string guid)
        // {
        //     var settings = AddressableAssetSettingsDefaultObject.Settings;
        //     if (settings == null)
        //     {
        //         RHTebug.LogError("AddressableAssetSettings not found.");
        //         return;
        //     }

        //     // var guid = AssetDatabase.AssetPathToGUID(assetPath);
        //     var entry = settings.FindAssetEntry(guid);
        //     if (entry == null)
        //     {
        //         entry = settings.CreateOrMoveEntry(guid, settings.DefaultGroup);
        //     }
        //     entry.address = address ?? System.IO.Path.GetFileNameWithoutExtension(assetPath);
        //     EditorUtility.SetDirty(settings);
        //     settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
        //     AssetDatabase.SaveAssets();
        // }
        public static void MakeAddressable(string assetPath, string address = null)
        {
            // CreateDefaultSettings();
            if (AddressableAssetSettingsDefaultObject.Settings == null)
            {
                RHTebug.LogError($"AddressableAssetSettings is null, go to Window > Asset Management > Addressables > Groups to setup defaults");
                return;
            }
            var settings = AddressableAssetSettingsDefaultObject.Settings;

            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            var entry = settings.FindAssetEntry(guid);
            if (entry == null)
            {
                entry = settings.CreateOrMoveEntry(guid, settings.DefaultGroup);
            }
            entry.address = address ?? System.IO.Path.GetFileNameWithoutExtension(assetPath);
            EditorUtility.SetDirty(settings);
            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
            AssetDatabase.SaveAssets();
        }

        // private static void CreateDefaultSettings()
        // {
        //     RHTebug.Log("CreateDefaultSettings");
        //     if (AddressableAssetSettingsDefaultObject.Settings != null) return;

        //     RHTebug.LogWarning("AddressableAssetSettings not found, creating default.");

        //     // Create default AddressableAssetSettings in Assets/AddressableAssetsData
        //     AddressableAssetSettings.Create(AddressableAssetSettingsDefaultObject.kDefaultConfigFolder, "AddressableAssetSettings", true, true);
        //     AssetDatabase.SaveAssets();
        //     AssetDatabase.Refresh();
            
        //     UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
        // }
    }
    #endif
}
