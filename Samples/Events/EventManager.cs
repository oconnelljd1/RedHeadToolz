using UnityEngine;
using UnityEngine.Events;

namespace RedHeadToolz.Utils
{
    public class EventManager : MonoBehaviour
    {
        public static UnityAction ExampleEvent;
        public static void OnExampleEvent() => ExampleEvent?.Invoke();
    }
}