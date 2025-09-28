using UnityEngine;
using RedHeadToolz.Addressables;
using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    [CreateAssetMenu(fileName = "AddressablesVideoClipDatabase", menuName = "RedHeadToolz/Databases/AddressablesVideoClipDatabase")]
    public class AddressablesVideoClipDatabase : BaseAssetDatabase
    {
        [SerializeField] private List<AssetReferenceVideoClip> _clips;
        private int _loadIndex;

        public override void Init()
        {
            RHTebug.Log("Initializing Addresables Video Clip Database");
            _initializationStatus = ManagerInitializationStatus.Initializing;
            LoadNextAsset();
            // base.Init();
        }

        private void LoadNextAsset()
        {
            if (_loadIndex >= _clips.Count)
            {
                RHTebug.Log("Finished loading VideoClips");
                if (_initializationStatus == ManagerInitializationStatus.Initializing)
                {
                    _initializationStatus = ManagerInitializationStatus.Success;
                }
                return;
            }
            _clips[_loadIndex].LoadAssetAsync<VideoClip>().Completed += OnAssetLoaded;
        }

        void OnAssetLoaded(AsyncOperationHandle<VideoClip> handle)
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

        public VideoClip GetVideoClip(string clip)
        {
            var newClip = _clips.Find(x => x.Asset.name == clip);
            if (newClip == null)
                RHTebug.LogError($"Clip {clip} not found!");
            return (VideoClip)newClip.Asset;
        }

        public VideoClip GetVideoClipByGUID(string GUID)
        {
            var newClip = _clips.Find(x => x.AssetGUID == GUID);
            if (newClip == null)
                RHTebug.LogError($"Clip {GUID} not found!");
            return (VideoClip)newClip.Asset;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/AddressablesVideoClipDatabase/Collect Clips")]
        private static void CollectClips(MenuCommand menuCommand)
        {
            AddressablesVideoClipDatabase audioManager = (AddressablesVideoClipDatabase)menuCommand.context;

            List<AssetReferenceVideoClip> newClips = new List<AssetReferenceVideoClip>();
            // string[] guids = AssetDatabase.FindAssets("t:VideoClip", new[] { "Assets/Video" });
            string[] guids = AssetDatabase.FindAssets("t:VideoClip", new[] { "Assets" });
            foreach (var guid in guids)
            {
                // AssetReferenceVideoClip clip = (AssetReferenceVideoClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(AssetReferenceVideoClip));
                // newClips.Add(clip);
                AddressableFactory.MakeAddressable(AssetDatabase.GUIDToAssetPath(guid));
                var assetRef = new AssetReferenceVideoClip(guid);
                newClips.Add(assetRef);
            }

            audioManager._clips = newClips;
            EditorUtility.SetDirty(audioManager);
        }
#endif
    }
}
