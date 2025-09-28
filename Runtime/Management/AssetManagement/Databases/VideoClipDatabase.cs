using System.Collections.Generic;
using RedHeadToolz.Debugging;
using UnityEngine;
using UnityEngine.Video;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    [CreateAssetMenu(fileName = "VideoClipDatabase", menuName = "RedHeadToolz/Databases/VideoClipDatabase")]
    public class VideoClipDatabase : BaseAssetDatabase
    {
        [SerializeField] private List<VideoClip> _clips;
        
        public VideoClip GetVideoClip(string clip)
        {
            var newClip = _clips.Find(x=>x.name == clip);
            if(newClip == null)
                RHTebug.LogError($"Clip {clip} not found!");
            return newClip;
        }

#if UNITY_EDITOR
        [MenuItem("CONTEXT/VideoManager/Collect Clips")]
        private static void CollectClips(MenuCommand menuCommand)
        {
            VideoClipDatabase videoClipDatabase = (VideoClipDatabase)menuCommand.context;

            List<VideoClip> newClips = new List<VideoClip>();
            string[] guids = AssetDatabase.FindAssets("t:VideoClip", new[] { "Assets/Video" });
            foreach (var guid in guids)
            {
                VideoClip clip = (VideoClip)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(VideoClip));
                newClips.Add(clip);
            }

            videoClipDatabase._clips = newClips;
            EditorUtility.SetDirty(videoClipDatabase);
        }
#endif
    }
}
