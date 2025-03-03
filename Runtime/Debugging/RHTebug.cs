using UnityEngine;

namespace RedHeadToolz.Debugging
{
    public class RHTebug
    {
        public static void Log(string message)
        {
            Debug.Log($"<color=orange>{message}</color>");
        }

        public static void LogWarning(string message, bool throwWarning = false)
        {
            
            if(throwWarning) Debug.LogWarning($"<color=red>{message}</color>");
            else Debug.Log($"<color=yellow>{message}</color>");
        }

        public static void LogError(string message, bool throwError = false)
        {
            if(throwError) Debug.LogError($"<color=red>{message}</color>");
            else Debug.Log($"<color=red>{message}</color>", null);
        }

        public static void LogSuccess(string message)
        {
            Debug.Log($"<color=green>{message}</color>");
        }
    }
}
