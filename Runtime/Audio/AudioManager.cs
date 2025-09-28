using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine;

namespace RedHeadToolz.Audio
{
    public class AudioManager : BaseManager
    {
        [SerializeField] private GameObject _channelPrefab;
        [SerializeField] private List<string> _channelIds = new List<string>();
        // [SerializeField] private List<AudioClip> _clips;
        private List<AudioChannel> _channels = new List<AudioChannel>();

        public override void Init()
        {
            _initializationStatus = ManagerInitializationStatus.Initializing;
            // RHTebug.Log("Audio Init");
            foreach (var id in _channelIds)
            {
                RHTebug.Log($"Adding Audio Channel {id}");
                AddChannel(id);
            }
            base.Init();
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
            var chan = _channels.Find(x=>x.Id == channel);
            if (chan == null)
            {
                RHTebug.LogError($"Channel {channel} not found");
            }
            return chan;
        }

        // Depricate, find channels and play there
        public void PlaySoundOnChannel(string clip, string channel)
        {
            var chan = _channels.Find(x=> x.Id == channel);
            if(chan == null) return;

            chan.Play(GeneralManager.Instance.GetManager<AssetManager>().GetAudioClip(clip));
        }

        // depricate, find channel and stop there
        public void StopChannel(string channel)
        {
            var chan = _channels.Find(x=> x.Id == channel);
            if(chan == null) return;

            chan.Stop();
        }

        public void StopAllChannels()
        {
            foreach (var channel in _channels)
            {
                channel.Stop();
            }
        }

        // depricate, find channel and mute there
        public void SetChannelMuted(string channel, bool mute)
        {
            var chan = _channels.Find(x => x.Id == channel);
            if (chan == null) return;

            if (mute)
                chan.Mute();
            else
                chan.Unmute();
        }

        // public void SetClips(List<AudioClip> newClips)
        // {
        //     _clips = newClips;
        // }

    }
}