using UnityEngine;

namespace RedHeadToolz.Debugging
{
    public class RHTebug
    {
        public static void Log(string message)
        {
#if RELEASE
            return;
#endif
            Debug.Log($"<color=orange>{message}</color>");
        }

        public static void LogWarning(string message, bool throwWarning = false)
        {
            if(throwWarning) Debug.LogWarning($"<color=red>{message}</color>");
#if RELEASE
            return;
#endif
            if(!throwWarning)Debug.Log($"<color=yellow>{message}</color>");
        }

        public static void LogError(string message, bool throwError = false)
        {
            if(throwError) Debug.LogError($"<color=red>{message}</color>");
#if RELEASE
            return;
#endif
            if(!throwError) Debug.Log($"<color=red>{message}</color>", null);
        }

        public static void LogSuccess(string message)
        {
#if RELEASE
            return;
#endif
            Debug.Log($"<color=green>{message}</color>");
        }
    }
}


// replace unity editor with develop flag and 
// have another script that when you create the project, 
// it adds the debug flag
// you can set toggle it off