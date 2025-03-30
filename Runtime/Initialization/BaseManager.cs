using UnityEngine;

namespace RedHeadToolz
{
    public class BaseManager : MonoBehaviour
    {
        protected ManagerInitializationStatus _initializationStatus = ManagerInitializationStatus.Uninitialized;
        public ManagerInitializationStatus InitializationStatus => _initializationStatus;
        public bool IsInitialized => _initializationStatus == ManagerInitializationStatus.Success;

        public virtual void Init()
        {
            _initializationStatus = ManagerInitializationStatus.Success;
        }

        public virtual void Update()
        {
            
        }
    }
}