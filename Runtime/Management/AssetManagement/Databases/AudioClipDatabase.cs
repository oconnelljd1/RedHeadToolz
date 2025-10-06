using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    // [CreateAssetMenu(fileName = "AudioClipDatabase", menuName = "RedHeadToolz/Databases/AudioClipDatabase")]
    public class AudioClipDatabase : BaseAssetDatabase
    {
        [SerializeField] private List<AudioClip> _clips;
        
        public AudioClip GetAudioClip(string clip)
        {
            var newClip = _clips.Find(x=>x.name == clip);
            if(newClip == null)
                RHTebug.LogError($"Clip {clip} not found!");
            return newClip;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/AudioManager/Collect Clips")]
        private static void CollectClips(MenuCommand menuCommand)
        {
            AudioClipDatabase audioClipDatabase = (AudioClipDatabase)menuCommand.context;

            List<AudioClip> newClips = new List<AudioClip>();
            string[] guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Audio" });
            foreach (var guid in guids)
            {
                AudioClip clip = (AudioClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(AudioClip));
                newClips.Add(clip);
            }

            audioClipDatabase._clips = newClips;
            EditorUtility.SetDirty(audioClipDatabase);
        }
#endif
    }
}
