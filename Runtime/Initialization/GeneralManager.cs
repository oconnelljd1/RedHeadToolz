using System.Collections;
using System.Collections.Generic;
using RedHeadToolz.Debugging;
using RedHeadToolz.Utils;
using UnityEngine;

namespace RedHeadToolz
{
    public enum ManagerInitializationStatus
    {
        Uninitialized,
        Initializing,
        Success,
        Failure
    }

    public class GeneralManager : Singleton<GeneralManager>
    {
        private ManagerInitializationStatus _initializationStatus;
        public ManagerInitializationStatus InitializationStatus => _initializationStatus;
        public bool IsInitialized => _initializationStatus == ManagerInitializationStatus.Success;

        [SerializeField] private List<BaseManager> _defaultManagers = new List<BaseManager>();

        private List<BaseManager> _managers = new List<BaseManager>();

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(InitializeManagers());
        }

        private IEnumerator InitializeManagers()
        {
            _initializationStatus = ManagerInitializationStatus.Initializing;

            foreach (BaseManager manager in _defaultManagers)
            {
                // not sure if screen manager will work for this where it has a rect transform and needs a canvas
                BaseManager newManager = Instantiate(manager, transform).GetComponent<BaseManager>();
                _managers.Add(newManager);
                if(newManager.InitializationStatus == ManagerInitializationStatus.Uninitialized)
                {
                    newManager.Init();
                }

                while(newManager.IsInitialized == false)
                {
                    yield return null;
                }
            }
            
            _initializationStatus = ManagerInitializationStatus.Success;
        }

        public T GetManager<T>() where T : BaseManager
        {
            foreach (BaseManager manager in _managers)
            {
                if(manager is T)
                {
                    return manager as T;
                }
            }

            RHTebug.LogWarning($"Manager of type {typeof(T)} not found.");
            return null;
        }

        // does it make sense to have an add manager funciton
        // because if a module needs time to initialize...
        // whatever adds it shouldn't immediatley access it
        // maybe make it some kind of await/async function
    }
}