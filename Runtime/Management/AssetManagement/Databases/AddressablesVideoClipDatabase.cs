using UnityEngine;
using RedHeadToolz.Addressables;
using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;
using System.Threading.Tasks;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    [CreateAssetMenu(fileName = "AddressablesVideoClipDatabase", menuName = "RedHeadToolz/Databases/AddressablesVideoClipDatabase")]
    public class AddressablesVideoClipDatabase : BaseAssetDatabase
    {
        [SerializeField] private List<AssetReferenceVideoClip> _clips;

        public override async Task<InitializationStatus> Init()
        {
            RHTebug.Log("Initializing Addresables Video Clip Database");
            await LoadAssets();
            return await base.Init();
        }

        private async Task LoadAssets()
        {
            List<Task> loadTasks = new List<Task>();
            foreach (var clip in _clips)
            {
                loadTasks.Add(LoadAsset(clip));
            }
            await Task.WhenAll(loadTasks);
        }

        private async Task LoadAsset(AssetReferenceVideoClip clip)
        {
#if RELEASE
            if (clip.IsDone) return;
#endif

            var result = await UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<VideoClip>(clip).Task;
            if (result == null)
            {
                RHTebug.LogError($"Failed to load video clip with GUID {clip.AssetGUID}!");
            }
            else
            {
                RHTebug.LogSuccess($"Videoclip {result.name} loaded successfully!");
            }
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
