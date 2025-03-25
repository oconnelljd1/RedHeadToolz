using UnityEngine;

public class DebugObject : MonoBehaviour
{
    public void Awake()
    {
#if RELEASE
        SetActive(false);
#endif
    }
}