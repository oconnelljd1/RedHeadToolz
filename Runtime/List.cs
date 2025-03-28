using System.Collections.Generic;
using UnityEngine;

namespace RedHeadToolz.Utils
{
    public static class ListExtensions
    {

        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogWarning("Cannot get a random element from an empty or null list.");
                return default;
            }

            int index = Random.Range(0, list.Count);
            return list[index];
        }
        
        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}