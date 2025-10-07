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
        [SerializeField] private List<Sprite> _sprites;
        private int _loadIndex;

        public override void Init()
        {

            _initializationStatus = ManagerInitializationStatus.Initializing;
            base.Init();
        }

        public Sprite GetSprite(string sprite)
        {
            var newSprite = _sprites.Find(x=>x.name == sprite);
            if(newSprite == null)
                RHTebug.LogError($"Sprite {sprite} not found!");
            return newSprite;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/SpriteManager/Collect Sprites")]
        private static void CollectSprites(MenuCommand menuCommand)
        {
            SpriteManager spriteManager = (SpriteManager)menuCommand.context;

            List<Sprite> newSprites = new List<Sprite>();
            string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets/Sprites" });
            foreach (var guid in guids)
            {
                Sprite sprite = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Sprite));
                newSprites.Add(sprite);
                // AddressableFactory.MakeAddressable(AssetDatabase.GUIDToAssetPath(guid));
                // var assetRef = new AssetReferenceSprite(guid);
                // newSprites.Add(assetRef);
            }

            spriteManager._sprites = newSprites;
            EditorUtility.SetDirty(spriteManager);
        }
#endif
    }
}
