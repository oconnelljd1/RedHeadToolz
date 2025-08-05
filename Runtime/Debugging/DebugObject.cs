using UnityEngine;

namespace RedHeadToolz.Debugging
{
    public class DebugObject : MonoBehaviour
    {
        public void Awake()
        {
#if RELEASE
            gameObject.SetActive(false);
#endif
        }
    }
}