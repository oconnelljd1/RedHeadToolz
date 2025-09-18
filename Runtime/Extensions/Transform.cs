using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace RedHeadToolz.Utils
{
    public static class TransformExtensions
    {
        // public static List<Transform> Children(this Transform transform)
        // {
        //     List<Transform> children = new List<Transform>();
        //     for(int i = 0; i < transform.childCount; i++)
        //     {
        //         children.Add(transform.GetChild(i));
        //     }
        //     return children;
        // }

        public static List<Transform> GetChildren(this Transform transform)
        {
            List<Transform> children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }
            return children;
            // return transform.Cast<Transform>().ToList();
        }

        public static void SetAsSecondLastChild(this Transform transform)
        {
            if (transform.parent != null)
            {
                transform.SetSiblingIndex(Mathf.Max(0, transform.parent.childCount - 2));
            }
        }

        public static void DeleteAllChildren(this Transform transform)
        {
            for(int i = transform.childCount - 1; i > -1; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}