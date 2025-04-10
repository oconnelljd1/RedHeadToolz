using UnityEngine;
using DG.Tweening;
using System;
using RedHeadToolz.Debugging;

namespace RedHeadToolz.Screens
{
    // a class built to handle showing and hiding through a generic scale animation
    public class BasicScreen : BaseScreen
    {
        [Header("BasicScreen")]
        [SerializeField] private GameObject _root;

        public override void Show(Action callback = null)
        {
            // RHTebug.Log("BasicScreen.Show");
            _showing = true;
            _lockInput = false;
            _root.SetActive(true);
        }

        public override void ShowImmediate()
        {
            _showing = true;
            _lockInput = false;
            _root.SetActive(true);
        }

        public override void Hide(Action callback = null)
        {
            // RHTebug.Log("BasicScreen.Hide");
            _lockInput = true;
            _showing = false;
            _root.SetActive(false);
        }
    }
}
