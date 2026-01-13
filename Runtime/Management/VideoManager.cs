using System.Collections.Generic;
using UnityEngine;
using RedHeadToolz.Debugging;
using UnityEngine.Video;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    public class VideoManager : BaseManager
    {
        [SerializeField] private List<VideoClip> _videos;
        private int _loadIndex;

        public override void Init()
        {

            _initializationStatus = ManagerInitializationStatus.Initializing;
            // LoadNextAsset();
            base.Init();
        }

        // private void LoadNextAsset()
        // {
        //     if (_loadIndex >= _videos.Count)
        //     {
        //         RHTebug.Log("Finished loading Videos");
        //         if (_initializationStatus == ManagerInitializationStatus.Initializing)
        //         {
        //             _initializationStatus = ManagerInitializationStatus.Success;
        //         }
        //         return;
        //     }
        //     _videos[_loadIndex].LoadAssetAsync().Completed += OnAssetLoaded;
        // }

        // void OnAssetLoaded(AsyncOperationHandle<VideoClip> handle)
        // {
        //     if (handle.Status == AsyncOperationStatus.Succeeded)
        //     {
        //         RHTebug.LogSuccess($"Asset {handle.Result.name} loaded successfully!");
        //     }
        //     else
        //     {
        //         RHTebug.LogError($"Asset {_videos[_loadIndex]} loaded unseccessfully");
        //     }
        //     _loadIndex++;
        //     LoadNextAsset();
        // }

        public VideoClip GetVideo(string video)
        {
            var newVideo = _videos.Find(x=>x.name == video);
            if(newVideo == null)
                RHTebug.LogError($"Video {video} not found!");
            return newVideo;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/VideoManager/Collect Videos")]
        private static void CollectVideos(MenuCommand menuCommand)
        {
            VideoManager videoManager = (VideoManager)menuCommand.context;

            List<VideoClip> newVideos = new List<VideoClip>();
            string[] guids = AssetDatabase.FindAssets("t:VideoClip", new[] { "Assets" });
            foreach (var guid in guids)
            {
                VideoClip video = (VideoClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(VideoClip));
                newVideos.Add(video);
                // AddressableFactory.MakeAddressable(AssetDatabase.GUIDToAssetPath(guid));
                // var assetRef = new AssetReferenceVideoClip(guid);
                // newVideos.Add(assetRef);
            }

            videoManager._videos = newVideos;
            EditorUtility.SetDirty(videoManager);
        }
#endif
    }
}
