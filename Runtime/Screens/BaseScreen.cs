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

        public abstract void Show(Action callback = null);

        public abstract void ShowImmediate();

        public abstract void Hide(Action callback = null);

        public virtual void Close(Action callback = null)
        {
            Hide(() => {
                if(callback != null) callback();
                ScreenManager.Instance.CloseScreen(this);
            });
        }

        public virtual void Close()
        {
            Hide(()=>{
                ScreenManager.Instance.CloseScreen(this);
            });
        }
    }
}