using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace RedHeadToolz.Screens
{
    // A class that must be extended from if you expect to use it with the ScreenManager
    public abstract class BaseScreen : MonoBehaviour
    {
        [Header("Base Screen")]
        [SerializeField] List<BaseTransition> _inTransitions;
        // public List<BaseTransition> InTransitions => _inTransitions;
        [SerializeField] List<BaseTransition> _outTransitions;
        // public List<BaseTransition> OutTransitions => _outTransitions;

        [SerializeField] protected CanvasGroup _root;
        [SerializeField] protected GameObject _inputLock;
        [SerializeField] private bool _hideStack = false;
        public bool HideStack {
            get { return _hideStack; }
            set {}
        }
        [SerializeField] private bool _singleInstance = false;
        public bool SingleInstance {
            get { return _singleInstance; }
            set {}
        }

        protected bool _showing = false;
        public bool Showing => _showing;

        protected bool _lockInput{
            get{
                return _inputLock.activeSelf;
            }
            set{
                _inputLock.SetActive(value);
            }
        }

        public virtual void Show(Action callback = null)
        {
            StartCoroutine(ShowRoutine(callback));
        }

        private IEnumerator ShowRoutine(Action callback = null)
        {
            _lockInput = true;
            _showing = true;
            float elapsed = 0f;
            bool finished = false;

            while (finished == false)
            {
                finished = true;
                foreach (var transition in _inTransitions)
                {
                    transition.Evaluate(_root, elapsed);
                    if (finished == true)
                        finished = transition.IsComplete(elapsed);
                }
                if (finished == false)
                {
                    yield return null;
                    elapsed += UnityEngine.Time.deltaTime;
                }
            }
            _lockInput = false;
            if (callback != null) callback();
        }

        public virtual void ShowImmediate()
        {
            foreach (var transition in _inTransitions)
            {
                transition.Complete(_root);
            }
            _showing = true;
        }

        public virtual void Hide(Action callback = null)
        {
            StartCoroutine(HideRoutine(callback));
        }

        private IEnumerator HideRoutine(Action callback = null)
        {
            _lockInput = true;
            float elapsed = 0f;
            bool finished = false;

            while (finished == false)
            {
                finished = true;
                foreach (var transition in _outTransitions)
                {
                    transition.Evaluate(_root, elapsed);
                    if (finished == true)
                        finished = transition.IsComplete(elapsed);
                }
                if (finished == false)
                {
                    yield return null;
                    elapsed += UnityEngine.Time.deltaTime;
                }
            }
            _showing = false;
            _lockInput = false;
            if (callback != null) callback();
        }

        public virtual void Close(Action callback = null)
        {
            Hide(() =>
            {
                if (callback != null) callback();
                GeneralManager.Instance.GetManager<ScreenManager>().CloseScreen(this);
            });
        }

        public virtual void Close()
        {
            Hide(()=>{
                GeneralManager.Instance.GetManager<ScreenManager>().CloseScreen(this);
            });
        }

        public virtual void CloseImmediate()
        {
            GeneralManager.Instance.GetManager<ScreenManager>().CloseScreen(this);
        }
    }
}