using System;
using System.Threading.Tasks;
using RedHeadToolz.Debugging;
using UnityEngine;

namespace RedHeadToolz
{
    // [CreateAssetMenu(fileName = "BaseAssetDatabase", menuName = "Scriptable Objects/BaseAssetDatabase")]
    public class BaseAssetDatabase : ScriptableObject
    {
        public Action OnInitialized;
        public Action OnDisposed;

        public virtual async Task<InitializationStatus> Init()
        {
            OnInitialized?.Invoke();
            return InitializationStatus.Success;
        }
    }
}
