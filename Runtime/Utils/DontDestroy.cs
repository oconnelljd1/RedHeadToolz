using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedHeadToolz.Utils
{
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
