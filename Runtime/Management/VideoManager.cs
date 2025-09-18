using System.Collections.Generic;
using UnityEngine;
using RedHeadToolz.Debugging;
using UnityEngine.ResourceManagement.AsyncOperations;
using RedHeadToolz.Addressables;
using UnityEngine.Video;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    public class VideoManager : BaseManager
    {
        [SerializeField] private List<AssetReferenceVideoClip> _videos;
        private int _loadIndex;

        public override void Init()
        {
            
            _initializationStatus = ManagerInitializationStatus.Initializing;
            LoadNextAsset();
        }

        private void LoadNextAsset()
        {
            if (_loadIndex >= _videos.Count)
            {
                RHTebug.Log("Finished loading Videos");
                if (_initializationStatus == ManagerInitializationStatus.Initializing)
                {
                    _initializationStatus = ManagerInitializationStatus.Success;
                }
                return;
            }
            _videos[_loadIndex].LoadAssetAsync().Completed += OnAssetLoaded;
        }

        void OnAssetLoaded(AsyncOperationHandle<VideoClip> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                RHTebug.LogSuccess($"Asset {handle.Result.name} loaded successfully!");
            }
            else
            {
                RHTebug.LogError($"Asset {_videos[_loadIndex]} loaded unseccessfully");
            }
            _loadIndex++;
            LoadNextAsset();
        }

        public VideoClip GetVideo(string video)
        {
            var newVideo = _videos.Find(x=>x.Asset.name == video);
            if(newVideo == null)
                RHTebug.LogError($"Video {video} not found!");
            return (VideoClip)newVideo.Asset;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/VideoManager/Collect Videos")]
        private static void CollectVideos(MenuCommand menuCommand)
        {
            VideoManager videoManager = (VideoManager)menuCommand.context;

            List<AssetReferenceVideoClip> newVideos = new List<AssetReferenceVideoClip>();
            string[] guids = AssetDatabase.FindAssets("t:VideoClip", new[] { "Assets/Videos" });
            foreach (var guid in guids)
            {
                // AssetReferenceVideo video = (AssetReferenceVideo)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(AssetReferenceVideo));
                // newVideos.Add(video);
                AddressableFactory.MakeAddressable(AssetDatabase.GUIDToAssetPath(guid));
                var assetRef = new AssetReferenceVideoClip(guid);
                newVideos.Add(assetRef);
            }

            videoManager._videos = newVideos;
            EditorUtility.SetDirty(videoManager);
        }
#endif
    }
}
