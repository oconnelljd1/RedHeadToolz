using System.Collections.Generic;
using UnityEngine;

namespace RedHeadGamez.Toolz
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
    }
}