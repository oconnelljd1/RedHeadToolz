using UnityEngine;
using RedHeadToolz.Addressables;
using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    // [CreateAssetMenu(fileName = "AddressablesAudioClipDatabase", menuName = "RedHeadToolz/Databases/AddressablesAudioClipDatabase")]
    public class AddressablesAudioClipDatabase : BaseAssetDatabase
    {
        [SerializeField] private List<AssetReferenceAudioClip> _clips;

        public override async Task<InitializationStatus> Init()
        {
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

        private async Task LoadAsset(AssetReferenceAudioClip clip)
        {
#if RELEASE
            if (clip.IsDone) return;
#endif
            
            var result = await UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<AudioClip>(clip).Task;
            if (result == null)
            {
                RHTebug.LogError($"Failed to load video clip with GUID {clip.AssetGUID}!");
            }
            else
            {
                RHTebug.LogSuccess($"AudioClip {result.name} loaded successfully!");
            }
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
