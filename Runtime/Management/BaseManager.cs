using System;
using System.Threading.Tasks;
using UnityEngine;

namespace RedHeadToolz
{
    public class BaseManager : MonoBehaviour
    {
        public Action OnInitialized;
        public Action OnDisposed;

        public virtual async Task<InitializationStatus> Init()
        {
            OnInitialized?.Invoke();
            return InitializationStatus.Success;
        }

        public virtual void Dispose()
        {
            OnDisposed?.Invoke();
            OnInitialized = null;
            OnDisposed = null;
        }
    }
}