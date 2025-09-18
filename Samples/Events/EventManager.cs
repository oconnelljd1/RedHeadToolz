using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RedHeadToolz.Utils
{
    // don't like this name as it doesn't fit the Manager system
    public class EventManager : MonoBehaviour
    {
        public static UnityAction ExampleEvent;
        public static void OnExampleEvent() => ExampleEvent?.Invoke();
    }
}