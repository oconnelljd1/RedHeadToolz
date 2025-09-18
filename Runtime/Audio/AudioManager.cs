using System.Collections.Generic;
using RedHeadToolz.Addressables;
using RedHeadToolz.Debugging;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz.Audio
{
    public class AudioManager : BaseManager
    {
        [SerializeField] private GameObject _channelPrefab;
        [SerializeField] private List<string> _channelIds = new List<string>();
        // [SerializeField] private List<AudioClip> _clips;
        [SerializeField] private List<AssetReferenceAudioClip> _clips;
        private List<AudioChannel> _channels = new List<AudioChannel>();
        private int _loadIndex;

        public override void Init()
        {
            _initializationStatus = ManagerInitializationStatus.Initializing;
            // RHTebug.Log("Audio Init");
            foreach (var id in _channelIds)
            {
                AddChannel(id);
            }
            LoadNextAsset();
            // base.Init();
        }

        private void LoadNextAsset()
        {
            if (_loadIndex >= _clips.Count)
            {
                RHTebug.Log("Finished loading Clips");
                if (_initializationStatus == ManagerInitializationStatus.Initializing)
                {
                    _initializationStatus = ManagerInitializationStatus.Success;
                }
                return;
            }
            _clips[_loadIndex].LoadAssetAsync().Completed += OnAssetLoaded;
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

        public void AddChannel(string id, int sources = 1)
        {
            AudioChannel channel = _channels.Find(x => x.Id == id);
            if (channel != null)
            {
                Debug.LogError($"Channel with Id {id} already exists, aborting.");
                return;
            }

            channel = Instantiate(_channelPrefab, gameObject.transform).GetComponent<AudioChannel>();
            channel.Init(id, sources);

            _channels.Add(channel);
        }

        public AudioChannel GetChannel(string channel)
        {
            return _channels.Find(x=>x.Id == channel);
        }

        public AudioClip GetClip(string clip)
        {
            var newClip = _clips.Find(x=>x.Asset.name == clip);
            if(newClip == null)
                RHTebug.LogError($"Clip {clip} not found!");
            return (AudioClip)newClip.Asset;
        }

        // Depricate, find channels and play there
        public void PlaySoundOnChannel(string clip, string channel)
        {
            var chan = _channels.Find(x=> x.Id == channel);
            if(chan == null) return;

            chan.Play(GetClip(clip));
        }

        // depricate, find channel and stop there
        public void StopChannel(string channel)
        {
            var chan = _channels.Find(x=> x.Id == channel);
            if(chan == null) return;

            chan.Stop();
        }

        // depricate, find channel and mute there
        public void SetChannelMuted(string channel, bool mute)
        {
            var chan = _channels.Find(x=> x.Id == channel);
            if(chan == null) return;
            
            if(mute)
                chan.Mute();
            else
                chan.Unmute();
        }

        // public void SetClips(List<AudioClip> newClips)
        // {
        //     _clips = newClips;
        // }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/AudioManager/Collect Clips")]
        private static void CollectClips(MenuCommand menuCommand)
        {
            AudioManager audioManager = (AudioManager)menuCommand.context;

            List<AssetReferenceAudioClip> newClips = new List<AssetReferenceAudioClip>();
            string[] guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Audio" });
            foreach (var guid in guids)
            {
                // AssetReferenceAudioClip clip = (AssetReferenceAudioClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(AssetReferenceAudioClip));
                // newClips.Add(clip);
                AddressableFactory.MakeAddressable(AssetDatabase.GUIDToAssetPath(guid));
                var assetRef = new AssetReferenceAudioClip(guid);
                newClips.Add(assetRef);
            }

            audioManager._clips = newClips;
            EditorUtility.SetDirty(audioManager);
        }
#endif
    }
}