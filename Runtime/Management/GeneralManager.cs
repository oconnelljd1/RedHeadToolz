using System.Collections;
using System.Collections.Generic;
using RedHeadToolz.Debugging;
using RedHeadToolz.Utils;
using UnityEngine;
using System;
using System.Threading.Tasks;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RedHeadToolz
{
    public enum InitializationStatus
    {
        Uninitialized,
        Initializing,
        Success,
        Failure
    }

    public class GeneralManager : Singleton<GeneralManager>
    {

        [SerializeField] private List<BaseManager> _managers = new List<BaseManager>();
        
        private List<BaseManager> _activeManagers = new List<BaseManager>();

        protected override void Awake()
        {
            base.Awake();
        }

        public async Task InitializeManagers(List<BaseManager> toInit)
        {
            foreach (BaseManager manager in toInit)
            {
                BaseManager newManager = Instantiate(manager, transform).GetComponent<BaseManager>();
                var result = await newManager.Init();
                _activeManagers.Add(newManager);

                if (result == InitializationStatus.Success)
                {
                    RHTebug.LogSuccess($"Manager {newManager.GetType()} initialized successfully.");
                }
                else if (result == InitializationStatus.Failure)
                {
                    RHTebug.LogError($"Manager {newManager.GetType()} failed to initialize.");
                }
            }
        }

        public T GetManager<T>() where T : BaseManager
        {
            foreach (BaseManager manager in _activeManagers)
            {
                if (manager is T)
                {
                    return manager as T;
                }
            }

            RHTebug.LogWarning($"Manager of type {typeof(T)} not found.");
            return null;
        }

        public async Task<T> AddManager<T>() where T : BaseManager
        {
            if (GetManager<T>() != null)
            {
                RHTebug.LogWarning($"Manager of type {typeof(T)} already exists.");
                return GetManager<T>();
            }
            foreach (BaseManager manager in _managers)
            {
                BaseManager newManager = Instantiate(manager, transform).GetComponent<BaseManager>();
                _managers.Add(newManager);
                await newManager.Init();
                
                return newManager as T;
            }
            return null;
        }

        // public T RemoveManager<T>() where T : BaseManager
        // {
        //     T managerToRemove = GetManager<T>();
        //     if (managerToRemove == null)
        //     {
        //         RHTebug.LogWarning($"Manager of type {typeof(T)} does not exist.");
        //         return null;
        //     }

        //     managerToRemove.Dispose();
        //     _managers.Remove(managerToRemove);
        //     Destroy(managerToRemove.gameObject);
        //     return managerToRemove;
        // }

        // does it make sense to have an add manager funciton
        // because if a module needs time to initialize...
        // whatever adds it shouldn't immediatley access it
        // maybe make it some kind of await/async function
#if UNITY_EDITOR
        [MenuItem("CONTEXT/GeneralManager/Collect Manager")]
        private static void CollectManagers(MenuCommand menuCommand)
        {
            GeneralManager generalManager = (GeneralManager)menuCommand.context;

            List<BaseManager> newMangers = new List<BaseManager>();
            // string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefabs/Screens" });
            string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" });
            foreach (var guid in guids)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));
                BaseManager screen = prefab.GetComponent<BaseManager>();
                if(screen != null)
                    newMangers.Add(screen);
            }

            generalManager._managers = newMangers;
            EditorUtility.SetDirty(generalManager);
        }
#endif
    }
}