using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Video;

namespace RedHeadToolz.Addressables
{
    [System.Serializable]
    public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
    {
        public AssetReferenceAudioClip(string guid) : base(guid)
        {
            
        }
    }

    [System.Serializable]
    public class AssetReferenceVideoClip : AssetReferenceT<VideoClip>
    {
        public AssetReferenceVideoClip(string guid) : base(guid)
        {
            
        }
    }
}
