using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RedHeadToolz.Audio
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance;
        [SerializeField] private GameObject _channelPrefab;
        [SerializeField] private List<AudioClip> _clips;
        private List<AudioChannel> _channels = new List<AudioChannel>();

        // Start is called before the first frame update
        void Awake()
        {
            if(Instance)
                Destroy(gameObject);
            
            Instance = this;
            DontDestroyOnLoad(this);
        }

        public void AddChannel(string id, int sources = 1)
        {
            foreach(var chan in _channels)
            {
                if(chan.Id == id)
                {
                    Debug.LogError($"Channel with Id {id} already exists, aborting.");
                    return;
                }
            }

            AudioChannel channel = Instantiate(_channelPrefab, gameObject.transform).GetComponent<AudioChannel>();
            channel.Init(id, sources);

            _channels.Add(channel);
        }

        public AudioChannel GetChannel(string channel)
        {
            return _channels.Find(x=>x.Id == channel);
        }

        public AudioClip GetClip(string clip)
        {
            return _clips.Find(x=>x.name == clip);
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
        [MenuItem("CONTEXT/AudioController/Collect Clips")]
        private static void CollectClips(MenuCommand menuCommand)
        {
            AudioController audioController = (AudioController)menuCommand.context;

            List<AudioClip> newClips = new List<AudioClip>();
            string[] guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Audio" });
            foreach (var guid in guids)
            {
                AudioClip clip = (AudioClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(AudioClip));
                newClips.Add(clip);
            }

            audioController._clips = newClips;
            EditorUtility.SetDirty(audioController);
        }
#endif
    }
}