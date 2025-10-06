using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    // [CreateAssetMenu(fileName = "SpriteDatabase", menuName = "RedHeadToolz/Databases/SpriteDatabase")]
    public class SpriteDatabase : BaseAssetDatabase
    {
        [SerializeField] private List<Sprite> _sprites;
        public Sprite GetSprite(string sprite)
        {
            foreach (Sprite s in _sprites)
            {
                if (s.name == sprite)
                {
                    return s;
                }
            }
            RHTebug.LogError($"Sprite {sprite} not found!");
            return null;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/SpriteDatabase/Collect Sprites")]
        private static void CollectClips(MenuCommand menuCommand)
        {
            SpriteDatabase audioManager = (SpriteDatabase)menuCommand.context;

            List<Sprite> newSprites = new List<Sprite>();
            string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets" });
            foreach (var guid in guids)
            {
                Sprite clip = (Sprite)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Sprite));
                newSprites.Add(clip);
            }

            audioManager._sprites = newSprites;
            EditorUtility.SetDirty(audioManager);
        }
#endif
    }
}
