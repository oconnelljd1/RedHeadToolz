using System;
using UnityEngine;

namespace RedHeadToolz
{
    public class BaseManager : MonoBehaviour
    {
        protected ManagerInitializationStatus _initializationStatus = ManagerInitializationStatus.Uninitialized;
        public ManagerInitializationStatus InitializationStatus => _initializationStatus;
        public bool IsInitialized => _initializationStatus == ManagerInitializationStatus.Success;

        public Action OnInitialized;
        public Action OnDisposed;

        public virtual void Init()
        {
            _initializationStatus = ManagerInitializationStatus.Success;
            OnInitialized?.Invoke();
        }

        protected virtual void Update()
        {
            
        }

        public virtual void Dispose()
        {
            _initializationStatus = ManagerInitializationStatus.Uninitialized;
            OnDisposed?.Invoke();
            OnInitialized = null;
            OnDisposed = null;
        }
    }
}