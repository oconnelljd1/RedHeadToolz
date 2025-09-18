using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using RedHeadToolz.Debugging;
using UnityEngine.ResourceManagement.AsyncOperations;
using RedHeadToolz.Addressables;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    public class SpriteManager : BaseManager
    {
        [SerializeField] private List<AssetReferenceSprite> _sprites;
        private int _loadIndex;

        public override void Init()
        {
            
            _initializationStatus = ManagerInitializationStatus.Initializing;
            LoadNextAsset();
        }

        private void LoadNextAsset()
        {
            if (_loadIndex >= _sprites.Count)
            {
                RHTebug.Log("Finished loading Sprites");
                if (_initializationStatus == ManagerInitializationStatus.Initializing)
                {
                    _initializationStatus = ManagerInitializationStatus.Success;
                }
                return;
            }
            _sprites[_loadIndex].LoadAssetAsync().Completed += OnAssetLoaded;
        }

        void OnAssetLoaded(AsyncOperationHandle<Sprite> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                RHTebug.LogSuccess($"Asset {handle.Result.name} loaded successfully!");
            }
            else
            {
                RHTebug.LogError($"Asset {_sprites[_loadIndex]} loaded unseccessfully");
            }
            _loadIndex++;
            LoadNextAsset();
        }

        public Sprite GetSprite(string sprite)
        {
            var newSprite = _sprites.Find(x=>x.Asset.name == sprite);
            if(newSprite == null)
                RHTebug.LogError($"Sprite {sprite} not found!");
            return (Sprite)newSprite.Asset;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/SpriteManager/Collect Sprites")]
        private static void CollectSprites(MenuCommand menuCommand)
        {
            SpriteManager spriteManager = (SpriteManager)menuCommand.context;

            List<AssetReferenceSprite> newSprites = new List<AssetReferenceSprite>();
            string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets/Sprites" });
            foreach (var guid in guids)
            {
                // AssetReferenceSprite sprite = (AssetReferenceSprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(AssetReferenceSprite));
                // newSprites.Add(sprite);
                AddressableFactory.MakeAddressable(AssetDatabase.GUIDToAssetPath(guid));
                var assetRef = new AssetReferenceSprite(guid);
                newSprites.Add(assetRef);
            }

            spriteManager._sprites = newSprites;
            EditorUtility.SetDirty(spriteManager);
        }
#endif
    }
}
