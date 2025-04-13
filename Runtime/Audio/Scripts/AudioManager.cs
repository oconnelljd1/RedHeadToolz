using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEditor;
using UnityEngine;

namespace RedHeadToolz.Audio
{
    public class AudioManager : BaseManager
    {
        [SerializeField] private GameObject _channelPrefab;
        [SerializeField] private List<string> _channelIds = new List<string>();
        [SerializeField] private List<AudioClip> _clips;
        private List<AudioChannel> _channels = new List<AudioChannel>();

        public override void Init()
        {
            // RHTebug.Log("Audio Init");
            foreach(var id in _channelIds)
            {
                AddChannel(id);
            }
            base.Init();
        }

        public void AddChannel(string id, int sources = 1)
        {
            AudioChannel channel = _channels.Find(x => x.Id == id);
            if(channel != null)
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
            var newClip = _clips.Find(x=>x.name == clip);
            if(newClip == null)
                RHTebug.LogError($"Clip {clip} not found!");
            return newClip;
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

        public void SetClips(List<AudioClip> newClips)
        {
            _clips = newClips;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/AudioManager/Collect Clips")]
        private static void CollectClips(MenuCommand menuCommand)
        {
            AudioManager audioManager = (AudioManager)menuCommand.context;

            List<AudioClip> newClips = new List<AudioClip>();
            string[] guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Audio" });
            foreach (var guid in guids)
            {
                AudioClip clip = (AudioClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(AudioClip));
                newClips.Add(clip);
            }

            audioManager._clips = newClips;
            EditorUtility.SetDirty(audioManager);
        }
#endif
    }
}