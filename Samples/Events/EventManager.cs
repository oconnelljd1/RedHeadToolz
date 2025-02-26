using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RedHeadGamez.Toolz
{
    public class EventManager : MonoBehaviour
    {
        public static UnityAction ExampleEvent;
        public static void OnExampleEvent() => ExampleEvent?.Invoke();
    }
}