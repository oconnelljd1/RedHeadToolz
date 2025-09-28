using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine;
using System.Collections;
using UnityEngine.Video;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    public class AssetManager : BaseManager
    {
        [SerializeField] private List<BaseAssetDatabase> _databases;

        public override void Init()
        {
            _initializationStatus = ManagerInitializationStatus.Initializing;
            StartCoroutine(InitializeDatabases());
            // base.Init();
        }

        private IEnumerator InitializeDatabases()
        {
            foreach (BaseAssetDatabase database in _databases)
            {
                // RHTebug.Log($"DatabaseStatus: {database.InitializationStatus}");
                if (database == null) continue;

                database.Init(); // forcing Init becuase scriptable object holds onto it's values between play sessions

                while (database.IsInitialized == false)
                {
                    yield return null;
                }
            }

            // RHTebug.Log("AssetManager initialized!");
            _initializationStatus = ManagerInitializationStatus.Success;
            OnInitialized?.Invoke();
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
        