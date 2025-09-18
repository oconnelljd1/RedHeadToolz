using RedHeadToolz.Debugging;
using UnityEngine;

namespace RedHeadToolz.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        private static readonly object _lock = new object();

        protected Singleton() { }

        public static T Instance
        {
            get
            {
                // RHTebug.Log("Singleton get: " + typeof(T));
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).ToString() + " (Singleton)";

                        DontDestroyOnLoad(singleton);
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            // RHTebug.Log("Singleton Awake: " + typeof(T));
            if (_instance == null)
            {
                _instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}