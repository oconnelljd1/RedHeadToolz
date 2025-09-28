using UnityEngine;
using RedHeadToolz.Addressables;
using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine.ResourceManagement.AsyncOperations;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    [CreateAssetMenu(fileName = "AddressablesAudioClipDatabase", menuName = "RedHeadToolz/Databases/AddressablesAudioClipDatabase")]
    public class AddressablesAudioClipDatabase : BaseAssetDatabase
    {
        [SerializeField] private List<AssetReferenceAudioClip> _clips;
        private int _loadIndex = 0;

        public override void Init()
        {
            RHTebug.Log("Initializing Addresables Audio Clip Database");
            _initializationStatus = ManagerInitializationStatus.Initializing;
            LoadNextAsset();
            // base.Init();
        }

        private void LoadNextAsset()
        {
            RHTebug.Log($"index: {_loadIndex}, count: {_clips.Count}");
            if (_loadIndex >= _clips.Count)
            {
                RHTebug.Log("Finished loading Audio Clips");
                if (_initializationStatus == ManagerInitializationStatus.Initializing)
                {
                    _initializationStatus = ManagerInitializationStatus.Success;
                    OnInitialized?.Invoke();
                }
                return;
            }
            _clips[_loadIndex].LoadAssetAsync<AudioClip>().Completed += OnAssetLoaded;
        }

        void OnAssetLoaded(AsyncOperationHandle<AudioClip> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                RHTebug.LogSuccess($"Asset {handle.Result.name} loaded successfully!");
            }
            else
            {
                RHTebug.LogError($"Asset {_clips[_loadIndex]} loaded unseccessfully");
            }
            _loadIndex++;
            LoadNextAsset();
        }

        public AudioClip GetAudioClip(string clip)
        {
            var newClip = _clips.Find(x => x.Asset.name == clip);
            if (newClip == null)
                RHTebug.LogError($"Clip {clip} not found!");
            return (AudioClip)newClip.Asset;
        }

        public AudioClip GetAudioClipByGUID(string GUID)
        {
            var newClip = _clips.Find(x => x.AssetGUID == GUID);
            if (newClip == null)
                RHTebug.LogError($"Clip {GUID} not found!");
            return (AudioClip)newClip.Asset;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/AddressablesAudioClipDatabase/Collect Clips")]
        private static void CollectClips(MenuCommand menuCommand)
        {
            AddressablesAudioClipDatabase videoManager = (AddressablesAudioClipDatabase)menuCommand.context;

            List<AssetReferenceAudioClip> newClips = new List<AssetReferenceAudioClip>();
            string[] guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets" });
            foreach (var guid in guids)
            {
                AddressableFactory.MakeAddressable(AssetDatabase.GUIDToAssetPath(guid));
                var assetRef = new AssetReferenceAudioClip(guid);
                newClips.Add(assetRef);
            }

            videoManager._clips = newClips;
            EditorUtility.SetDirty(videoManager);
        }
#endif
    }
}
