using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using RedHeadToolz.Addressables;
using System.Threading.Tasks;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    // [CreateAssetMenu(fileName = "AddressablesSpriteDatabase", menuName = "RedHeadToolz/Databases/AddressablesSpriteDatabase")]
    public class AddressablesSpriteDatabase : BaseAssetDatabase
    {
        [SerializeField] private List<AssetReferenceSprite> _sprites;

        public override async Task<InitializationStatus> Init()
        {
            RHTebug.Log("Initializing Addresables Sprite Database");
            await LoadAssets();
            return await base.Init();
        }

        private async Task LoadAssets()
        {
            List<Task> loadTasks = new List<Task>();
            foreach (var sprite in _sprites)
            {
                loadTasks.Add(LoadAsset(sprite));
            }
            await Task.WhenAll(loadTasks);
        }

        private async Task LoadAsset(AssetReferenceSprite sprite)
        {
// #if RELEASE
//             if (clip.IsDone) return;
// #endif
            var loadEvent = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<Sprite>(sprite);
#if RELEASE
            if (loadEvent.Status.Equals(AsyncOperationStatus.Succeeded))
            {
                RHTebug.Log($"Sprite {sprite.Asset.name} already loaded");
                return;
            }
#endif

            var result = await loadEvent.Task;
            if (result == null)
            {
                RHTebug.LogError($"Failed to load video clip with GUID {sprite.AssetGUID}!");
            }
            else
            {
                RHTebug.LogSuccess($"Sprite {result.name} loaded successfully!");
            }

            // var result = await UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<Sprite>(sprite).Task;
            // if (result == null)
            // {
            //     RHTebug.LogError($"Failed to load video clip with GUID {sprite.AssetGUID}!");
            // }
            // else
            // {
            //     RHTebug.LogSuccess($"Clip {result.name} loaded successfully!");
            // }
        }

        public Sprite GetSprite(string sprite)
        {
            RHTebug.Log("maybe?");
            // var newSprite = _sprites.Find(x => x.Asset.name == sprite);
            var newSprite = _sprites.Find(
            delegate(AssetReferenceSprite x)
            {
                RHTebug.Log("checking " + x.Asset);
                RHTebug.Log("checking " + x.Asset.name);
                return x.Asset.name == sprite;
            }
            );
            RHTebug.Log("" + newSprite.Asset);
            if (newSprite == null)
                RHTebug.LogError($"Sprite {sprite} not found!");
            return (Sprite)newSprite.Asset;
            // return null;
        }

        public Sprite GetSpriteByGUID(string GUID)
        {
            // foreach (var s in _sprites)
            // {
            //     RHTebug.Log(s.AssetGUID);
            // }
            var newSprite = _sprites.Find(x => x.AssetGUID == GUID);
            RHTebug.Log("" + newSprite.Asset);
            if (newSprite == null)
                RHTebug.LogError($"Sprite {GUID} not found!");
            return (Sprite)newSprite.Asset;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/AddressablesSpriteDatabase/Collect Sprites")]
        private static void CollectSprites(MenuCommand menuCommand)
        {
            AddressablesSpriteDatabase spriteDatabase = (AddressablesSpriteDatabase)menuCommand.context;

            List<AssetReferenceSprite> newSprites = new List<AssetReferenceSprite>();
            // string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets/Sprites" });
            string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets" });
            foreach (var guid in guids)
            {
                // AssetReferenceSprite sprite = (AssetReferenceSprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(AssetReferenceSprite));
                // newSprites.Add(sprite);
                AddressableFactory.MakeAddressable(AssetDatabase.GUIDToAssetPath(guid));
                var assetRef = new AssetReferenceSprite(guid);
                newSprites.Add(assetRef);
            }

            spriteDatabase._sprites = newSprites;
            EditorUtility.SetDirty(spriteDatabase);
        }
#endif
    }
}
