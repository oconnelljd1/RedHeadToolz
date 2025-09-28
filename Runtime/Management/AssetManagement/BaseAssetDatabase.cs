using System;
using RedHeadToolz.Debugging;
using UnityEngine;

namespace RedHeadToolz
{
    // [CreateAssetMenu(fileName = "BaseAssetDatabase", menuName = "Scriptable Objects/BaseAssetDatabase")]
    public class BaseAssetDatabase : ScriptableObject
    {
        
        protected ManagerInitializationStatus _initializationStatus = ManagerInitializationStatus.Uninitialized;
        public ManagerInitializationStatus InitializationStatus => _initializationStatus;
        public bool IsInitialized => _initializationStatus == ManagerInitializationStatus.Success;

        public Action OnInitialized;
        public Action OnDisposed;

        public virtual void Init()
        {
            // RHTebug.Log("Herr");
            _initializationStatus = ManagerInitializationStatus.Success;
            OnInitialized?.Invoke();
        }
    }
}
