using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine;
using System.Collections;
using UnityEngine.Video;
using System.Threading.Tasks;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    public class AssetManager : BaseManager
    {
        [SerializeField] private List<BaseAssetDatabase> _databases;

        public override async Task<InitializationStatus> Init()
        {
            await InitializeDatabases();
            return await base.Init();
        }

        private async Task InitializeDatabases()
        {
            foreach (BaseAssetDatabase database in _databases)
            {
                // RHTebug.Log($"DatabaseStatus: {database.InitializationStatus}");
                if (database == null) continue;

                var result = await database.Init(); // forcing Init becuase scriptable object holds onto it's values between play sessions

                if (result == InitializationStatus.Success)
                {
                    RHTebug.LogSuccess($"Database {database.name} initialized successfully.");
                }
                else if (result == InitializationStatus.Failure)
                {
                    RHTebug.LogError($"Database {database.name} failed to initialize.");
                }
            }
        }

        public T GetDatabase<T>() where T : BaseAssetDatabase
        {
            foreach (BaseAssetDatabase database in _databases)
            {
                if (database is T)
                {
                    return database as T;
                }
            }

            RHTebug.LogWarning($"Database of type {typeof(T)} not found.");
            return null;
        }

        public Sprite GetSprite(string sprite)
        {
            if (GetDatabase<SpriteDatabase>() != null)
            {
                var newSprite = GetDatabase<SpriteDatabase>().GetSprite(sprite);
                if (newSprite != null)
                {
                    return newSprite;
                }
            }
            if (GetDatabase<AddressablesSpriteDatabase>() != null)
            {
                RHTebug.Log("herrrr");
                var newSprite = GetDatabase<AddressablesSpriteDatabase>().GetSprite(sprite);
                if (newSprite != null)
                {
                    return newSprite;
                }
            }
            RHTebug.LogError($"Sprite {sprite} not found in any database!");
            return null;
        }

        public Sprite GetSpriteByGUID(string GUID)
        {
            if (GetDatabase<AddressablesSpriteDatabase>() != null)
            {
                var newSprite = GetDatabase<AddressablesSpriteDatabase>().GetSpriteByGUID(GUID);
                if (newSprite != null)
                {
                    return newSprite;
                }
            }
            RHTebug.LogError($"Sprite {GUID} not found in any database!");
            return null;
        }

        public AudioClip GetAudioClip(string clip)
        {
            if (GetDatabase<AudioClipDatabase>() != null)
            {
                var newClip = GetDatabase<AudioClipDatabase>().GetAudioClip(clip);
                if (newClip != null)
                {
                    return newClip;
                }
            }
            if (GetDatabase<AddressablesAudioClipDatabase>() != null)
            {
                var newClip = GetDatabase<AddressablesAudioClipDatabase>().GetAudioClip(clip);
                if (newClip != null)
                {
                    return newClip;
                }
            }
            RHTebug.LogError($"Audio Clip {clip} not found in any database!");
            return null;
        }
        
        public AudioClip GetAudioClipByGUID(string GUID)
        {
            if (GetDatabase<AddressablesAudioClipDatabase>() != null)
            {
                var newClip = GetDatabase<AddressablesAudioClipDatabase>().GetAudioClipByGUID(GUID);
                if (newClip != null)
                {
                    return newClip;
                }
            }
            RHTebug.LogError($"Audio Clip {GUID} not found in any database!");
            return null;
        }
        
        public VideoClip GetVideoClip(string clip)
        {
            if (GetDatabase<VideoClipDatabase>() != null)
            {
                var newClip = GetDatabase<VideoClipDatabase>().GetVideoClip(clip);
                if (newClip != null)
                {
                    return newClip;
                }
            }
            if (GetDatabase<AddressablesVideoClipDatabase>() != null)
            {
                var newClip = GetDatabase<AddressablesVideoClipDatabase>().GetVideoClip(clip);
                if (newClip != null)
                {
                    return newClip;
                }
            }
            RHTebug.LogError($"Video Clip {clip} not found in any database!");
            return null;
        }
        
        public VideoClip GetVideoClipByGUID(string GUID)
        {
            if (GetDatabase<AddressablesVideoClipDatabase>() != null)
            {
                var newClip = GetDatabase<AddressablesVideoClipDatabase>().GetVideoClipByGUID(GUID);
                if (newClip != null)
                {
                    return newClip;
                }
            }
            RHTebug.LogError($"Video Clip {GUID} not found in any database!");
            return null;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/AssetManager/Collect Databases")]
    private static void CollectDatabases(MenuCommand menuCommand)
    {
        AssetManager controller = (AssetManager)menuCommand.context;

        List<BaseAssetDatabase> newDatabases = new List<BaseAssetDatabase>();
        // string[] guids = AssetDatabase.FindAssets("t:ClosedCaptionsData", new[] { "Assets/ScriptableObjects/ClosedCaptions" });
        string[] guids = AssetDatabase.FindAssets("t:BaseAssetDatabase", new[] { "Assets" });
        foreach (var guid in guids)
        {
            BaseAssetDatabase database = (BaseAssetDatabase)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(BaseAssetDatabase));
            newDatabases.Add(database);
        }

        controller._databases = newDatabases;
        EditorUtility.SetDirty(controller);
    }
#endif
    }
}
        